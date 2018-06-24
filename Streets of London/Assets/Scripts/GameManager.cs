using UnityEngine;
using UnityEngine.UI;
using System;

/*
 * GameManager Skript
 * 
 * 
 * 
 * Autor: Martin Schuster
 */ 

public class GameManager : MonoBehaviour {
    
    //Variablen
    private float count;
    private int side;
    bool paused;
    public float timer = 10f;
    Text tTimer;
    private Vector3 defaultPosition;
    int turn;
    int overallTurns;
    //GameObjects
    public GameObject timeLine;

    public GameObject mainUI;

    //TextFelder
    public GameObject goldText;
    public GameObject zusatzGold;
    public GameObject myTimer;
    public GameObject playerText;
    public GameObject gesamtEinheiten;
    public GameObject beweglicheEinheiten;
    public GameObject endScreen;

    //Scripte
    public CameraController cc;
    public DataBaseController dbc;
    public SpielerMenu pm;
    public FieldBuilder fb;
    public KaufMenu kms;
    public Ressources rc;
    public UnitTypCards utc;
    public MoveUnit mu;
    public Bank b;
    public Trickkarten tk;

    private void Start()
    {
        //Default-Werte für Testzwecke 
        if (PassthroughData.player1 == null)
        {
            dbc.WriteToDB("INSERT INTO Spieler(ID, Name, Gold) VALUES (1, 'Spieler 1', 20)");
            PassthroughData.player1 = "Spieler 1";
            PassthroughData.currentPlayer = 1;
            PassthroughData.startPlayer = 1;
        }
        if (PassthroughData.player2 == null)
        {
            dbc.WriteToDB("INSERT INTO Spieler(ID, Name, Gold) VALUES (2, 'Spieler 2', 20)");
            PassthroughData.player2 = "Spieler 2";
        }

        //Show SpielerMenu
        pm.PanelState(true);
        //Pause GameFlow
        paused = true;
        //Hilfsvariable, wieviele Züge absolviert wurden
        overallTurns = 0;
        PassthroughData.gameActiv = false; 
        defaultPosition = timeLine.transform.localScale; //speichert die Ursprungsgröße der Zeitleiste
    }

    public void SetupScene()
    {
        mainUI.SetActive(true);
        turn = 0;
        if (PassthroughData.startPlayer == 1)
        {
            side = 1;
            PassthroughData.currentPlayer = 1;
            cc.SwitchSide(side);
            playerText.GetComponent<Text>().text = PassthroughData.player1;
            gesamtEinheiten.GetComponent<Text>().text = "Einheiten gesamt: " +Convert.ToString(dbc.NumOfUnits(1));
            beweglicheEinheiten.GetComponent<Text>().text = "3";
            mu.DeselectUnit();
            mu.DeselectFeld();
            rc.RefreshDisplay(1);
        }
        else
        {
            side = 2;
            PassthroughData.currentPlayer = 2;
            cc.SwitchSide(side);
            playerText.GetComponent<Text>().text = PassthroughData.player2;
            gesamtEinheiten.GetComponent<Text>().text = "Einheiten gesamt: " + Convert.ToString(dbc.NumOfUnits(2));
            beweglicheEinheiten.GetComponent<Text>().text = "3";
            mu.DeselectUnit();
            mu.DeselectFeld();
            rc.RefreshDisplay(2);
        }
        count = timer;
        utc.Initialise();
        tTimer = myTimer.GetComponent<Text>();
        paused = false;
        cc.cameraActiv = true;
        InvokeRepeating("TimeLine", 1.0f, 1.0f);
        
    }

    //Prüft am Ende eines Frames, ob die ESC-Taste gedrückt wurde, um das Spiel zu pausieren
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Pause GameFlow
            paused = true;
            PassthroughData.gameActiv = false;
            //Show SpielerMenu
            pm.PanelState(true);
        }
    }

    //Setzt den Namen des aktuellen Spielers
    public void SetPlayer(string name)
    {
        playerText.GetComponent<Text>().text = name;
    }

    //Möglichkeit die UI-Anzeigen eines Spieler zu aktualisieren
    public void Refresh()
    {
        goldText.GetComponent<Text>().text = "Gold: " + dbc.RequestFromDB("Select Gold from Spieler where ID = '"+PassthroughData.currentPlayer+"'");
        gesamtEinheiten.GetComponent<Text>().text = "Einheiten gesamt: " + Convert.ToString(dbc.NumOfUnits(PassthroughData.currentPlayer));
    }

    //Timer-Funktion, wenn Spiel nicht pausiert ist, läuft die Timerzeit runter und die Zeitleisten-Anzeige wird verringert
    void TimeLine()
    {
        if (!paused)
        {
            if (count >= 0)
            {
                tTimer.text = count.ToString();
                timeLine.transform.localScale += new Vector3(-1 / (timer + 1), 0, 0);
            }
            else if (count == -1)
            {
                paused = true;
                PassthroughData.gameActiv = false;
                Reset();
            }
            count--;
        }
    }

    //Wird am Ende des Timers oder mit dem Betätigen des "Zug Beenden"-Buttons ausgeführt
    //Setzt den Timer zurück und entsprechend des aktuellen Spielers wird der nächste Spieler gesetzt und die Anzeigen angepasst
    private void Reset()
    {
        timeLine.transform.localScale = defaultPosition;
        count = timer;
        tTimer.text = timer.ToString();
        if (side == 1)
        {
            side = 2;
            cc.SwitchSide(side);
            mu.DeselectUnit();
            mu.DeselectFeld();
            mu.DeselectFabrik();
            mu.DeselectGegner();
            mu.ResetAktionsMenu();
            gesamtEinheiten.GetComponent<Text>().text = "Einheiten gesamt: " + Convert.ToString(dbc.NumOfUnits(1));
            beweglicheEinheiten.GetComponent<Text>().text = "3";
            PassthroughData.startPlayer = side;
            PassthroughData.currentPlayer = 2;
            SetPlayer(PassthroughData.player2);
            pm.SetPlayer(PassthroughData.player2);
            turn++;
        }
        else
        {
            side = 1;
            cc.SwitchSide(side);
            mu.DeselectUnit();
            mu.DeselectFeld();
            mu.DeselectFabrik();
            mu.DeselectGegner();
            mu.ResetAktionsMenu();
            gesamtEinheiten.GetComponent<Text>().text = "Einheiten gesamt: " + Convert.ToString(dbc.NumOfUnits(2));
            beweglicheEinheiten.GetComponent<Text>().text = "3";
            PassthroughData.startPlayer = side;
            PassthroughData.currentPlayer = 1;
            SetPlayer(PassthroughData.player1);
            pm.SetPlayer(PassthroughData.player1);
            turn++;
        }
        tk.CheckActiveEffects(); 
        rc.RefreshDisplay(PassthroughData.currentPlayer);
        if (turn == 2)
        {
            TurnOver();
        }
        pm.PanelState(true);
        kms.SchliesseKaufmenu();
    }

    //Wenn eine Runde beendet wurde (Jeder Spieler war  einmal an der Reihe) wird die TurnOver Methode ausgeführt
    void TurnOver()
    {
        mu.ResetAP(); //Zurücksetzen der AP der eingesetzten Einheiten
        rc.AktualisiereGold(1); //Aktualisieren des Goldes von Spieler 1
        rc.AktualisiereGold(2); //Aktualisieren des Goldes von Spieler 2
        b.HasUnit(); //Prüft, ob eine Einheit auf dem Bankfeld ist und, wenn "Ja" fügt das vorhandene Gold einem Spieler zu, wenn "Nein" wird der Bank eine Goldmünze hinzugefügt
        turn = 0;
        overallTurns++;
    }

    //Funktion zum Fortsetzen eines Pausierten Spieles
    public void Continue()
    {
        paused = false;
        PassthroughData.gameActiv = true;
        cc.cameraActiv = true;
    }

    //Funktion zum Beenden eines Zuges
    public void ZugBeenden()
    {
        tk.SchliesseTrickkartenMenu();
        paused = true;
        PassthroughData.gameActiv = false;
        Reset();
    }

    //Wird ausgeführt, wenn Spielsiegbedingung erreicht wurde
    public void GameOver()
    {
        paused = true;
        endScreen.SetActive(true);
    }

    //Funktion, um Spiel zu Pausieren
    public void Paused()
    {
        paused = true;
    }
}
