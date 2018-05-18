using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DataBaseController : MonoBehaviour {

    IDbConnection dbconn;
    IDataReader reader;
    IDbCommand dbcmd;
    static bool init = false;

    void Initialise()
    {    
        init = true;
        CleanDB();
    }
    void OpenDBConnection()
    {
        if (!init)
        {
            Initialise();
        }
        Debug.Log("open connection");
        string conn = "URI=file:" + Application.dataPath + "/DB/PlayerData.db"; //Path to database.
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
    }


    
    public void CleanDB()
    {
        if (!init)
        {
            Initialise();
        }
        Debug.Log("clean");
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "DELETE FROM Spieler";
        dbcmd.ExecuteNonQuery();
        dbcmd.CommandText = "DELETE FROM Einheitentyp";
        dbcmd.ExecuteNonQuery();
        dbcmd.CommandText = "VACUUM";
        dbcmd.ExecuteNonQuery();
        CloseDBConnection();
    }
    public string RequestFromDB(string query)
    {
        if (!init)
        {
            Initialise();
        }
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

    public int[] GetUnitIds(int playerid)
    {
        if (!init)
        {
            Initialise();
        }
        int[] id = new int[5];
        int count = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select ID from Einheitentyp Where SpielerID ="+playerid;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            id[count] = reader.GetInt32(0);
            count++;
        }
        reader.Close();
        reader = null;
        CloseDBConnection();

        return id;
    }      

    public void WriteToDB(string query)
    {
        if (!init)
        {
            Initialise();
        }
        Debug.Log("test");
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = query;
        dbcmd.ExecuteNonQuery();
        CloseDBConnection();
    }

    void CloseDBConnection()
    {
        if (!init)
        {
            Initialise();
        }
        Debug.Log("close");
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
	
	
}
