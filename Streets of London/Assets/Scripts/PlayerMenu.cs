using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour {

    public GameObject playerMenu;
    public GameObject playerName;
    Text player;

    private void Start()
    {
        player = playerName.GetComponent<Text>();
        if (PassthrougData.startPlayer == 0)
        {
            player.text = PassthrougData.player1;
        }
        else
        {
            player.text = PassthrougData.player2;
        }
    }


    public void PanelState(bool state)
    {
        playerMenu.SetActive(state);
    }
}
