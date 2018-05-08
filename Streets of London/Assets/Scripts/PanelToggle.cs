using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggle : MonoBehaviour {

    void ChangeState(GameObject panel, bool state)
    {
        if (state)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }

}
