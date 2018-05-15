using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {
    
    //Variablen
    private float count;
    private int side;
    bool paused;
    public float timer = 10f;
    Text tTimer;
    private Vector3 defaultPosition;

    //GameObjects
    public GameObject camera;
    public GameObject timeLine;

    //TextFelder
    public GameObject goldText;
    public GameObject myTimer;
    public GameObject playerText;

    //Scripte
    public CameraController cc;
    public DataBaseController dbc;
    public SpielerMenu pm;
   

    private void Start()
    {
        //Show SpielerMenu
        pm.PanelState(true);
        //Pause GameFlow
        paused = true;
        PassthrougData.gameactiv = false;
        
    }

    public void SetupScene()
    {
        side = PassthrougData.startPlayer;
        if (PassthrougData.startPlayer == 0)
        {
            playerText.GetComponent<Text>().text = PassthrougData.player1;
            goldText.GetComponent<Text>().text = "Gold: " + dbc.RequestFromDB("Select Gold from Spieler where ID = '1'");
        }
        else
        {
            playerText.GetComponent<Text>().text = PassthrougData.player2;
            goldText.GetComponent<Text>().text = "Gold: " + dbc.RequestFromDB("Select Gold from Spieler where ID = '2'");         
        }
        count = timer;
        tTimer = myTimer.GetComponent<Text>();
        paused = false;
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
    }

    void TimeLine()
    {
        if (!paused)
        {
            if (count >= 0)
            {
                defaultPosition = timeLine.transform.localScale;
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
            PassthrougData.startPlayer = side;
            SetPlayer(PassthrougData.player2);
            pm.SetPlayer(PassthrougData.player2);
        }
        else
        {
            side = 0;
            PassthrougData.startPlayer = side;
            SetPlayer(PassthrougData.player1);
            pm.SetPlayer(PassthrougData.player1);
        }
        pm.PanelState(true);
    }

    public void Continue()
    {
        paused = false;
        PassthrougData.gameactiv = true;
        cc.cameraActiv = true;
    }

}
