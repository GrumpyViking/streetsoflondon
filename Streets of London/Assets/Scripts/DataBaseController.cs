using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DataBaseController : MonoBehaviour {

    IDbConnection dbconn;
    IDataReader reader;
    IDbCommand dbcmd;

    void OpenDBConnection()
    {
        string conn = "URI=file:" + Application.dataPath + "/DB/PlayerData.db"; //Path to database.
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
    }


    void Start () {
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
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int gold = reader.GetInt32(2);
        reader.Close();
        reader = null;
        CloseDBConnection();

        return "id " + id + "  name =" + name + "  gold =" + gold;
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
