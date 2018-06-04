using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="neue Trickkarte", menuName ="Trickkarte" )]
public class Trickkarten : MonoBehaviour {

    public string nameTK;
    public string wirkungTK;
 
    public Sprite wirkungsbereich;
    public Sprite trickkartenbild;

    public GameObject trickkartenMenu;

    public void OeffneTrickkartenMenu()
    {
        trickkartenMenu.SetActive(true);
    }

    public void SchliesseTrickkartenMenu()
    {
        trickkartenMenu.SetActive(false);
    }

    //-------------------------------------------------------------------------
    //Anzeige der Kartendetails
    public void initializeTKDetails(string name)
    {
        switch (name)
        {
            case "Fusel":
                nameTK = name;
                wirkungTK = "Erhöhe den AW einer EInheit um 1 für 2 Runden.";
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

	}

