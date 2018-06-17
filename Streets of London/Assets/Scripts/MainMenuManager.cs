using UnityEngine;  
using UnityEngine.UI;

/*
 * MainMenuManager Klasse:
 * 
 * Dient der verwaltung der Hauptmenu Szene. 
 * Sorgt dafür das die Namen der Spieler in die Datenbank geschrieben werden.
 * Bestimmt entsprechend der Auswahl den Beginnenden Spieler
 * 
 * 
 */
 
public class MainMenuManager : MonoBehaviour {

    //Variablen zur Bearbitung der Textobjekte
    public GameObject playerOneText;
    Text pot;

    public GameObject playerTwoText;
    Text ptt;

    public GameObject ChooseStartingPlayer;
    Text ctp;

    //Scripte auf die Zugegriffen wird
    private SwitchScene switchSceneScript;
    public DataBaseController dbc;

    //Wird mit dem Klick auf den Startbutton ausgeführt
    public void StartGame()
    {
        //Schreibt den Namen der spieler auf die variablen pot (playeronetext) & ptt (playertwotext)
        pot = playerOneText.GetComponent<Text>(); 
        ptt = playerTwoText.GetComponent<Text>();

        //Die auswahl welcher Spieler beginnt wird auf die variable ctp(choosestartingplayer) geschrieben
        ctp = ChooseStartingPlayer.GetComponent<Text>();

        //Schreibt den Namen der Spieler und das dazugehörige Startgold in die Datenbank
        dbc.WriteToDB("INSERT INTO Spieler(ID, Name, Gold) VALUES (1,'" + pot.text + "',20)");
        dbc.WriteToDB("INSERT INTO Spieler(ID, Name, Gold) VALUES (2,'" + ptt.text + "' ,20)");

        //Das PassthroughData Script bietet die möglichkeit grundlegende Daten zurverfügungzustellen
        PassthroughData.startPlayer = StartingPlayer(ctp.text);
        PassthroughData.player1 = pot.text;
        PassthroughData.player2 = ptt.text;

        //Wechsel zur Spielansicht
        switchSceneScript = GameObject.FindGameObjectWithTag("Scene").GetComponent<SwitchScene>();
        switchSceneScript.ChangeScene(1);//Lädt die Szene 1 
    }

    //Festlegen des aktuellen Spielern
    int StartingPlayer(string choise)
    {
        if(choise.Equals("Spieler 1"))
        {
            PassthroughData.currentPlayer = 1;
            return 1;
        }else if (choise.Equals("Spieler 2"))
        {
            PassthroughData.currentPlayer = 2;
            return 2;
        }
        else
        {
            PassthroughData.currentPlayer = Random.Range(1, 3); //Random.Range(1,3) da der Max Wert Exklusive ist
            return PassthroughData.currentPlayer;
        }
    }

}
