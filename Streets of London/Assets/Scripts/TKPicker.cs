using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TKPicker : MonoBehaviour {

    public int anzahlTK;
    public string gewaehlteTK;
    public string nameTK;
    public string wirkungTK;
    public string[] poolTK = {"Fusel", "Infektion", "Verstärkter Mantel", "Ration"};
    public static string[] player1TK = {" ", " ", " ", " ", " ", " ", " ", " "};
    public static string[] player2TK = {" ", " ", " ", " ", " ", " ", " ", " "};

    public GameObject anzeigeName1;
    public GameObject anzeigeWirkung1;
    public GameObject anzeigeName2;
    public GameObject anzeigeWirkung2;
    public GameObject anzeigeName3;
    public GameObject anzeigeWirkung3;

    public GameObject aufleuchtenTK1;
    public GameObject aufleuchtenTK2;
    public GameObject aufleuchtenTK3;

    public GameObject TKPickerScriptObject;
    
    public void OeffnePickerMenu(int gekaufteTK)
    {
        TKPickerScriptObject.SetActive(true);
        anzahlTK = gekaufteTK;
        RandomiseTKChoice();
    }

    public void RandomiseTKChoice()
    {
        System.Random rnd = new System.Random();
        InitializeTKDetails(poolTK[rnd.Next(3)]);
        ShowSlot1();
        InitializeTKDetails(poolTK[rnd.Next(3)]);
        ShowSlot2();
        InitializeTKDetails(poolTK[rnd.Next(3)]);
        ShowSlot3();
    }

    public void InitializeTKDetails(string name)
    {
        switch (name)
        {
            case "Fusel":
                nameTK = name;
                wirkungTK = "Erhöhe den AW einer Einheit um 1 für 2 Runden.";
                break;
            case "Infektion":
                nameTK = name;
                wirkungTK = "Alle gegnerischen Einheiten im Wirkungsbereich verlieren 2 LP.";
                break;
            case "Verstärkter Mantel":
                nameTK = name;
                wirkungTK = "Erhöhe den VW einer Einheit um 1 für 2 Runden";
                break;
            case "Ration":
                nameTK = name;
                wirkungTK = "Heile alle Verbündeten im Wirkungsbereich um 2 LP.";
                break;
            case "Sabotage":
                nameTK = name;
                wirkungTK = "Beende den Effekt einer beliebigen, aktiven Trickkarte (auch Sabotage).";
                break;
            case "Investition":
                nameTK = name;
                wirkungTK = "Erhalte 3 Gold pro Runde für 3 Runden. Nach jeder Runde wird ein Schadensmarker als Rundenzähler auf die Karte gelegt.";
                break;
            case "Verführung":
                nameTK = name;
                wirkungTK = "Übernimm für eine Runde die Kontrolle über eine beliebige gegnerische Einheit.";
                break;
            case "Verstärkung":
                nameTK = name;
                wirkungTK = "Platziere einen Schläger an einer beliebigen Stelle auf deiner Spielfeldseite.";
                break;
        }
    }

    //Anzeige der Kartendetails in den einzelnen Slots
    public void ShowSlot1()
    {
        anzeigeName1.GetComponent<Text>().text = nameTK;
        anzeigeWirkung1.GetComponent<Text>().text = wirkungTK;
    }

    public void ShowSlot2()
    {
        anzeigeName2.GetComponent<Text>().text = nameTK;
        anzeigeWirkung2.GetComponent<Text>().text = wirkungTK;
    }

    public void ShowSlot3()
    {
        anzeigeName3.GetComponent<Text>().text = nameTK;
        anzeigeWirkung3.GetComponent<Text>().text = wirkungTK;
    }
    //----------------------------------------------------------------------------------------------
    //Auswahl der Kartenslots (OnClick-Methoden)
    public void ChooseCard1()
    {
        aufleuchtenTK1.SetActive(true);
        aufleuchtenTK2.SetActive(false);
        aufleuchtenTK3.SetActive(false);
        gewaehlteTK = anzeigeName1.GetComponent<Text>().text;
    }

    public void ChooseCard2()
    {
        aufleuchtenTK1.SetActive(false);
        aufleuchtenTK2.SetActive(true);
        aufleuchtenTK3.SetActive(false);
        gewaehlteTK = anzeigeName2.GetComponent<Text>().text;
    }

    public void ChooseCard3()
    {
        aufleuchtenTK1.SetActive(false);
        aufleuchtenTK2.SetActive(false);
        aufleuchtenTK3.SetActive(true);
        gewaehlteTK = anzeigeName1.GetComponent<Text>().text;
    }

    public void SubmitChoice()
    {
        if (gewaehlteTK != null)
        {
            if (PassthrougData.currentPlayer == 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (player1TK[i] == " ")
                    {
                        player1TK[i] = gewaehlteTK;
                        Debug.Log("Spieler 1 kriegt " + gewaehlteTK);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (player2TK[i] == " ")
                    {
                        player2TK[i] = gewaehlteTK;
                        Debug.Log("Spieler 2 kriegt " + gewaehlteTK);
                        break;
                    }
                }
            }
            aufleuchtenTK1.SetActive(false);
            aufleuchtenTK2.SetActive(false);
            aufleuchtenTK3.SetActive(false);
            TKPickerScriptObject.SetActive(false);
            anzahlTK--;
            gewaehlteTK = null;
            if (anzahlTK != 0)
            {
                OeffnePickerMenu(anzahlTK);
            }
        }
    }


}
