using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KaufMenuScript : MonoBehaviour
{
    public GameObject kaufMenuScriptObject;
    public GameObject playerTextTop;
    public GameObject playerTextKaufMenu;
    public Sprite[] unitpictures;
    public GameObject[] units;

    int gold;
    int goldremaining;
    public GameObject goldvor;
    public GameObject goldnach;

    public GameObject anzeige1;
    public GameObject anzeige2;
    public GameObject anzeige3;
    public GameObject anzeige4;
    public GameObject anzeige5;
    public GameObject anzeigeTK;

    public GameObject[] gesamtEinheit;
    int[] einheitenPrice;
    int[] ids;
    public GameObject preis1;
    public GameObject preis2;
    public GameObject preis3;
    public GameObject preis4;
    public GameObject preis5;
    public GameObject preisTK;

    public DataBaseController dbc;
    public GameManager gm;

    public void OeffneKaufmenue()
    {
        kaufMenuScriptObject.SetActive(true);
        playerTextKaufMenu.GetComponent<Text>().text = playerTextTop.GetComponent<Text>().text;
        gold = Convert.ToInt32(dbc.RequestFromDB("Select Gold from Spieler where ID = " + PassthrougData.currentPlayer));
        goldremaining = gold;
        goldvor.GetComponent<Text>().text = Convert.ToString(gold);
        goldnach.GetComponent<Text>().text = Convert.ToString(goldremaining);
        EinheitentypAktualisierung(PassthrougData.currentPlayer);

    }

    public void SchliesseKaufmenu()
    {
        kaufMenuScriptObject.SetActive(false);
        anzeige1.GetComponent<Text>().text = "0";
        preis1.GetComponent<Text>().text = "0";
        anzeige2.GetComponent<Text>().text = "0";
        preis2.GetComponent<Text>().text = "0";
        anzeige3.GetComponent<Text>().text = "0";
        preis3.GetComponent<Text>().text = "0";
        anzeige4.GetComponent<Text>().text = "0";
        preis4.GetComponent<Text>().text = "0";
        anzeige5.GetComponent<Text>().text = "0";
        preis5.GetComponent<Text>().text = "0";
        anzeigeTK.GetComponent<Text>().text = "0";
        preisTK.GetComponent<Text>().text = "0";
        gm.Refresh();
    }
    //---------------------------------------------------------------------------------------------------------------
    //OnClick-Methoden der Buttons zum Erhöhen und Verringern der Kaufmenge der Einheitentypen 1-5 und Trickkarten
    public void ErhoeheAnzeige1()
    {
        if((Convert.ToInt32(preis1.GetComponent<Text>().text)+ Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis4.GetComponent<Text>().text)) < gold)
        {
            int x1 = Convert.ToInt32(anzeige1.GetComponent<Text>().text);
            String text1 = Convert.ToString(x1 + 1);
            anzeige1.GetComponent<Text>().text = text1;
            Preisaktualisierung1();
        }
        
    }

    public void VerringereAnzeige1()
    {

        if (Convert.ToInt32(anzeige1.GetComponent<Text>().text) > 0)
        {
            int x1 = Convert.ToInt32(anzeige1.GetComponent<Text>().text);
            String text1 = Convert.ToString(x1 - 1);
            anzeige1.GetComponent<Text>().text = text1;
            Preisaktualisierung1();
        }
    }

    public void ErhoeheAnzeige2()
    {
        if ((Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis4.GetComponent<Text>().text)) < gold)
        {
            int x2 = Convert.ToInt32(anzeige2.GetComponent<Text>().text);
            String text2 = Convert.ToString(x2 + 1);
            anzeige2.GetComponent<Text>().text = text2;
            Preisaktualisierung2();
        }
    }

    public void VerringereAnzeige2()
    {
        if (Convert.ToInt32(anzeige2.GetComponent<Text>().text) > 0)
        {
            int x2 = Convert.ToInt32(anzeige2.GetComponent<Text>().text);
            String text2 = Convert.ToString(x2 - 1);
            anzeige2.GetComponent<Text>().text = text2;
            Preisaktualisierung2();
        }
    }

    public void ErhoeheAnzeige3()
    {
        if ((Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis4.GetComponent<Text>().text)) < gold)
        {
            int x3 = Convert.ToInt32(anzeige3.GetComponent<Text>().text);
            String text3 = Convert.ToString(x3 + 1);
            anzeige3.GetComponent<Text>().text = text3;
            Preisaktualisierung3();
        }
    }

    public void VerringereAnzeige3()
    {
        if (Convert.ToInt32(anzeige3.GetComponent<Text>().text) > 0)
        {
            int x3 = Convert.ToInt32(anzeige3.GetComponent<Text>().text);
            String text3 = Convert.ToString(x3 - 1);
            anzeige3.GetComponent<Text>().text = text3;
            Preisaktualisierung3();
        }
    }

    public void ErhoeheAnzeige4()
    {
        if ((Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis4.GetComponent<Text>().text)) < gold)
        {
            int x4 = Convert.ToInt32(anzeige4.GetComponent<Text>().text);
            String text4 = Convert.ToString(x4 + 1);
            anzeige4.GetComponent<Text>().text = text4;
            Preisaktualisierung4();
        }
    }

    public void VerringereAnzeige4()
    {
        if (Convert.ToInt32(anzeige4.GetComponent<Text>().text) > 0)
        {
            int x4 = Convert.ToInt32(anzeige4.GetComponent<Text>().text);
            String text4 = Convert.ToString(x4 - 1);
            anzeige4.GetComponent<Text>().text = text4;
            Preisaktualisierung4();
        }
    }

    public void ErhoeheAnzeige5()
    {
        if ((Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis4.GetComponent<Text>().text)) < gold)
        {
            int x5 = Convert.ToInt32(anzeige5.GetComponent<Text>().text);
            String text5 = Convert.ToString(x5 + 1);
            anzeige5.GetComponent<Text>().text = text5;
            Preisaktualisierung5();
        }
    }

    public void VerringereAnzeige5()
    {
        if (Convert.ToInt32(anzeige5.GetComponent<Text>().text) > 0)
        {
            int x5 = Convert.ToInt32(anzeige5.GetComponent<Text>().text);
            String text5 = Convert.ToString(x5 - 1);
            anzeige5.GetComponent<Text>().text = text5;
            Preisaktualisierung5();
        }
    }

    public void ErhoeheAnzeigeTK()
    {
        if ((Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis4.GetComponent<Text>().text)) < gold)
        {
            int xTK = Convert.ToInt32(anzeigeTK.GetComponent<Text>().text);
            String textTK = Convert.ToString(xTK + 1);
            anzeigeTK.GetComponent<Text>().text = textTK;
            PreisaktualisierungTK();
        }
    }

    public void VerringereAnzeigeTK()
    {
        if (Convert.ToInt32(anzeigeTK.GetComponent<Text>().text) > 0)
        {
            int xTK = Convert.ToInt32(anzeigeTK.GetComponent<Text>().text);
            String textTK = Convert.ToString(xTK - 1);
            anzeigeTK.GetComponent<Text>().text = textTK;
            PreisaktualisierungTK();
        }
    }
    //---------------------------------------------------------------------------------------------------------------
    //Aktualisierung der Gesamtpreisanzeigen für die Einheitentypen 1-5 und die Trickkarten
    public void Preisaktualisierung1()
    {
        preis1.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige1.GetComponent<Text>().text) * einheitenPrice[0]);
        refrechRemainingGold();

    }

    public void Preisaktualisierung2()
    {
        preis2.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige2.GetComponent<Text>().text) * einheitenPrice[1]);
        refrechRemainingGold();
    }

    public void Preisaktualisierung3()
    {
        preis3.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige3.GetComponent<Text>().text) * einheitenPrice[2]);
        refrechRemainingGold();
    }

    public void Preisaktualisierung4()
    {
        preis4.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige4.GetComponent<Text>().text) * einheitenPrice[3]);
        refrechRemainingGold();
    }

    public void Preisaktualisierung5()
    {
        preis5.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige5.GetComponent<Text>().text) * einheitenPrice[4]);
        refrechRemainingGold();
    }

    public void PreisaktualisierungTK()
    {
        preisTK.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeigeTK.GetComponent<Text>().text) * 2);
        refrechRemainingGold();
    }
    //---------------------------------------------------------------------------------------------------------------

    public void refrechRemainingGold()
    {
        goldnach.GetComponent<Text>().text = Convert.ToString(gold- (Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis5.GetComponent<Text>().text)));
    }

    public void KaufBestaetigen()
    {
        //Schreibe Einheiten in DB
        //dbc.WriteToDB("Insert into Einheit(ID,Name,Aktionspunkte,Angriffspunkte,Lebenspunkte, Verteidigungspunkte,Reichweite,Kosten, SpielerID) VALues()");
        int[] boughtunits = { Convert.ToInt32(anzeige1.GetComponent<Text>().text), Convert.ToInt32(anzeige2.GetComponent<Text>().text), Convert.ToInt32(anzeige3.GetComponent<Text>().text), Convert.ToInt32(anzeige4.GetComponent<Text>().text), Convert.ToInt32(anzeige5.GetComponent<Text>().text) };
        string[] unitnames = new string[5];
        int offset;
        for (int i = 0; i < boughtunits.Length; i++)
        {
            if (boughtunits[i] != 0)
            {
                if (ids[i] == 1 || ids[i] == 2)
                {
                    unitnames[i] = "Boss";
                }
                if (ids[i] == 3 || ids[i] == 4)
                {
                    unitnames[i] = "Diebin";
                }
                if (ids[i] == 5 || ids[i] == 6)
                {
                    unitnames[i] = "Meuchelmoerder";
                }
                if (ids[i] == 7 || ids[i] == 8)
                {
                    unitnames[i] = "Pestarzt";
                }
                if (ids[i] == 9 || ids[i] == 10)
                {
                    unitnames[i] = "Polizist";
                }
                if (ids[i] == 11 || ids[i] == 12)
                {
                    unitnames[i] = "Raufbold";
                }
                if (ids[i] == 13 || ids[i] == 14)
                {
                    unitnames[i] = "Scharfschuetze";
                }
                if (ids[i] == 15 || ids[i] == 16)
                {
                    unitnames[i] = "Schlaeger";
                }
                if (ids[i] == 17 || ids[i] == 18)
                {
                    unitnames[i] = "Taschendieb";
                }
                if (ids[i] == 19 || ids[i] == 20)
                {
                    unitnames[i] = "Tueftler";
                }
            }
            for (int j = 0; j < boughtunits[i]; j++)
            {
                if (ids[i] == 1 || ids[i] == 2)
                {
                    if (PassthrougData.currentPlayer == 1)
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Boss',  3, 5, 3, 1, 5, 4, 1)");
                    }
                    else
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 2 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Boss',  3, 5, 3, 1, 5, 4, 2)");
                    }
                }
                if (ids[i] == 3 || ids[i] == 4)
                {
                    if (PassthrougData.currentPlayer == 1)
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Diebin',  3, 3, 2, 2, 3, 3, 1)");
                    }
                    else
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 2 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Diebin',  3, 3, 2, 2, 3, 3, 2)");
                    }
                }
                if (ids[i] == 5 || ids[i] == 6)
                {
                    if (PassthrougData.currentPlayer == 1)
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Meuchelmoerder',  4, 3, 1, 1, 4, 3, 1)");
                    }
                    else
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 2 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + " , 'Meuchelmoerder',  4, 3, 1, 1, 4, 3, 2)");
                    }
                }
                if (ids[i] == 7 || ids[i] == 8)
                {
                    if (PassthrougData.currentPlayer == 1)
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Pestarzt',  3, 3, 4, 1, 4, 1, 1)");
                    }
                    else
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 2 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Pestarzt',  3, 3, 4, 1, 4, 1, 2)");
                    }
                }
                if (ids[i] == 9 || ids[i] == 10)
                {
                    if (PassthrougData.currentPlayer == 1)
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Polizist',  2, 5, 3, 1, 3, 2, 1)");
                    }
                    else
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 2 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Polizist',  2, 5, 3, 1, 3, 2, 2)");
                    }
                }
                if (ids[i] == 11 || ids[i] == 12)
                {
                    if (PassthrougData.currentPlayer == 1)
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Raufbold',  2, 6, 3, 1, 3, 1, 1)");
                    }
                    else
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 2 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Raufbold',  2, 6, 3, 1, 3, 1, 2)");
                    }
                }
                if (ids[i] == 13 || ids[i] == 14)
                {
                    if (PassthrougData.currentPlayer == 1)
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Scharfschuetze',  3, 3, 2, 3, 4, 3, 1)");
                    }
                    else
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 2 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Scharfschuetze',  3, 3, 2, 3, 4, 3, 2)");
                    }
                }
                if (ids[i] == 15 || ids[i] == 16)
                {
                    if (PassthrougData.currentPlayer == 1)
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Schlaeger',  2, 4, 2, 1, 2, 2, 1)");
                    }
                    else
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 2 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Schlaeger',  2, 4, 2, 1, 2, 2, 2)");
                    }
                }
                if (ids[i] == 17 || ids[i] == 18)
                {
                    if (PassthrougData.currentPlayer == 1)
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Taschendieb',  4, 2, 1, 1, 2, 1, 1)");
                    }
                    else
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 2 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Taschendieb',  4, 2, 1, 1, 2, 1, 2)");
                    }
                }
                if (ids[i] == 19 || ids[i] == 20)
                {
                    if (PassthrougData.currentPlayer == 1)
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Tueftler',  2, 2, 1, 2, 5, 5, 1)");
                    }
                    else
                    {
                        //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 2 + "" + dbc.GetNumUnitsofPlayer(PassthrougData.currentPlayer) + ", 'Tueftler',  2, 2, 1, 2, 5, 5, 2)");
                    }
                }

                if (PassthrougData.currentPlayer == 2)
                {
                    if (i == 0)
                    {
                        offset = dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), PassthrougData.currentPlayer);
                        offset = offset - Convert.ToInt32(anzeige1.GetComponent<Text>().text);
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-900, ((j * 10f) + (10f * offset)), 1239), Quaternion.Euler(-90, 0, 0));

                    }
                    if (i == 1)
                    {
                        offset = dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), PassthrougData.currentPlayer);
                        offset = offset - Convert.ToInt32(anzeige1.GetComponent<Text>().text);
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-653, ((j * 10f) + (10f * offset)), 1233), Quaternion.Euler(-90, 0, 0));
                    }
                    if (i == 2)
                    {
                        offset = dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), PassthrougData.currentPlayer);
                        offset = offset - Convert.ToInt32(anzeige1.GetComponent<Text>().text);
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-895, ((j * 10f) + (10f * offset)), 1233), Quaternion.Euler(-90, 0, 0));
                    }
                    if (i == 3)
                    {
                        offset = dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), PassthrougData.currentPlayer);
                        offset = offset - Convert.ToInt32(anzeige1.GetComponent<Text>().text);
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-508.5f, ((j * 10f) + (10f * offset)), 1394), Quaternion.Euler(-90, 0, 0));
                    }
                    if (i == 4)
                    {
                        offset = dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), PassthrougData.currentPlayer);
                        offset = offset - Convert.ToInt32(anzeige1.GetComponent<Text>().text);
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-783.5f, ((j * 10f) + (10f * offset)), 1393), Quaternion.Euler(-90, 0, 0));
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        offset = dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), PassthrougData.currentPlayer);
                        offset = offset - Convert.ToInt32(anzeige1.GetComponent<Text>().text);
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-900, ((j * 10f) + (10f * offset)), -1239), Quaternion.Euler(-90, 180, 0));
                    }
                    if (i == 1)
                    {
                        offset = dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), PassthrougData.currentPlayer);
                        offset = offset - Convert.ToInt32(anzeige1.GetComponent<Text>().text);
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-627, ((j * 10f) + (10f * offset)), -1239), Quaternion.Euler(-90, 180, 0));

                    }
                    if (i == 2)
                    {
                        offset = dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), PassthrougData.currentPlayer);
                        offset = offset - Convert.ToInt32(anzeige1.GetComponent<Text>().text);
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-383, ((j * 10f) + (10f * offset)), -1243.6f), Quaternion.Euler(-90, 180, 0));

                    }
                    if (i == 3)
                    {
                        offset = dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), PassthrougData.currentPlayer);
                        offset = offset - Convert.ToInt32(anzeige1.GetComponent<Text>().text);
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-779, ((j * 10f) + (10f * offset)), -1400), Quaternion.Euler(-90, 180, 0));

                    }
                    if (i == 4)
                    {
                        offset = dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), PassthrougData.currentPlayer);
                        offset = offset - Convert.ToInt32(anzeige1.GetComponent<Text>().text);
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-501, ((j * 10f) + (10f * offset)), -1400), Quaternion.Euler(-90, 180, 0));

                    }
                }
            }
        }

        dbc.WriteToDB("UPDATE Spieler SET GOLD="+Convert.ToInt32(goldnach.GetComponent<Text>().text )+" Where ID ="+PassthrougData.currentPlayer);

        SchliesseKaufmenu();
    }

    public void EinheitentypAktualisierung(int playerID)
    {
        ids = new int[5];
        einheitenPrice = new int[5];
        ids = dbc.GetUnitIds(playerID);


        for(int i = 0; i < gesamtEinheit.Length; i++)
        {
            gesamtEinheit[i].GetComponent<Text>().text = Convert.ToString(dbc.GetNumofUnit(dbc.GetUnitName(ids[i]), playerID) + " / "+ Convert.ToString(dbc.GetMaxUnits(ids[i])));
            
            einheitenPrice[i] = dbc.GetUnitPrice(ids[i]);
        }


        for (int i = 0; i < units.Length; i++)
        {
            if (ids[i] == 1 || ids[i] == 2)
            {
                //material vom Boss zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[0];

            }
            if (ids[i] == 3 || ids[i] == 4)
            {
                //material vom Diebin zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[1];
            }
            if (ids[i] == 5 || ids[i] == 6)
            {
                //material vom Meuchelmörder zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[2];
            }
            if (ids[i] == 7 || ids[i] == 8)
            {
                //material vom pestarzt zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[3];
            }
            if (ids[i] == 9 || ids[i] == 10)
            {
                //material vom polizist zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[4];
            }
            if (ids[i] == 11 || ids[i] == 12)
            {
                //material vom raufbold zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[5];
            }
            if (ids[i] == 13 || ids[i] == 14)
            {
                //material vom scharfschuetze zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[6];
            }
            if (ids[i] == 15 || ids[i] == 16)
            {
                //material vom schlaeger zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[7];
            }
            if (ids[i] == 17 || ids[i] == 18)
            {
                //material vom taschendieb zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[8];
            }
            if (ids[i] == 19 || ids[i] == 20)
            {
                //material vom tueftler zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[9];
            }
        }
    }
}