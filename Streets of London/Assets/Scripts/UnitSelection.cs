using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelection : MonoBehaviour {

    public GameObject unitSelectionScriptObject;
    public GameObject playerTextSpielerMenu;
    public GameObject playerTextEinheitenAuswahl;

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

    private int unitsChosen = 0;

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

    public void Auswahl()
    {
        unitSelectionScriptObject.SetActive(true);
        playerTextEinheitenAuswahl.GetComponent<Text>().text = playerTextSpielerMenu.GetComponent<Text>().text;
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
        if (boss == true)
        {
            //Hier kommt die Methode hin um den Einheitentyp der Datenbank hinzuzufügen
        }
        if (diebin == true)
        {
            //Hier kommt die Methode hin um den Einheitentyp der Datenbank hinzuzufügen
        }
        if (meuchelmoerder == true)
        {
            //Hier kommt die Methode hin um den Einheitentyp der Datenbank hinzuzufügen
        }
        if (pestarzt == true)
        {
            //Hier kommt die Methode hin um den Einheitentyp der Datenbank hinzuzufügen
        }
        if (polizist == true)
        {
            //Hier kommt die Methode hin um den Einheitentyp der Datenbank hinzuzufügen
        }
        if (raufbold == true)
        {
            //Hier kommt die Methode hin um den Einheitentyp der Datenbank hinzuzufügen
        }
        if (scharfschuetze == true)
        {
            //Hier kommt die Methode hin um den Einheitentyp der Datenbank hinzuzufügen
        }
        if (schlaeger == true)
        {
            //Hier kommt die Methode hin um den Einheitentyp der Datenbank hinzuzufügen
        }
        if (taschendieb == true)
        {
            //Hier kommt die Methode hin um den Einheitentyp der Datenbank hinzuzufügen
        }
        if (tueftler == true)
        {
            //Hier kommt die Methode hin um den Einheitentyp der Datenbank hinzuzufügen
        }
    }
}
