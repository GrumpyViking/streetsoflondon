using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour {

    public GameObject playerOneText;
    Text pot;
    public GameObject playerTwoText;
    Text ptt;
    public GameObject ChooseStartingPlayer;
    Text ctp;
    private SwitchScene switchSceneScript;

    public void StartGame()
    {
        pot = playerOneText.GetComponent<Text>();
        ptt = playerTwoText.GetComponent<Text>();

        ctp = ChooseStartingPlayer.GetComponent<Text>();
        
        //setup database

        //übergabe werte an Spielansicht

        //Wechsel zur Spielansicht
        switchSceneScript = GameObject.FindGameObjectWithTag("Scene").GetComponent<SwitchScene>();
        switchSceneScript.ChangeScene(2);

    }

    
}
