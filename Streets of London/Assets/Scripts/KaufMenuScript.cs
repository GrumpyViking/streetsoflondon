using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KaufMenuScript : MonoBehaviour
{
    public GameObject kaufMenuScriptObject;
    public GameObject anzeige1;

    public void OeffneKaufmenue()
    {
        kaufMenuScriptObject.SetActive(true);
    }

    public void SchließeKaufmenu()
    {
        kaufMenuScriptObject.SetActive(false);
    }

    public void ErhoeheAnzeige1()
    {
        /*
        int x = Convert.ToInt32();
        String text1 = Convert.ToString(x++);
        anzeige1.GetComponent<UnityEngine.UI.Text>().text = text1;
        */
    }
}

