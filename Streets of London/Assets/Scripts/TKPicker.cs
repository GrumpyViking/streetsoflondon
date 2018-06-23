using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TKPicker : MonoBehaviour {

    public int anzahlTK;            //Attribut für die Speicherung der Anzahl der im Kaufmenü gekauften Trickkarten
    public string gewaehlteTK;      //Attribut für die Nummer des ausgewählten Picker-Slots

    //Temp-Speicher für die Anzeige der Trickkartendetails
    public string nameTK;
    public string wirkungTK;
    public Sprite bildTemp;


    public string[] poolTK = {"Fusel", "Infektion", "Verstärkter Mantel", "Ration", "Investition" };        //Pool der vorhandenen Trickkarten-Namen

    public static string[] player1TK = {" ", " ", " ", " ", " ", " ", " ", " "};        //Array für die Speicherung der Trickkarten im Besitz von Spieler 1
    public static string[] player2TK = {" ", " ", " ", " ", " ", " ", " ", " "};        //Array für die Speicherung der Trickkarten im Besitz von Spieler 2

    //Game-Object bzw. Image-Object für die Anzeige der Trickkartendetails im Picker-Slot 1
    public GameObject anzeigeName1;
    public GameObject anzeigeWirkung1;
    public Image anzeigeBild1;

    //Game-Object bzw. Image-Object für die Anzeige der Trickkartendetails im Picker-Slot 2
    public GameObject anzeigeName2;
    public GameObject anzeigeWirkung2;
    public Image anzeigeBild2;

    //Game-Object bzw. Image-Object für die Anzeige der Trickkartendetails im Picker-Slot 3
    public GameObject anzeigeName3;
    public GameObject anzeigeWirkung3;
    public Image anzeigeBild3;

    //Speicher-Attribute für die Bilder der jeweiligen Trickkarten-Bilder
    public Sprite bildInvestition;
    public Sprite bildDoppelbock;
    public Sprite bildMantel;
    public Sprite bildInfektion;
    public Sprite bildRation;

    //GameObjects der Aufleuchten-Objekte für die einzelnen PickerSlots
    public GameObject aufleuchtenTK1;
    public GameObject aufleuchtenTK2;
    public GameObject aufleuchtenTK3;

    public GameObject TKPickerScriptObject;     //GameObject des Trickkarten-Auswahl-Menüs

    //Methode zum Öffnen des Trickkartenmenüs und Speichern der Anzahl der gekauften Trickkarten
    public void OeffnePickerMenu(int gekaufteTK)
    {
        TKPickerScriptObject.SetActive(true);
        anzahlTK = gekaufteTK;
        RandomiseTKChoice();
    }

    //Zufällige Auswahl der Trickkarten und Aufruf der Methoden zur Anzeige dieser
    public void RandomiseTKChoice()
    {
        System.Random rnd = new System.Random();
        InitializeTKDetails(poolTK[rnd.Next(5)]);
        ShowSlot1();
        InitializeTKDetails(poolTK[rnd.Next(5)]);
        ShowSlot2();
        InitializeTKDetails(poolTK[rnd.Next(5)]);
        ShowSlot3();
    }

    //Initialisierung der Trickarten-Details anhand des übergebenen Trickkarten-Namens
    public void InitializeTKDetails(string name)
    {
        switch (name)
        {
            case "Fusel":
                nameTK = name;
                wirkungTK = "Erhöhe den AW deiner Einheiten um 1 für die nächste Runde.";
                bildTemp = bildDoppelbock;
                break;
            case "Infektion":
                nameTK = name;
                wirkungTK = "Alle gegnerischen Einheiten verlieren 1 LP. Einheiten können nicht durch diesen Effekt sterben.";
                bildTemp = bildInfektion;
                break;
            case "Verstärkter Mantel":
                nameTK = name;
                wirkungTK = "Erhöhe den VW deiner Einheiten um 1 für die nächste Runde.";
                bildTemp = bildMantel;
                break;
            case "Ration":
                nameTK = name;
                wirkungTK = "Erhöhe die LP deiner aktiven Einheiten um 1 LP.";
                bildTemp = bildRation;
                break;
            case "Investition":
                nameTK = name;
                wirkungTK = "Erhalte 3 Gold pro Runde für 3 Runden.";
                bildTemp = bildInvestition;
                break;
        }
    }

    //Anzeige der Trickkartendetails im ersten Slot
    public void ShowSlot1()
    {
        anzeigeName1.GetComponent<Text>().text = nameTK;
        anzeigeWirkung1.GetComponent<Text>().text = wirkungTK;        
        anzeigeBild1.sprite = bildTemp;
    }

    //Anzeige der Trickkartendetails im zweiten Slot
    public void ShowSlot2()
    {
        anzeigeName2.GetComponent<Text>().text = nameTK;
        anzeigeWirkung2.GetComponent<Text>().text = wirkungTK;
        anzeigeBild2.sprite = bildTemp;
    }

    //Anzeige der Trickkartendetails im dritten Slot
    public void ShowSlot3()
    {
        anzeigeName3.GetComponent<Text>().text = nameTK;
        anzeigeWirkung3.GetComponent<Text>().text = wirkungTK;
        anzeigeBild3.sprite = bildTemp;
    }

    //Auswahl des ersten Kartenslots
    public void ChooseCard1()
    {
        aufleuchtenTK1.SetActive(true);
        aufleuchtenTK2.SetActive(false);
        aufleuchtenTK3.SetActive(false);
        gewaehlteTK = anzeigeName1.GetComponent<Text>().text;
    }

    //Auswahl des zweiten Kartenslots
    public void ChooseCard2()
    {
        aufleuchtenTK1.SetActive(false);
        aufleuchtenTK2.SetActive(true);
        aufleuchtenTK3.SetActive(false);
        gewaehlteTK = anzeigeName2.GetComponent<Text>().text;
    }

    //Auswahl des dritten Kartenslots
    public void ChooseCard3()
    {
        aufleuchtenTK1.SetActive(false);
        aufleuchtenTK2.SetActive(false);
        aufleuchtenTK3.SetActive(true);
        gewaehlteTK = anzeigeName3.GetComponent<Text>().text;
    }

    //Bestätigung der Auswahl und Speicherung der Trickkarte im Array des jeweiligen Spielers
    public void SubmitChoice()
    {
        if (gewaehlteTK != null)
        {
            if (PassthroughData.currentPlayer == 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (player1TK[i] == " ")
                    {
                        player1TK[i] = gewaehlteTK;
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
