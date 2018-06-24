using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;

/*
 * Das DataBaseController Skript ist zuständig für die verwaltung der Datenbank.
 * 
 * Autor: Martin Schuster
 */ 

public class DataBaseController : MonoBehaviour {

    IDbConnection dbConn;
    IDataReader reader;
    IDbCommand dbCMD;
    static bool init = false;

    //Initialisiert Datenbank, wenn noch nicht initialisiert wurde
    void Initialise()
    {    
        init = true;
        CleanDB();
    }

    //Stellt die verbindung zur Datenbank her
    void OpenDBConnection()
    {
        if (!init)
        {
            Initialise();
        }        
        //string conn = "URI=file:" + Application.dataPath + "/DB/PlayerData.db"; //Pfad zur Datenbank

        string conn = "URI=file:" + System.IO.Path.Combine(Application.streamingAssetsPath, "DB/PlayerData.db");
        dbConn = (IDbConnection)new SqliteConnection(conn);
        dbConn.Open(); //Öffnet die Verbindung
    }
    
    //Leert die Tabellen
    public void CleanDB()
    {
        if (!init)
        {
            Initialise();
        }
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "DELETE FROM Spieler";
        dbCMD.ExecuteNonQuery(); //Führt dén Befehl ohne rückgabe von Informationen aus
        dbCMD.CommandText = "DELETE FROM Einheitentyp";
        dbCMD.ExecuteNonQuery(); //Führt dén Befehl ohne rückgabe von Informationen aus
        dbCMD.CommandText = "DELETE FROM Einheit";
        dbCMD.ExecuteNonQuery(); //Führt dén Befehl ohne rückgabe von Informationen aus
        dbCMD.CommandText = "DELETE FROM Gelaendefelder";
        dbCMD.ExecuteNonQuery(); //Führt dén Befehl ohne rückgabe von Informationen aus
        dbCMD.CommandText = "VACUUM";
        dbCMD.ExecuteNonQuery(); //Führt dén Befehl ohne rückgabe von Informationen aus
        CloseDBConnection();
    }

    //Schließt die Datenbank-Verbindung
    void CloseDBConnection()
    {
        if (!init)
        {
            Initialise();
        }
        dbCMD.Dispose();
        dbCMD = null;
        dbConn.Close();
        dbConn = null;
    }

    //Alte Funktion für universelle Abfrage
    public string RequestFromDB(string query)
    {
        if (!init)
        {
            Initialise();
        }
        string buff = "";
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = query;
        reader = dbCMD.ExecuteReader();
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

    //Einheitenrelevante Informationen
    //Einheiten ID
    public int GetUnitID(string name)
    {
        if (!init)
        {
            Initialise();
        }
        int id = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select ID from Einheit Where Name = '" + name + "' Order by ID DESC Limit 1";
        id = Convert.ToInt32(dbCMD.ExecuteScalar().ToString());

        CloseDBConnection();

        return id;
    }

    //Einheit Name
    public string GetUnitNamedif(int id)
    {
        if (!init)
        {
            Initialise();
        }
        string name = "";
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Name from Einheit Where ID = " + id + "";
        name = dbCMD.ExecuteScalar().ToString();

        CloseDBConnection();

        return name;
    }
    
    //Einheit Aktionspunkte
    public int GetAP(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int ap = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Aktionspunkte from Einheit Where ID =" + id + "";
        ap = Convert.ToInt32(dbCMD.ExecuteScalar().ToString());

        CloseDBConnection();

        return ap;
    }

    //Einheit Lebenspunkte
    public int GetLP(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int lp = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Lebenspunkte from Einheit Where ID =" + id + "";
        lp = Convert.ToInt32(dbCMD.ExecuteScalar().ToString());

        CloseDBConnection();

        return lp;
    }

    //Einheit Reichweite
    public int GetRW(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int rw = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Reichweite from Einheit Where ID =" + id + "";
        rw = Convert.ToInt32(dbCMD.ExecuteScalar().ToString());

        CloseDBConnection();

        return rw;
    }

    //Einheit Angriffswert
    public int GetAtt(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int att = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Angriffspunkte from Einheit Where ID =" + id + "";
        att = Convert.ToInt32(dbCMD.ExecuteScalar().ToString());

        CloseDBConnection();

        return att;
    }

    //Einheit Verteidigungswert
    public int GetDef(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int def = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Verteidigungspunkte from Einheit Where ID =" + id + "";
        def = Convert.ToInt32(dbCMD.ExecuteScalar().ToString());

        CloseDBConnection();

        return def;
    }

    //Spieler, dem die Einheit zugeordnet ist
    public int GetUnitPlayerID(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int pid = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select SpielerID from Einheit Where ID = " + id + "";
        pid = Convert.ToInt32(dbCMD.ExecuteScalar().ToString());

        CloseDBConnection();

        return pid;
    }

    //Einheiten Preis
    public int GetUnitPrice(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int price = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Kosten from EinheitenTyp Where ID =" + id;
        reader = dbCMD.ExecuteReader();
        while (reader.Read())
        {
            price = reader.GetInt32(0);
        }
        reader.Close();
        reader = null;
        CloseDBConnection();

        return price;
    }

    //Einheitentyp LP
    public int GetMaxLP(String name)
    {
        if (!init)
        {
            Initialise();
        }
        int maxlp = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Lebenspunkte from EinheitenTyp Where Name =" + name;
        reader = dbCMD.ExecuteReader();
        while (reader.Read())
        {
            maxlp = reader.GetInt32(0);
        }
        reader.Close();
        reader = null;
        CloseDBConnection();

        return maxlp;
    }

    //Einheitentyp Name
    public string GetUnitName(int id)
    {
        if (!init)
        {
            Initialise();
        }
        string name = "";
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Name from EinheitenTyp Where ID =" + id;
        reader = dbCMD.ExecuteReader();
        while (reader.Read())
        {
            name = reader.GetString(0);
        }
        reader.Close();
        reader = null;
        CloseDBConnection();

        return name;
    }

    //Geländefeld relevante Informationen
    //Geldändefeld Bonus
    public int GetFieldBonus(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int bonus = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Bonus from Gelaendefelder Where ID =" + id+"";
        bonus = Convert.ToInt32(dbCMD.ExecuteScalar());
        CloseDBConnection();
        return bonus;
    }

    //Name des Geländefeldes
    public string GetFieldName(int id)
    {
        if (!init)
        {
            Initialise();
        }
        string name = "";
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Name from Gelaendefelder Where ID =" + id + "";
        name = dbCMD.ExecuteScalar().ToString();
        CloseDBConnection();
        return name;
    }

    //Anzahl von einem Geländefeld
    public int NumofFields(string name)
    {
        if (!init)
        {
            Initialise();
        }
        int num = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select ID from Gelaendefelder Where Name = '" + name + "'";
        reader = dbCMD.ExecuteReader();
        while (reader.Read())
        {
            num++;
        }
        reader.Close();
        reader = null;
        CloseDBConnection();

        return num;
    }

    //Spielerrelevante Informationen
    //Spielername
    public string GetName(int id)
    {
        if (!init)
        {
            Initialise();
        }
        string name = "";
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Name from Spieler Where ID =" + id;
        name = dbCMD.ExecuteScalar().ToString();
        CloseDBConnection();
        return name;
    }

    //Spieler Gold
    public int GoldPlayer(int playerid)
    {
        if (!init)
        {
            Initialise();
        }
        int gold = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select Gold from Spieler Where ID =" + playerid+"";
        gold = Convert.ToInt32(dbCMD.ExecuteScalar().ToString());
        
        CloseDBConnection();

        return gold;
    }

    //Alle Einheitentypen eines Spielers
    public int[] GetUnitIds(int playerid)
    {
        if (!init)
        {
            Initialise();
        }
        int[] id = new int[5];
        int count = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select ID from Einheitentyp Where SpielerID =" + playerid;
        reader = dbCMD.ExecuteReader();
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

    //Unit-IDs des jeweiligen Spielers
    public int[] GetSingleUnitIdsByPlayerId(int playerid)
    {
        if (!init)
        {
            Initialise();
        }
        int[] id = new int[NumOfUnits(playerid)];
        int count = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select ID from Einheit Where SpielerID =" + playerid;
        reader = dbCMD.ExecuteReader();
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

    //Anzahl der Einheiten eines Spielers
    public int NumOfUnits(int playerid)
    {
        if (!init)
        {
            Initialise();
        }
        int num = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select ID from Einheit Where SpielerID =" + playerid;
        reader = dbCMD.ExecuteReader();
        while (reader.Read())
        {
            num++;
        }
        reader.Close();
        reader = null;
        CloseDBConnection();


        return num;
    }
        
    //Methode zum Schreiben in die Datenbank
    public void WriteToDB(string query)
    {
        if (!init)
        {
            Initialise();
        }
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = query;
        dbCMD.ExecuteNonQuery();
        CloseDBConnection();
    }

    //
    public int GetMaxUnits(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int maxNum = 0;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "Select MaxAnzahl from Einheitentyp Where ID =" + id;
        reader = dbCMD.ExecuteReader();
        while (reader.Read())
        {
            maxNum = reader.GetInt32(0);
        }
        reader.Close();
        reader = null;
        CloseDBConnection();
        return maxNum;
    }

    //Anzahl aller Einheiten des jeweiligen Spielers
    public int GetNumUnitsofPlayer(int id)
    {
        if (!init)
        {
            Initialise();
        }
        int anz=0;
        string check;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "SELECT COUNT(*) from Einheit";
        check = dbCMD.ExecuteScalar().ToString();

        if (check.Equals("0"))
        {
            CloseDBConnection();
            return 0;
        }
        else
        {
            dbCMD.CommandText = "Select ID from Einheit Where SpielerID =" + id;
            reader = dbCMD.ExecuteReader();

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
        string check;
        OpenDBConnection();
        dbCMD = dbConn.CreateCommand();
        dbCMD.CommandText = "SELECT COUNT(*) from Einheit";
        check = dbCMD.ExecuteScalar().ToString();

        if (check.Equals("0"))
        {
            CloseDBConnection();
            return 0;
        }
        else
        {
            string anzahl;
            dbCMD.CommandText = "Select COUNT(*) from Einheit Where Name =  '"+name+"' And SpielerID = "+playerid+"";

            anzahl = dbCMD.ExecuteScalar().ToString();
            CloseDBConnection();

            return Convert.ToInt32(anzahl);
        }
    }	
}
