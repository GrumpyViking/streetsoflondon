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
    bool chooseField=false;
    Vector3 defaultPosition;
    GameObject select;

    void Initialise()
    {
        if (isActiveAndEnabled == true)
        {
            Debug.Log(PassthrougData.currentPlayer);
            playerText.GetComponent<Text>().text = dbc.GetName(PassthrougData.currentPlayer);
            defaultPosition = zeitleiste.transform.localScale;
            count = timer;
            timerstate = true;
            chooseField = true;
            InvokeRepeating("TimeLine", 1.0f, 1.0f);
        }
        
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
                Reset();
            }
            count--;
        }
        
        Debug.Log(count);
    }

    private void Reset()
    {
        zeitleiste.transform.localScale = defaultPosition;
        timerstate = false;
        chooseField = false;
        CancelInvoke();
        EndTurn();
    }

    void EndTurn()
    {
        if(PassthrougData.currentPlayer == 1)
        {
            PassthrougData.currentPlayer = 2;
            sm.SetPlayer(PassthrougData.player2);
        }
        else
        {
            PassthrougData.currentPlayer = 1;
            sm.SetPlayer(PassthrougData.player1);
        }
        PanelState(false);
        sm.PanelState(true);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && chooseField)
        {
            SelectFeld();
            Debug.Log("Pasta");
        }
    }

    void SelectFeld()
    {
        RaycastHit hitInfo;
        select = ReturnClickedObject(out hitInfo);
        if (select.tag == "HexFields")
        {
            select.GetComponent<Outline>().OutlineColor = Color.red;
            select.GetComponent<Outline>().enabled = true;
        }
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
            Debug.Log(target);
        }
        return target;
    }

}
