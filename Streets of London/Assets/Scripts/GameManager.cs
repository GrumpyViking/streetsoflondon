using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {
    
    private float count;
    public GameObject camera;


    private int side;
    
    public GameObject timeLine;
    Text tTimer;
    public float timer = 30f;
    public GameObject myTimer;

    public SpielerMenu pm;
    public GameObject playerText;


    private void Start()
    {
        pm.PanelState(true);
    }

    public void SetupScene()
    {
        if (PassthrougData.startPlayer == 0)
        {
            playerText.GetComponent<Text>().text = PassthrougData.player1;
        }
        else
        {
            playerText.GetComponent<Text>().text = PassthrougData.player2;
        }

        side = 0;
        count = timer;
        tTimer = myTimer.GetComponent<Text>();
        InvokeRepeating("TimeLine", 1.0f, 1.0f);
    }


    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Pause GameFlow

            
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
        if (count >= 0)
        {
            tTimer.text = count.ToString();
            timeLine.transform.localScale += new Vector3(-1 / (timer + 1), 0, 0);
        }
        else if (count == -1)
        {
            Debug.Log("else");
            Reset();
            if (side == 0)
            {
                side = 1;
                SetPlayer("Spieler 1");
            }
            else
            {
                side = 0;
                SetPlayer("Spieler 2");
            }


        }

        count--;
        Debug.Log(count);
    }

    private void Reset()
    {
        timeLine.transform.localScale = new Vector3(1, 0, 0);
        count = 0;
        tTimer.text = timer.ToString();
    }

}
