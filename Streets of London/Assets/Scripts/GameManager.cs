using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {
    public float timer = 30f;
    public GameObject myTimer;
    public GameObject playerText;
    private float count;
    public GameObject camera;
    Text tTimer;
    Text pPlayer;
    public GameObject timeLine;
    private int side;


    

    private void Start()
    {
        
        side = 0;
        count = timer;
        tTimer = myTimer.GetComponent<Text>();
        pPlayer = playerText.GetComponent<Text>();
        InvokeRepeating("TimeLine", 1.0f, 1.0f);
        
    }
    public void SceneSwitch(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetPlayer(string name)
    {
        if (side == 0)
        {
            pPlayer.text = name;
        }
        else
        {
            pPlayer.text = name;
        }
    }

    void TimeLine()
    {
        if (count >= 0)
        {
            tTimer.text = count.ToString();
            timeLine.transform.localScale += new Vector3(-1/(timer+1), 0, 0);
        }else if(count == -1)
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
    private void Update()
    {
        if (count == -1)
        {
            CancelInvoke();
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
            Debug.Log(side);
            camera.GetComponent<CameraController>().SwitchSide(side);
        }
    }

    private void Reset()
    {
        timeLine.transform.localScale = new Vector3(1, 0, 0);
        count = 0;
        tTimer.text = timer.ToString();
    }
}
