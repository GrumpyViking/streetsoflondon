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


    public void StartGame()
    {
        pot = playerOneText.GetComponent<Text>();
        ptt = playerTwoText.GetComponent<Text>();

        ctp = ChooseStartingPlayer.GetComponent<Text>();

        //setup database
        string conn = "URI=file:" + Application.dataPath + "/DB/PlayerData.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "DELETE FROM Spieler";
        dbcmd.ExecuteNonQuery();

        dbcmd.CommandText = "VACUUM";
        dbcmd.ExecuteNonQuery();

        
        dbcmd.CommandText = "INSERT INTO Spieler(Name, Gold) VALUES ('"+pot.text+"',20)";
        dbcmd.ExecuteNonQuery();

        dbcmd.CommandText = "INSERT INTO Spieler(NAME, GOLD) Values('"+ptt.text+"' ,'20')";
        dbcmd.ExecuteNonQuery();

        string sqlQuery = "SELECT * FROM Spieler";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int gold = reader.GetInt32(2);

            Debug.Log("id= " + id + "  name =" + name + "  gold =" + gold);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;


        //übergabe werte an Spielansicht

        //Wechsel zur Spielansicht
        switchSceneScript = GameObject.FindGameObjectWithTag("Scene").GetComponent<SwitchScene>();
        switchSceneScript.ChangeScene(2);

    }



    
}
