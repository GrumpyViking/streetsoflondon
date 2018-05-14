using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelection : MonoBehaviour {

    //Spiel-Objekte
    public GameObject unitSelectionScriptObject;
    public GameObject playerMenuScriptObject;
    public GameObject playerTextSpielerMenu;
    public GameObject playerTextEinheitenAuswahl;
    public GameManager gm;
    public SpielerMenu sm;        

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
        SubmitToDatabase();
        ResetSelection();
        if (finish == true)
        {
            if (side == 0)
            {
                side = 1;
                sm.SetPlayer(PassthrougData.player2);
            }
            else
            {
                side = 0;
                sm.SetPlayer(PassthrougData.player1);
            }
            sm.PanelState(true);
            unitSelectionScriptObject.SetActive(false);
        }
        else
        {
            finish = true;
            if (side == 0)
            {
                side = 1;
                sm.SetPlayer(PassthrougData.player2);
            }
            else
            {
                side = 0;
                sm.SetPlayer(PassthrougData.player1);
            }
            playerTextEinheitenAuswahl.GetComponent<Text>().text = playerTextSpielerMenu.GetComponent<Text>().text;
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
        if (boss == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
            }
        }
        if (diebin == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
            }
        }
        if (meuchelmoerder == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
            }
        }
        if (pestarzt == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
            }
        }
        if (polizist == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
            }
        }
        if (raufbold == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
            }
        }
        if (scharfschuetze == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
            }
        }
        if (schlaeger == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
            }
        }
        if (taschendieb == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
            }
        }
        if (tueftler == true)
        {
            if (side == 0)
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den ersten Spieler hinzuzufügen
            }
            else
            {
                //Hier kommt die Methode hin um den Einheitentyp der Datenbank für den zweiten Spieler hinzuzufügen
            }
        }
    }
}
