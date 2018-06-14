using UnityEngine;
using UnityEngine.UI;
using System;



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
    public KaufMenuScript kms;
    public Ressources rc;
    public UnitTypCards utc;
    public MoveUnit mu;
    public Bank b;

    private void Start()
    {
        //Default werte für test zwecke
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
        overallTurns = 0;
        PassthroughData.gameActiv = false;
        defaultPosition = timeLine.transform.localScale;
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

    public void SetPlayer(string name)
    {
        playerText.GetComponent<Text>().text = name;
        //rc.AktualisiereGold(Convert.ToInt32(dbc.RequestFromDB("Select ID from Spieler where Name = " + name)));
    }

    public void Refresh()
    {
        goldText.GetComponent<Text>().text = "Gold: " + dbc.RequestFromDB("Select Gold from Spieler where ID = '"+PassthroughData.currentPlayer+"'");
        gesamtEinheiten.GetComponent<Text>().text = "Einheiten gesamt: " + Convert.ToString(dbc.NumOfUnits(PassthroughData.currentPlayer));
    }

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
            gesamtEinheiten.GetComponent<Text>().text = "Einheiten gesamt: " + Convert.ToString(dbc.NumOfUnits(2));
            beweglicheEinheiten.GetComponent<Text>().text = "3";
            PassthroughData.startPlayer = side;
            PassthroughData.currentPlayer = 1;
            SetPlayer(PassthroughData.player1);
            pm.SetPlayer(PassthroughData.player1);
            turn++;
        }
        rc.RefreshDisplay(PassthroughData.currentPlayer);
        if (turn == 2)
        {
            TurnOver();
        }
        pm.PanelState(true);
        kms.SchliesseKaufmenu();
    }

    void TurnOver()
    {
        rc.AktualisiereGold(1);
        rc.AktualisiereGold(2);
        b.HasUnit();
        b.IncreaseGold();
        turn = 0;
        overallTurns++;
    }

    public void Continue()
    {
        paused = false;
        PassthroughData.gameActiv = true;
        cc.cameraActiv = true;
    }

    public void ZugBeenden()
    {
        paused = true;
        PassthroughData.gameActiv = false;
        Reset();
    }

    public void GameOver()
    {
        paused = true;
        endScreen.SetActive(true);
    }

    public void Paused()
    {
        paused = true;
    }
}
