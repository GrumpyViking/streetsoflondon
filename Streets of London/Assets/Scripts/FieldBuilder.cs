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

    private void Start()
    {
        dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 0 + 1 + ",'Bank',0)");
        dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 0 + 2 +  ",'Fabrik',99)");
        dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 0 + 3 + ",'Fabrik',99)");
    }

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
        select = null;
        selectOpposit = null;
        fieldbuild = true;
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].GetComponent<FieldHelper>().isSet == false)
            {
                fieldbuild = false;
            }
        }
        if (fieldbuild)
        {
            for(int i = 0; i < fields.Length; i++)
            {
                fields[i].GetComponent<Outline>().enabled = false;
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
        select.GetComponent<Outline>().enabled = false;
        select.GetComponent<Renderer>().material.mainTexture = fieldImages[index];
        select.GetComponent<FieldHelper>().isSet = true;
        select.GetComponent<Outline>().enabled = true;
        select.GetComponent<Outline>().OutlineColor = Color.green;

        selectOpposit.GetComponent<Outline>().enabled = false;
        selectOpposit.GetComponent<Renderer>().material.mainTexture = fieldImages[index];
        selectOpposit.GetComponent<FieldHelper>().isSet = true;
        selectOpposit.GetComponent<Outline>().enabled = true;
        selectOpposit.GetComponent<Outline>().OutlineColor = Color.green;
        for(int i = 0; i < 2; i++)
        {
            if (index == 0)
            {
                dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 1 + (dbc.NumofFields("Geschäft 1") + 1) + ",'Geschäft 1',2)");
            }
            if (index == 1)
            {
                dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 2 + (dbc.NumofFields("Geschäft 2") + 1) + ",'Geschäft 2',2)");

            }
            if (index == 2)
            {
                dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 3 + (dbc.NumofFields("Strasse") + 1) + ",'Strasse',2)");

            }
            if (index == 3)
            {
                dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 4 + (dbc.NumofFields("Gasse") + 1) + ",'Gasse',3)");

            }
            if (index == 4)
            {
                dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 5 + (dbc.NumofFields("Park") + 1) + ",'Park',1)");
            }
            if (index == 5)
            {
                dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 6 + (dbc.NumofFields("Dach") + 1) + ",'Dach',4)");
            }
            if (index == 6)
            {
                dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 7 + (dbc.NumofFields("Turm") + 1) + ",'Turm',99)");
            }
        }
        Reset();
    }

}
