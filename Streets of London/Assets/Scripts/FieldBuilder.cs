using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FieldBuilder : MonoBehaviour {
    public GameObject fieldbuildermenu;
    public SpielerMenu sm;
    public GameObject playerText;
    public GameObject zeitleiste;
    public GameObject timerText;
    public float timer;
    public DataBaseController dbc;

    float count;
    bool timerstate;
    Vector3 defaultPosition;

    void Initialise()
    {
        Debug.Log(PassthrougData.currentPlayer);
        playerText.GetComponent<Text>().text = dbc.GetName(PassthrougData.currentPlayer);
        defaultPosition = zeitleiste.transform.localScale;
        count = timer;
        timerstate = true;
        InvokeRepeating("TimeLine", 1.0f,1.0f);

    }

    public void PanelState(bool state)
    {
        fieldbuildermenu.SetActive(state);
        Initialise();
    }

    void TimeLine()
    {
        if (timerstate)
        {
            if (count > 0)
            {
                timerText.GetComponent<Text>().text = count.ToString();
                zeitleiste.transform.localScale += new Vector3(-1 / (timer + 1), 0, 0);
            }
            else if (count == 0)
            {
                timerstate = false;
                Reset();
            }
            count--;
        }
        
        Debug.Log(count);
    }

    private void Reset()
    {
        zeitleiste.transform.localScale = defaultPosition;
    }
}
