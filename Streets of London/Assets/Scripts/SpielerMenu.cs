using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpielerMenu : MonoBehaviour {

    public GameObject spielerMenuScriptObject;
    public GameObject playerTextObject;
    public GameManager gm;

    private void Start()
    {
        
        if (PassthrougData.startPlayer == 0)
        {
            playerTextObject.GetComponent<Text>().text = PassthrougData.player1;
        }
        else
        {
            playerTextObject.GetComponent<Text>().text = PassthrougData.player2;
        }
 
    }
    
    public void PanelState(bool state)
    {
        spielerMenuScriptObject.SetActive(state);


    }

    public void StartGame()
    {
        gm.SetupScene();
    }

    public void ExitPorgram()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
