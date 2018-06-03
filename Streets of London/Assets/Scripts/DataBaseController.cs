using UnityEngine;
using System;
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
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "DELETE FROM Spieler";
        dbcmd.ExecuteNonQuery();
        dbcmd.CommandText = "DELETE FROM Einheitentyp";
        dbcmd.ExecuteNonQuery();
        dbcmd.CommandText = "DELETE FROM Einheit";
        dbcmd.ExecuteNonQuery();
        dbcmd.CommandText = "DELETE FROM Gelaendefelder";
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

    public int GetLP(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int lp = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select Lebenspunkte from Einheit Where ID =" + id + "";
        lp = Convert.ToInt32(dbcmd.ExecuteScalar().ToString());

        CloseDBConnection();

        return lp;
    }

    public int GetRW(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int rw = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select Reichweite from Einheit Where ID =" + id + "";
        rw = Convert.ToInt32(dbcmd.ExecuteScalar().ToString());

        CloseDBConnection();

        return rw;
    }

    public int GetAtt(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int att = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select Angriffspunkte from Einheit Where ID =" + id + "";
        att = Convert.ToInt32(dbcmd.ExecuteScalar().ToString());

        CloseDBConnection();

        return att;
    }

    public int GetDef(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int def = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select Verteidigungspunkte from Einheit Where ID =" + id + "";
        def = Convert.ToInt32(dbcmd.ExecuteScalar().ToString());

        CloseDBConnection();

        return def;
    }

    public int GoldPlayer(int playerid)
    {
        if (!init)
        {
            Initialise();
        }
        int gold = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select Gold from Spieler Where ID =" + playerid+"";
        gold = Convert.ToInt32(dbcmd.ExecuteScalar().ToString());
        
        CloseDBConnection();

        return gold;
    }

    public Unit GetUnitInfo(int unitID)
    {
        if (!init)
        {
            Initialise();
        }
        Unit unit = null;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select * from Einheit Where ID =" + unitID + "";
        unit = (Unit) dbcmd.ExecuteScalar();

        CloseDBConnection();

        return unit;
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

    public int NumofFields(string name)
    {
        if (!init)
        {
            Initialise();
        }
        int num = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select ID from Gelaendefelder Where Name = '"+name+"'";
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            num++;
        }
        reader.Close();
        reader = null;
        CloseDBConnection();

        return num;
    }
    
    public int NumOfUnits(int playerid)
    {
        if (!init)
        {
            Initialise();
        }
        int num = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select ID from Einheit Where SpielerID =" + playerid;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            num++;
        }
        reader.Close();
        reader = null;
        CloseDBConnection();


        return num;
    }

    public string GetName(int id)
    {
        if (!init)
        {
            Initialise();
        }
        string name = "";
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select Name from Spieler Where ID ="+id;
        name = dbcmd.ExecuteScalar().ToString();
        CloseDBConnection();
        return name;
    }

    public int GetFieldBonus(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int bonus = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select Bonus from Gelaendefelder Where ID =" + id;
        bonus= Convert.ToInt32(dbcmd.ExecuteScalar());
        CloseDBConnection();
        return bonus;
    }

    public void WriteToDB(string query)
    {
        if (!init)
        {
            Initialise();
        }
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = query;
        dbcmd.ExecuteNonQuery();
        CloseDBConnection();
    }

    public int GetMaxUnits(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int maxNum = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select MaxAnzahl from Einheitentyp Where ID =" + id;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            maxNum = reader.GetInt32(0);
        }
        reader.Close();
        reader = null;
        CloseDBConnection();
        return maxNum;
    }
    public int GetNumUnitsofPlayer(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int anz=0;
        string check;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "SELECT COUNT(*) from Einheit";
        check = dbcmd.ExecuteScalar().ToString();

        if (check.Equals("0"))
        {
            CloseDBConnection();
            return 0;
        }
        else
        {
            dbcmd.CommandText = "Select ID from Einheit Where SpielerID =" + id;
            reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                anz++;
            }
            reader.Close();
            reader = null;
            CloseDBConnection();

            return anz;
        }
    }


    public int GetNumofUnit(string name, int playerid)
    {
        if (!init)
        {
            Initialise();
        }
        int count = 0;
        string check;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "SELECT COUNT(*) from Einheit";
        check = dbcmd.ExecuteScalar().ToString();

        if (check.Equals("0"))
        {
            CloseDBConnection();
            return 0;
        }
        else
        {
            string anzahl;
            dbcmd.CommandText = "Select COUNT(*) from Einheit Where Name =  '"+name+"' And SpielerID = "+playerid+"";

            anzahl = dbcmd.ExecuteScalar().ToString();
            Debug.Log(anzahl);
            CloseDBConnection();

            return Convert.ToInt32(anzahl);
        }
    }

    public int GetUnitPrice(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int price = 0;
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select Kosten from EinheitenTyp Where ID =" + id;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            price = reader.GetInt32(0);
        }
        reader.Close();
        reader = null;
        CloseDBConnection();

        return price;

    }

    public string GetUnitName(int id)
    {
        if (!init)
        {
            Initialise();
        }
        string name="";
        OpenDBConnection();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "Select Name from EinheitenTyp Where ID =" + id;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            name = reader.GetString(0);
        }
        reader.Close();
        reader = null;
        CloseDBConnection();

        return name;
    }

    void CloseDBConnection()
    {
        if (!init)
        {
            Initialise();
        }
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
	
	
}
