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
    private int unitsChosen = 0;            //Counter der gewaehlten Einheiten
    private int side;                       //Angabe des aktuellen Spielers

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


    private void Start()
    {
        count = timer;
        InvokeRepeating("TimeLine", 1.0f, 1.0f);
    }

    //---------------------------------------------------------------------
    //Methode zum Aufrufen der ersten Auswahl
    public void Auswahl()
    {
        unitSelectionScriptObject.SetActive(true);
        playerTextEinheitenAuswahl.GetComponent<Text>().text = playerTextSpielerMenu.GetComponent<Text>().text;
        side = PassthrougData.startPlayer;
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
        if(unitsChosen == 5)
        {
            Reset();
            SubmitToDatabase();
            ResetSelection();
            unitSelectionScriptObject.SetActive(false);
            if ((PassthrougData.startPlayer = 1 - side) == 0)
            {
                sm.SetPlayer(PassthrougData.player1);
            }
            else
            {
                sm.SetPlayer(PassthrougData.player2);
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
        unitsChosen = 0;
    }

    //---------------------------------------------------------------------
    //Methode für die Übertragung der Auswahl an die Datenbank
    public void SubmitToDatabase()
    {
        Debug.Log("Aktueller Spieler: " + side);
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
        if (count >= 0)
        { 
            timerText.GetComponent<Text>().text = count.ToString();
            zeitleiste.transform.localScale += new Vector3(-1 / (timer + 1), 0, 0);
        }
        else if (count == -1)
        {
            SubmitUnitSelection();
            
        }
        count--;
    }

    private void Reset()
    {
        zeitleiste.transform.localScale += new Vector3(1 , 0, 0);
        count = timer;
    }
}
