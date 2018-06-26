using UnityEngine;
using UnityEngine.UI;

/*
 * Anzeige zwischen den einzelnen Phasen
 * - beim Zugende eines Spielers wird der nächste Spieler Angezeigt
 * - Pausiert das Spielgeschehen
 * 
 * Autor: Martin Schuster
 */

public class SpielerMenu : MonoBehaviour {

    //UI-Objekte, die beeinflusst werden können
    public GameObject spielerMenuScriptObject;
    public GameObject playerTextObject;
    public GameObject mainUI;

    //Scripte, die genutzt werden
    public GameManager gm;
    public DataBaseController dbc;
    public UnitSelection us;
    public FieldBuilder fb;

    //Hilfsvariablen
    bool initialise;
    bool fieldBuild;
    int unitsSelected;

    //Initialisieren setzt Startwerte für Spielbeginn
    void Start()
    {
        initialise = false;
        fieldBuild = false;
        unitsSelected = 0;
        mainUI.SetActive(false);
        if (PassthroughData.currentPlayer == 1)
        {
            SetPlayer(PassthroughData.player1);
        }
        else
        {
            SetPlayer(PassthroughData.player2);
        }
    }
    
    //Anzeige des Panels
    public void PanelState(bool state)
    {
        spielerMenuScriptObject.SetActive(state);

    }

    //Name des Spielers setzen
    public void SetPlayer(string name)
    {
        playerTextObject.GetComponent<Text>().text = name;
    }

    //Prüft, ob das Spielfeld schon aufgebaut wurde
    //Ansonsten beginnt das Spiel
    public void CheckAction()
    {
        if (!fieldBuild)
        {
            FieldBuilder();
        }
        else
        {
            StartGame();
        }
    }

    //Setzt den Status für den Spielfeldaufbau
    public void SetFieldBuild(bool value)
    {
        fieldBuild = value;
    }

    //Zeigt das Menu zum Feldaufbau an
    void FieldBuilder()
    {
        fb.PanelState(true);
    }


    //Start des eigentlichen Spieles
    public void StartGame()
    {
        //Prüft, ob beide Spieler ihre Einheitentypen gewählt haben
        if (unitsSelected != 2)
        { 
            unitsSelected++;
            us.Auswahl();
            us.StartTimer();
        }
        else
        {
            //Wenn noch nicht geschehen, wird das Spiel initialisiert 
            if (!initialise)
            {
                gm.SetupScene();
                PassthroughData.gameActiv = true;
                initialise = true;
            }
            else //Andernfalls wird das Spiel fortgesetzt
            {
                PassthroughData.gameActiv = true;
                gm.Continue();
            }
        }
    }

    //Beenden des Programms
    public void ExitPorgram()
    {
        PassthroughData.unitsPlayer1 = 0; // Zurücksetzten der maximalen Einheiten Spieler 1
        PassthroughData.unitsPlayer2 = 0; // Zurücksetzten der maximalen Einheiten Spieler 2
        dbc.CleanDB(); // Säubere Datenbank am Spielende
        Application.Quit();//Beendet die Anwendung, wenn das Projekt exportiert wurde
        
        //UnityEditor.EditorApplication.isPlaying = false; // Beenden wenn im Editor gestartet
    }
}
