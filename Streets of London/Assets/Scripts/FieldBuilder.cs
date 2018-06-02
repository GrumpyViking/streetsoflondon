using System;
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
        //Prüft bei jedem neuen Frame ob die Maustaste betätigt wurde wenn kein Feldgewählt wurde
        if (Input.GetMouseButtonDown(0) && chooseField)
        {
            SelectFeld();
        }
    }

    //Auswahl des Feldes und des entsprechenden gegenüberliegenden Feld
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
            //Auswahl des gegenüberliegenden Feldes
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

    //Liefert nach Mausklick ein Objekt zurück
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
        int id;

        //Setzt GeländefeldBild auf ausgewähltes Feld
        select.GetComponent<Outline>().enabled = false;
        select.GetComponent<Renderer>().material.mainTexture = fieldImages[index];
        select.GetComponent<FieldHelper>().isSet = true;
        select.GetComponent<Outline>().enabled = true;
        select.GetComponent<Outline>().OutlineColor = Color.green;

        //Setzt GeländefeldBild auf das gegenüberliegende Feld
        selectOpposit.GetComponent<Outline>().enabled = false;
        selectOpposit.GetComponent<Renderer>().material.mainTexture = fieldImages[index];
        selectOpposit.GetComponent<FieldHelper>().isSet = true;
        selectOpposit.GetComponent<Outline>().enabled = true;
        selectOpposit.GetComponent<Outline>().OutlineColor = Color.green;

        //Schreibt das Feld in die Datenbank
        for (int i = 0; i < 2; i++)
        {
            if (index == 0)
            {
                if (i == 0)
                {
                    id = Convert.ToInt32("1" + Convert.ToString(dbc.NumofFields("Geschäft 1") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Geschäft 1',2)");
                    select.GetComponent<FieldHelper>().id = id;
                }
                else
                {
                    id = Convert.ToInt32("1" + Convert.ToString(dbc.NumofFields("Geschäft 1") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Geschäft 1',2)");
                    selectOpposit.GetComponent<FieldHelper>().id = id;
                }
            }
            if (index == 1)
            {
                if (i == 0)
                {
                    id = Convert.ToInt32("2" + Convert.ToString(dbc.NumofFields("Geschäft 2") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Geschäft 2',2)");
                    select.GetComponent<FieldHelper>().id = id;
                }
                else
                {
                    id = Convert.ToInt32("2" + Convert.ToString(dbc.NumofFields("Geschäft 2") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Geschäft 2',2)");
                    selectOpposit.GetComponent<FieldHelper>().id = id;
                }
            }
            if (index == 2)
            {
                if (i == 0)
                {
                    id = Convert.ToInt32("3" + Convert.ToString(dbc.NumofFields("Strasse") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Strasse',2)");
                    select.GetComponent<FieldHelper>().id = id;
                }
                else
                {
                    id = Convert.ToInt32("3" + Convert.ToString(dbc.NumofFields("Strasse") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Strasse',2)");
                    selectOpposit.GetComponent<FieldHelper>().id = id;
                }
            }
            if (index == 3)
            {
                if (i == 0)
                {
                    id = Convert.ToInt32("4" + Convert.ToString(dbc.NumofFields("Gasse") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Gasse',3)");
                    select.GetComponent<FieldHelper>().id = id;
                }
                else
                {
                    id = Convert.ToInt32("4" + Convert.ToString(dbc.NumofFields("Gasse") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Gasse',3)");
                    selectOpposit.GetComponent<FieldHelper>().id = id;
                }
            }
            if (index == 4)
            {
                if (i == 0)
                {
                    id = Convert.ToInt32("5" + Convert.ToString(dbc.NumofFields("Park") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Park',1)");
                    select.GetComponent<FieldHelper>().id = id;
                }
                else
                {
                    id = Convert.ToInt32("5" + Convert.ToString(dbc.NumofFields("Park") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Park',1)");
                    selectOpposit.GetComponent<FieldHelper>().id = id;
                }
            }
            if (index == 5)
            {
                if (i == 0)
                {
                    id = Convert.ToInt32("6" + Convert.ToString(dbc.NumofFields("Dach") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Dach',4)");
                    select.GetComponent<FieldHelper>().id = id;
                }
                else
                {
                    id = Convert.ToInt32("6" + Convert.ToString(dbc.NumofFields("Dach") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Dach',4)");
                    selectOpposit.GetComponent<FieldHelper>().id = id;
                }
            }
            if (index == 6)
            {
                if (i == 0)
                {
                    id = Convert.ToInt32("7" + Convert.ToString(dbc.NumofFields("Turm") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Turm',99)");
                    select.GetComponent<FieldHelper>().id = id;
                }
                else
                {
                    id = Convert.ToInt32("7" + Convert.ToString(dbc.NumofFields("Turm") + 1));
                    dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + id + ",'Turm',99)");
                    selectOpposit.GetComponent<FieldHelper>().id = id;
                }
            }
        }
        Reset();
    }
}
