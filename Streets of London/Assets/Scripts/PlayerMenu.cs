using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour {

    public GameObject playerMenu;
    public GameObject playerName;
    private void Start()
    {
        
        if (PassthrougData.startPlayer == 0)
        {
            playerName.GetComponent<Text>().text = PassthrougData.player1;
        }
        else
        {
            playerName.GetComponent<Text>().text = PassthrougData.player2;
        }
 
    }
    
    public void PanelState(bool state)
    {
        playerMenu.SetActive(state);
    }

    public void ExitPorgram()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
