using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DataBaseController : MonoBehaviour {

    IDbConnection dbconn;
    IDataReader reader;
    IDbCommand dbcmd;
    static bool init = false;
    void OpenDBConnection()
    {
        string conn = "URI=file:" + Application.dataPath + "/DB/PlayerData.db"; //Path to database.
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
    }


    void Start () {
        if (!init)
        {
            CleanDB();
            init = true;
        }
        
    }
    public void CleanDB()
    {
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "DELETE FROM Spieler";
        dbcmd.ExecuteNonQuery();

        dbcmd.CommandText = "VACUUM";
        dbcmd.ExecuteNonQuery();
        CloseDBConnection();
    }
    public string RequestFromDB(string query)
    {
        string buff = "";
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int gold = reader.GetInt32(0);

            buff = gold.ToString();
        }

        reader.Close();
        reader = null;
        CloseDBConnection();

        return buff;
    }

    public void WriteToDB(string query)
    {
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = query;
        dbcmd.ExecuteNonQuery();
        CloseDBConnection();
    }

    void CloseDBConnection()
    {
        
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
	
	
}
