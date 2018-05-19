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

    //GameObjects
    public GameObject timeLine;

    //TextFelder
    public GameObject goldText;
    public GameObject myTimer;
    public GameObject playerText;
    public GameObject gesamtEinheiten;
    public GameObject beweglicheEinheiten;

    //Scripte
    public CameraController cc;
    public DataBaseController dbc;
    public SpielerMenu pm;
    public KaufMenuScript kms;
    public Ressources rc;
    public UnitTypCards utc;
   

    private void Start()
    {
        if (PassthrougData.player1 == null)
        {
            dbc.WriteToDB("INSERT INTO Spieler(ID, Name, Gold) VALUES (1, 'Spieler 1', 20)");
            PassthrougData.player1 = "Spieler 1";
        }
        if (PassthrougData.player2 == null)
        {
            dbc.WriteToDB("INSERT INTO Spieler(ID, Name, Gold) VALUES (2, 'Spieler 2', 20)");
            PassthrougData.player2 = "Spieler 2";
        }

        //Show SpielerMenu
        pm.PanelState(true);
        //Pause GameFlow
        paused = true;

        PassthrougData.gameactiv = false;
        defaultPosition = timeLine.transform.localScale;
    }

    public void SetupScene()
    {
        side = PassthrougData.startPlayer;
        
        if (PassthrougData.startPlayer == 0)
        {
            PassthrougData.currentPlayer = 1;
            cc.SwitchSide(PassthrougData.startPlayer);
            playerText.GetComponent<Text>().text = PassthrougData.player1;
            gesamtEinheiten.GetComponent<Text>().text = "Einheiten gesamt: " +Convert.ToString(dbc.NumOfUnits(1));
            beweglicheEinheiten.GetComponent<Text>().text = "bewegliche Einheiten: 3";
            goldText.GetComponent<Text>().text = "Gold: " + dbc.RequestFromDB("Select Gold from Spieler where ID = '1'");
            //rc.AktualisiereGold(1);
        }
        else
        {
            PassthrougData.currentPlayer = 2;
            cc.SwitchSide(PassthrougData.startPlayer);
            playerText.GetComponent<Text>().text = PassthrougData.player2;
            gesamtEinheiten.GetComponent<Text>().text = "Einheiten gesamt: " + Convert.ToString(dbc.NumOfUnits(2));
            beweglicheEinheiten.GetComponent<Text>().text = "bewegliche Einheiten: 3";
            goldText.GetComponent<Text>().text = "Gold: " + dbc.RequestFromDB("Select Gold from Spieler where ID = '2'");
            //rc.AktualisiereGold(2);
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
            PassthrougData.gameactiv = false;
            //Show SpielerMenu
            pm.PanelState(true);
        }
    }

    public void SetPlayer(string name)
    {
        playerText.GetComponent<Text>().text = name;
        //rc.AktualisiereGold(Convert.ToInt32(dbc.RequestFromDB("Select ID from Spieler where Name = " + name)));
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
                PassthrougData.gameactiv = false;
                Reset();
            }
            count--;
        }
        Debug.Log(count);
    }

    private void Reset()
    {
        timeLine.transform.localScale = defaultPosition;
        count = timer;
        tTimer.text = timer.ToString();
        if (side == 0)
        {
            side = 1;
            cc.SwitchSide(1);
            PassthrougData.startPlayer = side;
            PassthrougData.currentPlayer = 2;
            SetPlayer(PassthrougData.player2);
            pm.SetPlayer(PassthrougData.player2);
        }
        else
        {
            side = 0;
            cc.SwitchSide(0);
            PassthrougData.startPlayer = side;
            PassthrougData.currentPlayer = 1;
            SetPlayer(PassthrougData.player1);
            pm.SetPlayer(PassthrougData.player1);
        }
        pm.PanelState(true);
        kms.SchliesseKaufmenu();
    }

    public void Continue()
    {
        paused = false;
        PassthrougData.gameactiv = true;
        cc.cameraActiv = true;
    }

    public void ZugBeenden()
    {
        paused = true;
        PassthrougData.gameactiv = false;
        Reset();
    }
}
