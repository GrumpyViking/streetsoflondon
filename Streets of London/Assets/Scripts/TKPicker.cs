using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TKPicker : MonoBehaviour {

    public int anzahlTK;
    public string nameTK;
    public string wirkungTK;
    public string[] poolTK = {"Fusel", "Infektion", "Verstärkter Mantel", "Ration"};

    public GameObject anzeigeName1;
    public GameObject anzeigeWirkung1;
    public GameObject anzeigeName2;
    public GameObject anzeigeWirkung2;
    public GameObject anzeigeName3;
    public GameObject anzeigeWirkung3;

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
        }
    }

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
}
