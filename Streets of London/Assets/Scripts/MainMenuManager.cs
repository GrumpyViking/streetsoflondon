using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;


public class MainMenuManager : MonoBehaviour {

    public GameObject playerOneText;
    Text pot;
    public GameObject playerTwoText;
    Text ptt;
    
    public GameObject ChooseStartingPlayer;
    Text ctp;
    private SwitchScene switchSceneScript;
    public DataBaseController dbc;

    public void StartGame()
    {
        pot = playerOneText.GetComponent<Text>();
        ptt = playerTwoText.GetComponent<Text>();

        ctp = ChooseStartingPlayer.GetComponent<Text>();

        dbc.WriteToDB("INSERT INTO Spieler(ID, Name, Gold) VALUES (1,'" + pot.text + "',20)");
        dbc.WriteToDB("INSERT INTO Spieler(ID, Name, Gold) VALUES (2,'" + ptt.text + "' ,20)");

        PassthrougData.startPlayer = StartingPlayer(ctp.text);
        PassthrougData.player1 = pot.text;
        PassthrougData.player2 = ptt.text;

        //Wechsel zur Spielansicht
        switchSceneScript = GameObject.FindGameObjectWithTag("Scene").GetComponent<SwitchScene>();
        switchSceneScript.ChangeScene(2);
    }

    int StartingPlayer(string choise)
    {
        Debug.Log(choise);
        if(choise.Equals("Spieler 1"))
        {
            PassthrougData.currentPlayer = 1;
            return 1;
        }else if (choise.Equals("Spieler 2"))
        {
            PassthrougData.currentPlayer = 2;
            return 2;
        }
        else if (choise.Equals("Zufall"))
        {
            return Random.Range(1, 2);
        }
        else
        {
            return 0; //Default Player 1
        }
    }

}
