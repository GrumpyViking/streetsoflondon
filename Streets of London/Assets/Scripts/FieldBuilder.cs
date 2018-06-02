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
    public GameObject[] fields;
    public float timer;
    public DataBaseController dbc;
    public Texture[] fieldImages;

    float count;
    bool timerstate;
    bool fieldbuild;
    bool chooseField=false;
    Vector3 defaultPosition;
    GameObject select;
    GameObject selectOpposit;

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
        
        select.GetComponent<Outline>().OutlineColor = Color.green;
        selectOpposit.GetComponent<Outline>().OutlineColor = Color.green;
        fieldbuild = true;
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].GetComponent<FieldHelper>().isSet == false)
            {
                fieldbuild = false;
            }
        }
        PanelState(false);
        sm.SetFieldBuild(fieldbuild);
        sm.PanelState(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && chooseField)
        {
            SelectFeld();
        }
    }

    void SelectFeld()
    {
        RaycastHit hitInfo;
        int pos = 0;
        select = ReturnClickedObject(out hitInfo);
        if (select.tag == "HexFields" && !select.GetComponent<FieldHelper>().isSet)
        {
            for(int i = 0; i < fields.Length; i++)
            {
                if(fields[i] == select)
                {
                    pos = i;
                }
            }
            select.GetComponent<Outline>().OutlineColor = Color.red;
            select.GetComponent<Outline>().enabled = true;
            if (pos < 24)
            {
                selectOpposit = fields[pos+23];
            }
            else
            {
                selectOpposit = fields[pos - 23];
            }
            selectOpposit.GetComponent<Outline>().enabled = true;
            chooseField = false;
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

    public void SetFieldImage(int index)
    {
        Debug.Log(fieldImages[index]);
        select.GetComponent<Renderer>().material.mainTexture = fieldImages[index];
        select.GetComponent<FieldHelper>().isSet = true;
        selectOpposit.GetComponent<Renderer>().material.mainTexture = fieldImages[index];
        selectOpposit.GetComponent<FieldHelper>().isSet = true;
        Reset();
    }

}
