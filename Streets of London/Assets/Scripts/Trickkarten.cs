using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="neue Trickkarte", menuName ="Trickkarte" )]
public class Trickkarten : MonoBehaviour {

    public string trickkartenname;
    public string wirung;
    public int preis;

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


	}

