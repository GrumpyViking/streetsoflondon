using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelection : MonoBehaviour {

    //Spiel-Objekte
    public GameObject unitSelectionScriptObject;
    public GameObject playerTextSpielerMenu;
    public GameObject playerTextEinheitenAuswahl;
    public GameObject zeitleiste;
    public GameObject timerText;
    public SpielerMenu sm;
    public DataBaseController dbc;

    //Aufleucht-Objekte
    public GameObject leuchten01;
    public GameObject leuchten02;
    public GameObject leuchten03;
    public GameObject leuchten04;
    public GameObject leuchten05;
    public GameObject leuchten06;
    public GameObject leuchten07;
    public GameObject leuchten08;
    public GameObject leuchten09;
    public GameObject leuchten10;

    

    public float timer;
    private float count;
    private bool paused;
    private int unitsChosen = 0;            //Counter der gewaehlten Einheiten
    private int side;                       //Angabe des aktuellen Spielers
    private Vector3 defaultPosition;

    //Bool-Werte für die Auswahl der Einheitentypen
    private bool boss = false;
    private bool diebin = false;
    private bool meuchelmoerder = false;
    private bool pestarzt = false;
    private bool polizist = false;
    private bool raufbold = false;
    private bool scharfschuetze = false;
    private bool schlaeger = false;
    private bool taschendieb = false;
    private bool tueftler = false;

    private bool finish = false;            //Bool-Wert um die Auswahl für den zweiten Spieler zu ermöglichen

    private bool bossRnd = false;
    private bool diebinRnd = false;
    private bool meuchelmoerderRnd = false;
    private bool pestarztRnd = false;
    private bool polizistRnd = false;
    private bool raufboldRnd = false;
    private bool scharfschuetzeRnd = false;
    private bool schlaegerRnd = false;
    private bool taschendiebRnd = false;
    private bool tueftlerRnd = false;

    

    private void Start()
    {
    }

    public void StartTimer()
    {
        count = timer;
        paused = false;
        InvokeRepeating("TimeLine", 1.0f, 1.0f);
    }

    //---------------------------------------------------------------------
    //Methode zum Aufrufen der ersten Auswahl
    public void Auswahl()
    {
        defaultPosition = zeitleiste.transform.localScale;
        unitSelectionScriptObject.SetActive(true);
        playerTextEinheitenAuswahl.GetComponent<Text>().text = playerTextSpielerMenu.GetComponent<Text>().text;
        if (PassthroughData.currentPlayer == 1)
        {
            side = 0;
        }
        else
        {
            side = 1;
        }
    }

    //---------------------------------------------------------------------
    //OnClick-Methoden für die einzelnen Einheitentypen-Buttons

    public void BossSelected()
    {
        if (boss == false)
        {
            if (unitsChosen < 5)
            {
                boss = true;
                leuchten01.SetActive(true);
                unitsChosen++;
            } else { }
        } else
        {
            boss = false;
            leuchten01.SetActive(false);
            unitsChosen--;
        }  
    }

    public void DiebinSelected()
    {
        if (diebin == false)
        {
            if (unitsChosen < 5)
            {
                diebin = true;
                leuchten02.SetActive(true);
                unitsChosen++;
            }
            else { }
        }
        else
        {
            diebin = false;
            leuchten02.SetActive(false);
            unitsChosen--;
        }
    }

    public void MeuchelmoerderSelected()
    {
        if (meuchelmoerder == false)
        {
            if (unitsChosen < 5)
            {
                meuchelmoerder = true;
                leuchten03.SetActive(true);
                unitsChosen++;
            }
            else { }
        }
        else
        {
            meuchelmoerder = false;
            leuchten03.SetActive(false);
            if (unitsChosen > 0)
            unitsChosen--;
        }
    }

    public void PestarztSelected()
    {
        if (pestarzt == false)
        {
            if (unitsChosen < 5)
            {
                pestarzt = true;
                leuchten04.SetActive(true);
                unitsChosen++;
            }
            else { }
        }
        else
        {
            pestarzt = false;
            leuchten04.SetActive(false);
            unitsChosen--;
        }
    }

    public void PolizistSelected()
    {
        if (polizist == false)
        {
            if (unitsChosen < 5)
            {
                polizist = true;
                leuchten05.SetActive(true);
                unitsChosen++;
            }
            else { }
        }
        else
        {
            polizist = false;
            leuchten05.SetActive(false);
            unitsChosen--;
        }
    }

    public void RaufboldSelected()
    {
        if (raufbold == false)
        {
            if (unitsChosen < 5)
            {
                raufbold = true;
                leuchten06.SetActive(true);
                unitsChosen++;
            }
            else { }
        }
        else
        {
            raufbold = false;
            leuchten06.SetActive(false);
            unitsChosen--;
        }
    }

    public void ScharfschuetzeSelected()
    {
        if (scharfschuetze == false)
        {
            if (unitsChosen < 5)
            {
                scharfschuetze = true;
                leuchten07.SetActive(true);
                unitsChosen++;
            }
            else { }
        }
        else
        {
            scharfschuetze = false;
            leuchten07.SetActive(false);
            unitsChosen--;
        }
    }

    public void SchlaegerSelected()
    {
        if (schlaeger == false)
        {
            if (unitsChosen < 5)
            {
                schlaeger = true;
                leuchten08.SetActive(true);
                unitsChosen++;
            }
            else { }
        }
        else
        {
            schlaeger = false;
            leuchten08.SetActive(false);
            unitsChosen--;
        }
    }

    public void TaschendiebSelected()
    {
        if (taschendieb == false)
        {
            if (unitsChosen < 5)
            {
                taschendieb = true;
                leuchten09.SetActive(true);
                unitsChosen++;
            }
            else { }
        }
        else
        {
            taschendieb = false;
            leuchten09.SetActive(false);
            unitsChosen--;
        }
    }

    public void TueftlerSelected()
    {
        if (tueftler == false)
        {
            if (unitsChosen < 5)
            {
                tueftler = true;
                leuchten10.SetActive(true);
                unitsChosen++;
            }
            else { }
        }
        else
        {
            tueftler = false;
            leuchten10.SetActive(false);
            unitsChosen--;
        }
    }

    //---------------------------------------------------------------------
    //OnClick-Methode für den Bestätigen-Button
    public void SubmitUnitSelection()
    {
        SelectRandomUnits();
        if (unitsChosen == 5)
        {
            SubmitToDatabase();
            Reset();
            ResetSelection();
            unitSelectionScriptObject.SetActive(false);
            if (PassthroughData.currentPlayer == 1)
            {
                PassthroughData.currentPlayer = 2;
                sm.SetPlayer(PassthroughData.player2);
            }
            else
            {
                PassthroughData.currentPlayer = 1;
                sm.SetPlayer(PassthroughData.player1);
            }
            sm.PanelState(true);
        }
    }

    //---------------------------------------------------------------------
    //Methode zum Reset des Leuchtens und der bool-Werte
    public void ResetSelection()
    {        
        leuchten01.SetActive(false);
        leuchten02.SetActive(false);
        leuchten03.SetActive(false);
        leuchten04.SetActive(false);
        leuchten05.SetActive(false);
        leuchten06.SetActive(false);
        leuchten07.SetActive(false);
        leuchten08.SetActive(false);
        leuchten09.SetActive(false);
        leuchten10.SetActive(false);
        boss = false;
        diebin = false;
        meuchelmoerder = false;
        pestarzt = false;
        polizist = false;
        raufbold = false;
        scharfschuetze = false;
        schlaeger = false;
        taschendieb = false;
        tueftler = false;
        bossRnd = false;
        diebinRnd = false;
        meuchelmoerderRnd = false;
        pestarztRnd = false;
        polizistRnd = false;
        raufboldRnd = false;
        scharfschuetzeRnd = false;
        schlaegerRnd = false;
        taschendiebRnd = false;
        tueftlerRnd = false;
        unitsChosen = 0;
    }

    //---------------------------------------------------------------------
    //Methode für die Übertragung der Auswahl an die Datenbank
    public void SubmitToDatabase()
    {
        if (boss == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (1, 'Boss', 'Boss', 3, 5, 3, 1, 5, 4, 1)");
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (2, 'Boss', 'Boss', 3, 5, 3, 1, 5, 4, 2)");
            }
        }
        if (diebin == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (3, 'Diebin', 'Diebin', 3, 3, 2, 2, 3, 3, 1)");
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (4, 'Diebin', 'Diebin', 3, 3, 2, 2, 3, 3, 2)");
            }
        }
        if (meuchelmoerder == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (5, 'Meuchelmoerder', 'Meuchelmoerder', 4, 3, 1, 1, 4, 3, 1)");
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (6, 'Meuchelmoerder', 'Meuchelmoerder', 4, 3, 1, 1, 4, 3, 2)");
            }
        }
        if (pestarzt == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (7, 'Pestarzt', 'Pestarzt', 3, 3, 4, 1, 4, 1, 1)");
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (8, 'Pestarzt', 'Pestarzt', 3, 3, 4, 1, 4, 1, 2)");
            }
        }
        if (polizist == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (9, 'Polizist', 'Polizist', 2, 5, 3, 1, 3, 2, 1)");
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (10, 'Polizist', 'Polizist', 2, 5, 3, 1, 3, 2, 2)");
            }
        }
        if (raufbold == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (11, 'Raufbold', 'Raufbold', 2, 6, 3, 1, 3, 1, 1)");
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (12, 'Raufbold', 'Raufbold', 2, 6, 3, 1, 3, 1, 2)");
            }
        }
        if (scharfschuetze == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (13, 'Scharfschuetze', 'Scharfschuetze', 3, 3, 2, 3, 4, 3, 1)");
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (14, 'Scharfschuetze', 'Scharfschuetze', 3, 3, 2, 3, 4, 3, 2)");
            }
        }
        if (schlaeger == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (15, 'Schlaeger', 'Schlaeger', 2, 4, 2, 1, 2, 2, 1)");
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (16, 'Schlaeger', 'Schlaeger', 2, 4, 2, 1, 2, 2, 2)");
            }
        }
        if (taschendieb == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (17, 'Taschendieb', 'Taschendieb', 4, 2, 1, 1, 2, 1, 1)");
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (18, 'Taschendieb', 'Taschendieb', 4, 2, 1, 1, 2, 1, 2)");
            }
        }
        if (tueftler == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (19, 'Tueftler', 'Tueftler', 2, 2, 1, 2, 5, 5, 1)");
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
                dbc.WriteToDB("Insert Into Einheitentyp (ID, Name, Beschreibung, Aktionspunkte, Lebenspunkte, Verteidigungspunkte, Reichweite, Kosten, Angriffspunkte, SpielerID) Values (20, 'Tueftler', 'Tueftler', 2, 2, 1, 2, 5, 5, 2)");
            }
        }
    }

    void TimeLine()
    {
        if (!paused)
        {
            if (count >= 0)
            {
                
                timerText.GetComponent<Text>().text = count.ToString();
                zeitleiste.transform.localScale += new Vector3(-1 / (timer + 1), 0, 0);
            }
            else if (count == -1)
            {
                SubmitUnitSelection();
                paused = true;
                Reset();
            }
            count--;
        }
    }

    private void Reset()
    {
        CancelInvoke();
        paused = true;
        zeitleiste.transform.localScale = defaultPosition;
        count = timer;
        timerText.GetComponent<Text>().text = timer.ToString();
    }

    public void SelectRandomUnits()
    {
        if (unitsChosen != 5)
        {
            String[] RandomUnits = new String[] { "", "", "", "", "", "", "", "", "", "" };
            for (int j = (10 - unitsChosen); j > 0; j--)
            {
                if (boss == false && bossRnd == false)
                {
                    RandomUnits[j] = "boss";
                    bossRnd = true;
                    continue;
                }
                if (diebin == false && diebinRnd == false)
                {
                    RandomUnits[j] = "diebin";
                    diebinRnd = true;
                    continue;
                }
                if (meuchelmoerder == false && meuchelmoerderRnd == false)
                {
                    RandomUnits[j] = "meuchelmoerder";
                    meuchelmoerderRnd = true;
                    continue;
                }
                if (pestarzt == false && pestarztRnd == false)
                {
                    RandomUnits[j] = "pestarzt";
                    pestarztRnd = true;
                    continue;
                }
                if (polizist == false && polizistRnd == false)
                {
                    RandomUnits[j] = "polizist";
                    polizistRnd = true;
                    continue;
                }
                if (raufbold == false && raufboldRnd == false)
                {
                    RandomUnits[j] = "raufbold";
                    raufboldRnd = true;
                    continue;
                }
                if (scharfschuetze == false && scharfschuetzeRnd == false)
                {
                    RandomUnits[j] = "scharfschuetze";
                    scharfschuetzeRnd = true;
                    continue;
                }
                if (schlaeger == false && schlaegerRnd == false)
                {
                    RandomUnits[j] = "schlaeger";
                    schlaegerRnd = true;
                    continue;
                }
                if (taschendieb == false && taschendiebRnd == false)
                {
                    RandomUnits[j] = "taschendieb";
                    taschendiebRnd = true;
                    continue;
                }
                if (tueftler == false && tueftlerRnd == false)
                {
                    RandomUnits[j] = "tueftler";
                    tueftlerRnd = true;
                    continue;
                }
            }
            bossRnd = false;
            diebinRnd = false;
            meuchelmoerderRnd = false;
            pestarztRnd = false;
            polizistRnd = false;
            raufboldRnd = false;
            scharfschuetzeRnd = false;
            schlaegerRnd = false;
            taschendiebRnd = false;
            tueftlerRnd = false;
            System.Random rnd = new System.Random();
            ArrayList rndlist = new ArrayList();
            for (int i = unitsChosen; i < 5; i++)
            {
                int rndzahl = rnd.Next(1, RandomUnits.Length);

                if (!rndlist.Contains(rndzahl))
                {
                    rndlist.Add(rndzahl);
                }
                else
                {
                    while (rndlist.Contains(rndzahl))
                    {
                        if (!rndlist.Contains(rndzahl))
                        {
                            rndlist.Add(rndzahl);
                        }
                        else
                        {
                            rndzahl = rnd.Next(0, RandomUnits.Length);
                        }
                    }   
                }
               
                String name = Convert.ToString(RandomUnits.GetValue(rndzahl));
                unitsChosen += 1;
                if (name == "boss" && bossRnd == false)
                {
                    boss = true;
                    bossRnd = true;                    
                    continue;
                }
                if (name == "diebin" && diebinRnd == false)
                {
                    diebin = true;
                    diebinRnd = true;                    
                    continue;
                }
                if (name == "meuchelmoerder" && meuchelmoerderRnd == false)
                {
                    meuchelmoerder = true;
                    meuchelmoerderRnd = true;                    
                    continue;
                }
                if (name == "pestarzt" && pestarztRnd == false)
                {
                    pestarzt = true;
                    pestarztRnd = true;                    
                    continue;
                }
                if (name == "polizist" && polizistRnd == false)
                {
                    polizist = true;
                    polizistRnd = true;                   
                    continue;
                }
                if (name == "raufbold" && raufboldRnd == false)
                {
                    raufbold = true;
                    raufboldRnd = true;                    
                    continue;
                }
                if (name == "scharfschuetze" && scharfschuetzeRnd == false)
                {
                    scharfschuetze = true;
                    scharfschuetzeRnd = true;
                    continue;
                }
                if (name == "schlaeger" && schlaegerRnd == false)
                {
                    schlaeger = true;
                    schlaegerRnd = true;
                    continue;
                }
                if (name == "taschendieb" && taschendiebRnd == false)
                {
                    taschendieb = true;
                    taschendiebRnd = true;
                    continue;
                }
                if (name == "tueftler" && tueftlerRnd == false)
                {
                    tueftler = true;
                    tueftlerRnd = true;
                    continue;
                }
                
            }
            rndlist.Clear();
        }
    }
}
