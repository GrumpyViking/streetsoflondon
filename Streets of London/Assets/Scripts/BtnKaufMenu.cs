using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnKaufMenu : MonoBehaviour
{
    public GameObject kaufMenuScriptObject;

    public static void OeffneKaufmenue()
    {
        kaufMenuScriptObject.SetActive(true);
    }
}

