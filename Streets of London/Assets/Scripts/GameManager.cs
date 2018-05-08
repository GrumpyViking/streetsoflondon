using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {
    
    private float count;
    public GameObject camera;
    bool paused;
    

    private int side;
    
    public GameObject timeLine;
    Text tTimer;
    public float timer = 10f;
    public GameObject myTimer;

    public SpielerMenu pm;
    public GameObject playerText;


    private void Start()
    {
        pm.PanelState(true);
        paused = true;
    }

    public void SetupScene()
    {
        side = PassthrougData.startPlayer;
        if (PassthrougData.startPlayer == 0)
        {
            playerText.GetComponent<Text>().text = PassthrougData.player1;
        }
        else
        {
            playerText.GetComponent<Text>().text = PassthrougData.player2;
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
                tTimer.text = count.ToString();
                timeLine.transform.localScale += new Vector3(-1 / (timer + 1), 0, 0);
            }
            else if (count == -1)
            {
                Reset();
            }
            count--;
        }
        

        
        Debug.Log(count);
    }

    private void Reset()
    {
        timeLine.transform.localScale += new Vector3(1, 0, 0);
        count = timer;
        tTimer.text = timer.ToString();
        if (side == 0)
        {
            side = 1;
            SetPlayer(PassthrougData.player2);
            pm.SetPlayer(PassthrougData.player2);
            

        }
        else
        {
            side = 0;
            SetPlayer(PassthrougData.player1);
            pm.SetPlayer(PassthrougData.player1);
        }
        pm.PanelState(true);
    }

    public void Continue()
    {
        paused = false;
    }

}
