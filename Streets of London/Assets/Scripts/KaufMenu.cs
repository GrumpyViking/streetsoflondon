using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KaufMenu : MonoBehaviour
{
    public GameObject kaufMenuScriptObject;     //GameObject für den Kaufmenü-Bildschirm
    public GameObject playerTextTop;            //GameObject für die Anzeige des Spielernames am oberen Bildschirmrand
    public GameObject playerTextKaufMenu;       //GameObject für die Anzeige des Spielernames im Kaufmenü
    public Sprite[] unitpictures;               //Array für die Speicherung der Einheitentypenkarten-Bilder 
    public GameObject[] units;                  //Array für die Speicherung der GameObjects der Slots

    int gold;                       //Attribut zur Speicherung des aktuellen Goldes
    int goldremaining;              //Attribut zur Speicherung des verbleibenden Goldes
    public GameObject goldvor;      //GameObject zur Anzeige des Goldes vor dem Kauf
    public GameObject goldnach;     //GameObject zur Anzeige des Goldes nach dem Kauf

    //GameObjects zur Anzeige der Anzahl der gekauften Einheiten bzw. Trickkarten
    public GameObject anzeige1;
    public GameObject anzeige2;
    public GameObject anzeige3;
    public GameObject anzeige4;
    public GameObject anzeige5;
    public GameObject anzeigeTK;

    //GameObjects zum Spawnen der Einheiten für den Spieler auf der linken Spielfeldhälfte
    public GameObject spawnL1;
    public GameObject spawnL2;
    public GameObject spawnL3;
    public GameObject spawnL4;
    public GameObject spawnL5;

    //GameObjects zum Spawnen der Einheiten für den Spieler auf der rechten Spielfeldhälfte
    public GameObject spawnR1;
    public GameObject spawnR2;
    public GameObject spawnR3;
    public GameObject spawnR4;
    public GameObject spawnR5;

    public GameObject[] gesamtEinheit;      //Array zur Speicherung der GameObjects für die Anzeige der aktuellen Einheitenanzahl bzw. der maximalen Einheitenanzahl des Einheitentyps
    int[] einheitenPrice;                   //Array zur Speicherung der Preise der jeweiligen Einheitentypen
    int[] ids;                              //Array zur Speicherung der IDs der jeweiligen Einheitentypen

    int gekaufteTK = 0;                     //Attribut zur Speicherung und Übergabe der Anzahl der gekauften Trickkarten

    public GameObject preis1;       //GameObject zur Anzeige des Preises für den ersten Einheitentyp
    public GameObject preis2;       //GameObject zur Anzeige des Preises für den zweiten Einheitentyp
    public GameObject preis3;       //GameObject zur Anzeige des Preises für den dritten Einheitentyp
    public GameObject preis4;       //GameObject zur Anzeige des Preises für den vierten Einheitentyp
    public GameObject preis5;       //GameObject zur Anzeige des Preises für den fünften Einheitentyp
    public GameObject preisTK;      //GameObject zur Anzeige des Preises für die Trickkarten

    public DataBaseController dbc;      //DataBaseController-Object
    public GameManager gm;              //GameManager-Object
    public TKPicker tkp;                //TKPicker-Object

    //Methode zum Öffnen des Trickkartenmenüs
    public void OeffneKaufmenu()
    {
        kaufMenuScriptObject.SetActive(true);
        playerTextKaufMenu.GetComponent<Text>().text = playerTextTop.GetComponent<Text>().text;
        gold = Convert.ToInt32(dbc.RequestFromDB("Select Gold from Spieler where ID = " + PassthroughData.currentPlayer));
        goldremaining = gold;
        goldvor.GetComponent<Text>().text = Convert.ToString(gold);
        goldnach.GetComponent<Text>().text = Convert.ToString(goldremaining);
        EinheitentypAktualisierung(PassthroughData.currentPlayer);
    }

    //Methode zum Schließen des Trickkartenmenüs
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

    //Methode des Buttons zum Erhöhen der Kaufmenge des ersten Einheitentyps
    public void ErhoeheAnzeige1()
    {
        if((einheitenPrice[0] <= goldremaining) && 
            ((Convert.ToInt32(preis1.GetComponent<Text>().text)+ Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text) + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis5.GetComponent<Text>().text)) < gold) && 
            (dbc.GetNumofUnit(dbc.GetUnitName(ids[0]), PassthroughData.currentPlayer) + Convert.ToInt32(anzeige1.GetComponent<Text>().text) <5))
        {
            int x1 = Convert.ToInt32(anzeige1.GetComponent<Text>().text);
            String text1 = Convert.ToString(x1 + 1);
            anzeige1.GetComponent<Text>().text = text1;
            Preisaktualisierung1();
        }
        
    }

    //Methode des Buttons zum Verringern der Kaufmenge des ersten Einheitentyps
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

    //Methode des Buttons zum Erhöhen der Kaufmenge des zweiten Einheitentyps
    public void ErhoeheAnzeige2()
    {
        if ((Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis5.GetComponent<Text>().text)) < gold && einheitenPrice[1] <= goldremaining &&
            (dbc.GetNumofUnit(dbc.GetUnitName(ids[1]), PassthroughData.currentPlayer) + Convert.ToInt32(anzeige2.GetComponent<Text>().text) < 5))
        {
            int x2 = Convert.ToInt32(anzeige2.GetComponent<Text>().text);
            String text2 = Convert.ToString(x2 + 1);
            anzeige2.GetComponent<Text>().text = text2;
            Preisaktualisierung2();
        }
    }

    //Methode des Buttons zum Verringern der Kaufmenge des zweiten Einheitentyps
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

    //Methode des Buttons zum Erhöhen der Kaufmenge des dritten Einheitentyps
    public void ErhoeheAnzeige3()
    {
        if ((Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis5.GetComponent<Text>().text)) < gold && einheitenPrice[2] <= goldremaining &&
            (dbc.GetNumofUnit(dbc.GetUnitName(ids[2]), PassthroughData.currentPlayer) + Convert.ToInt32(anzeige3.GetComponent<Text>().text) < 5))
        {
            int x3 = Convert.ToInt32(anzeige3.GetComponent<Text>().text);
            String text3 = Convert.ToString(x3 + 1);
            anzeige3.GetComponent<Text>().text = text3;
            Preisaktualisierung3();
        }
    }

    //Methoden der Buttons zum Verringern der Kaufmenge des dritten Einheitentyps
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

    //Methoden der Buttons zum Erhöhen der Kaufmenge des vierten Einheitentyps
    public void ErhoeheAnzeige4()
    {
        if ((Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis5.GetComponent<Text>().text)) < gold && einheitenPrice[3] <= goldremaining &&
            (dbc.GetNumofUnit(dbc.GetUnitName(ids[3]), PassthroughData.currentPlayer) + Convert.ToInt32(anzeige4.GetComponent<Text>().text) < 5))
        {
            int x4 = Convert.ToInt32(anzeige4.GetComponent<Text>().text);
            String text4 = Convert.ToString(x4 + 1);
            anzeige4.GetComponent<Text>().text = text4;
            Preisaktualisierung4();
        }
    }

    //Methoden der Buttons zum Verringern der Kaufmenge des vierten Einheitentyps
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

    //Methoden der Buttons zum Erhöhen der Kaufmenge des fünften Einheitentyps
    public void ErhoeheAnzeige5()
    {
        if ((Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis5.GetComponent<Text>().text)) < gold && einheitenPrice[4] <= goldremaining &&
            (dbc.GetNumofUnit(dbc.GetUnitName(ids[4]), PassthroughData.currentPlayer) + Convert.ToInt32(anzeige5.GetComponent<Text>().text) < 5))
        {
            int x5 = Convert.ToInt32(anzeige5.GetComponent<Text>().text);
            String text5 = Convert.ToString(x5 + 1);
            anzeige5.GetComponent<Text>().text = text5;
            Preisaktualisierung5();
        }
    }

    //Methoden der Buttons zum Verringern der Kaufmenge des fünften Einheitentyps
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

    //Methoden der Buttons zum Erhöhen der Kaufmenge der Trickkarten
    public void ErhoeheAnzeigeTK()
    {
        if (goldremaining>4)
        {
            int xTK = Convert.ToInt32(anzeigeTK.GetComponent<Text>().text);
            String textTK = Convert.ToString(xTK + 1);
            anzeigeTK.GetComponent<Text>().text = textTK;
            PreisaktualisierungTK();
        }
    }

    //Methoden der Buttons zum Verringern der Kaufmenge der Trickkarten
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
    
    //Aktualisierung der Gesamtpreisanzeigen für den ersten Einheitentypen
    public void Preisaktualisierung1()
    {
        preis1.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige1.GetComponent<Text>().text) * einheitenPrice[0]);
        RefreshRemainingGold();

    }

    //Aktualisierung der Gesamtpreisanzeigen für den zweiten Einheitentypen
    public void Preisaktualisierung2()
    {
        preis2.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige2.GetComponent<Text>().text) * einheitenPrice[1]);
        RefreshRemainingGold();
    }

    //Aktualisierung der Gesamtpreisanzeigen für den dritten Einheitentypen
    public void Preisaktualisierung3()
    {
        preis3.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige3.GetComponent<Text>().text) * einheitenPrice[2]);
        RefreshRemainingGold();
    }

    //Aktualisierung der Gesamtpreisanzeigen für den vierten Einheitentypen
    public void Preisaktualisierung4()
    {
        preis4.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige4.GetComponent<Text>().text) * einheitenPrice[3]);
        RefreshRemainingGold();
    }

    //Aktualisierung der Gesamtpreisanzeigen für den fünften Einheitentypen
    public void Preisaktualisierung5()
    {
        preis5.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige5.GetComponent<Text>().text) * einheitenPrice[4]);
        RefreshRemainingGold();
    }

    //Aktualisierung der Gesamtpreisanzeigen für die Trickkarten
    public void PreisaktualisierungTK()
    {
        preisTK.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeigeTK.GetComponent<Text>().text) * 4);
        RefreshRemainingGold();
    }

    //Aktualisierung der Anzeige des verbleibenden Goldes nach dem Kauf
    public void RefreshRemainingGold()
    {
        goldnach.GetComponent<Text>().text = Convert.ToString(gold- (Convert.ToInt32(preis1.GetComponent<Text>().text) + Convert.ToInt32(preis2.GetComponent<Text>().text) + Convert.ToInt32(preis3.GetComponent<Text>().text)
            + Convert.ToInt32(preis4.GetComponent<Text>().text) + Convert.ToInt32(preis5.GetComponent<Text>().text) + Convert.ToInt32(preisTK.GetComponent<Text>().text)));
        goldremaining = Convert.ToInt32(goldnach.GetComponent<Text>().text);
    }

    //Methode zur Bestätigung des Kaufs, Schreiben der gekauften Einheiten in die Datenbank, Spawn der gekauften Einheiten und Öffnen des Trickkarten-Pickers
    public void KaufBestaetigen()
    {
        int[] boughtunits = { Convert.ToInt32(anzeige1.GetComponent<Text>().text), Convert.ToInt32(anzeige2.GetComponent<Text>().text), Convert.ToInt32(anzeige3.GetComponent<Text>().text), Convert.ToInt32(anzeige4.GetComponent<Text>().text), Convert.ToInt32(anzeige5.GetComponent<Text>().text) };
        string[] unitnames = new string[5];
        int offset;
        for (int i = 0; i < boughtunits.Length; i++)
        {
            //Namen der gewählten Einheitentypen werden ermittelt
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
            //Einheiten werden in die Datenbank geschrieben
            for (int j = 0; j < boughtunits[i]; j++)
            {
                if (ids[i] == 1 || ids[i] == 2)
                {
                    if (PassthroughData.currentPlayer == 1)
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + PassthroughData.unitsPlayer1 + ", 'Boss',  3, 5, 3, 1, 5, 4, 1)");
                        PassthroughData.unitsPlayer1++;
                    }
                    else
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 5 + "" + PassthroughData.unitsPlayer2 + ", 'Boss',  3, 5, 3, 1, 5, 4, 2)");
                        PassthroughData.unitsPlayer2++;
                    }
                }
                if (ids[i] == 3 || ids[i] == 4)
                {
                    if (PassthroughData.currentPlayer == 1)
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + PassthroughData.unitsPlayer1 + ", 'Diebin',  3, 3, 2, 2, 3, 3, 1)");
                        PassthroughData.unitsPlayer1++;
                    }
                    else
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 5 + "" + PassthroughData.unitsPlayer2 + ", 'Diebin',  3, 3, 2, 2, 3, 3, 2)");
                        PassthroughData.unitsPlayer2++;
                    }
                }
                if (ids[i] == 5 || ids[i] == 6)
                {
                    if (PassthroughData.currentPlayer == 1)
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + PassthroughData.unitsPlayer1 + ", 'Meuchelmoerder',  4, 3, 1, 1, 4, 3, 1)");
                        PassthroughData.unitsPlayer1++;
                    }
                    else
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 5 + "" + PassthroughData.unitsPlayer2 + " , 'Meuchelmoerder',  4, 3, 1, 1, 4, 3, 2)");
                        PassthroughData.unitsPlayer2++;
                    }
                }
                if (ids[i] == 7 || ids[i] == 8)
                {
                    if (PassthroughData.currentPlayer == 1)
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + PassthroughData.unitsPlayer1 + ", 'Pestarzt',  3, 3, 4, 1, 4, 1, 1)");
                        PassthroughData.unitsPlayer1++;
                    }
                    else
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 5 + "" + PassthroughData.unitsPlayer2 + ", 'Pestarzt',  3, 3, 4, 1, 4, 1, 2)");
                        PassthroughData.unitsPlayer2++;
                    }
                }
                if (ids[i] == 9 || ids[i] == 10)
                {
                    if (PassthroughData.currentPlayer == 1)
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + PassthroughData.unitsPlayer1 + ", 'Polizist',  2, 5, 3, 1, 3, 2, 1)");
                        PassthroughData.unitsPlayer1++;
                    }
                    else
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 5 + "" + PassthroughData.unitsPlayer2 + ", 'Polizist',  2, 5, 3, 1, 3, 2, 2)");
                        PassthroughData.unitsPlayer2++;
                    }
                }
                if (ids[i] == 11 || ids[i] == 12)
                {
                    if (PassthroughData.currentPlayer == 1)
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + PassthroughData.unitsPlayer1 + ", 'Raufbold',  2, 6, 3, 1, 3, 1, 1)");
                        PassthroughData.unitsPlayer1++;
                    }
                    else
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 5 + "" + PassthroughData.unitsPlayer2 + ", 'Raufbold',  2, 6, 3, 1, 3, 1, 2)");
                        PassthroughData.unitsPlayer2++;
                    }
                }
                if (ids[i] == 13 || ids[i] == 14)
                {
                    if (PassthroughData.currentPlayer == 1)
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + PassthroughData.unitsPlayer1 + ", 'Scharfschuetze',  3, 3, 2, 3, 4, 3, 1)");
                        PassthroughData.unitsPlayer1++;
                    }
                    else
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 5 + "" + PassthroughData.unitsPlayer2 + ", 'Scharfschuetze',  3, 3, 2, 3, 4, 3, 2)");
                        PassthroughData.unitsPlayer2++;
                    }
                }
                if (ids[i] == 15 || ids[i] == 16)
                {
                    if (PassthroughData.currentPlayer == 1)
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + PassthroughData.unitsPlayer1 + ", 'Schlaeger',  2, 4, 2, 1, 2, 2, 1)");
                        PassthroughData.unitsPlayer1++;
                    }
                    else
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 5 + "" + PassthroughData.unitsPlayer2 + ", 'Schlaeger',  2, 4, 2, 1, 2, 2, 2)");
                        PassthroughData.unitsPlayer2++;
                    }
                }
                if (ids[i] == 17 || ids[i] == 18)
                {
                    if (PassthroughData.currentPlayer == 1)
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + PassthroughData.unitsPlayer1 + ", 'Taschendieb',  4, 2, 1, 1, 2, 1, 1)");
                        PassthroughData.unitsPlayer1++;
                    }
                    else
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 5 + "" + PassthroughData.unitsPlayer2 + ", 'Taschendieb',  4, 2, 1, 1, 2, 1, 2)");
                        PassthroughData.unitsPlayer2++;
                    }
                }
                if (ids[i] == 19 || ids[i] == 20)
                {
                    if (PassthroughData.currentPlayer == 1)
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 1 + "" + PassthroughData.unitsPlayer1 + ", 'Tueftler',  2, 2, 1, 2, 5, 5, 1)");
                        PassthroughData.unitsPlayer1++;
                    }
                    else
                    {
                        dbc.WriteToDB("Insert Into Einheit (ID, Name, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (" + 5 + "" + PassthroughData.unitsPlayer2 + ", 'Tueftler',  2, 2, 1, 2, 5, 5, 2)");
                        PassthroughData.unitsPlayer2++;
                    }
                }
                //Einheiten erscheinen auf dem Spielfeld 
                if (PassthroughData.currentPlayer == 2)
                {
                    if (i == 0)
                    {
                        offset = spawnR1.GetComponent<SpawnHelper>().numOfUnits;
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-386, ((j * 10f) + (10f * offset)), 1239), Quaternion.Euler(-90, 0, 0), 1);
                        spawnR1.GetComponent<SpawnHelper>().numOfUnits++;
                    }
                    if (i == 1)
                    {
                        offset = spawnR2.GetComponent<SpawnHelper>().numOfUnits;
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-653, ((j * 10f) + (10f * offset)), 1233), Quaternion.Euler(-90, 0, 0), 2);
                        spawnR2.GetComponent<SpawnHelper>().numOfUnits++;
                    }
                    if (i == 2)
                    {
                        offset = spawnR3.GetComponent<SpawnHelper>().numOfUnits;
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-895, ((j * 10f) + (10f * offset)), 1233), Quaternion.Euler(-90, 0, 0), 3);
                        spawnR3.GetComponent<SpawnHelper>().numOfUnits++;
                    }
                    if (i == 3)
                    {
                        offset = spawnR4.GetComponent<SpawnHelper>().numOfUnits;
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-508.5f, ((j * 10f) + (10f * offset)), 1394), Quaternion.Euler(-90, 0, 0), 4);
                        spawnR4.GetComponent<SpawnHelper>().numOfUnits++;
                    }
                    if (i == 4)
                    {
                        offset = spawnR5.GetComponent<SpawnHelper>().numOfUnits;
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-783.5f, ((j * 10f) + (10f * offset)), 1393), Quaternion.Euler(-90, 0, 0), 5);
                        spawnR5.GetComponent<SpawnHelper>().numOfUnits++;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        offset = spawnL1.GetComponent<SpawnHelper>().numOfUnits;
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-900, ((j * 10f) + (10f * offset)), -1239), Quaternion.Euler(-90, 180, 0), 1);
                        spawnL1.GetComponent<SpawnHelper>().numOfUnits++;
                    }
                    if (i == 1)
                    {
                        offset = spawnL2.GetComponent<SpawnHelper>().numOfUnits;
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-627, ((j * 10f) + (10f * offset)), -1239), Quaternion.Euler(-90, 180, 0), 2);
                        spawnL2.GetComponent<SpawnHelper>().numOfUnits++;
                    }
                    if (i == 2)
                    {
                        offset = spawnL3.GetComponent<SpawnHelper>().numOfUnits;
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-383, ((j * 10f) + (10f * offset)), -1243.6f), Quaternion.Euler(-90, 180, 0), 3);
                        spawnL3.GetComponent<SpawnHelper>().numOfUnits++;
                    }
                    if (i == 3)
                    {
                        offset = spawnL4.GetComponent<SpawnHelper>().numOfUnits;
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-779, ((j * 10f) + (10f * offset)), -1400), Quaternion.Euler(-90, 180, 0), 4);
                        spawnL4.GetComponent<SpawnHelper>().numOfUnits++;
                    }
                    if (i == 4)
                    {
                        offset = spawnL5.GetComponent<SpawnHelper>().numOfUnits;
                        ObjectPooler.Instance.SpawnFromPool(unitnames[i], new Vector3(-501, ((j * 10f) + (10f * offset)), -1400), Quaternion.Euler(-90, 180, 0), 5);
                        spawnL5.GetComponent<SpawnHelper>().numOfUnits++;
                    }
                }
            }
        }

        dbc.WriteToDB("UPDATE Spieler SET GOLD=" + Convert.ToInt32(goldnach.GetComponent<Text>().text) + " Where ID =" + PassthroughData.currentPlayer);

        if (Convert.ToInt32(anzeigeTK.GetComponent<Text>().text) != 0)
        {
            gekaufteTK = Convert.ToInt32(anzeigeTK.GetComponent<Text>().text);
            SchliesseKaufmenu();
            tkp.OeffnePickerMenu(gekaufteTK);
        }
        else
        {
            SchliesseKaufmenu();
        }
    }

    //Aktualisierung der Einheitentypen zur Anzeige des korrekten Kaufmenüs für den jeweiligen Spieler
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