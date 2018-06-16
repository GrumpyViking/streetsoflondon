using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * FieldBuilder Skript:
 * 
 * Beinhaltet alles für den Spielfeldaufbau.
 * Die Spieler haben abwechselnd 10 Sekunden zeit ein Spielfeld zu setzen.
 * Entsprechend der auswahl wird auf der anderen Spielfeldseite das gegerüberliegende Feld gesetzt.
 * Dadurch wird die Balance erhalten.
 * 
 */ 

public class FieldBuilder : MonoBehaviour {

    //Skripte die genutzt werden
    
    public SpielerMenu sm;
    public DataBaseController dbc;

    //Beeinflussbare Spielobjekte
    public GameObject fieldBuilderMenu;
    public GameObject playerText;
    public GameObject timeLine;
    public GameObject timerText;
    public GameObject anzahlTurm;
    public GameObject anzahlDach;
    public GameObject anzahlGeschaeft1;
    public GameObject anzahlGeschaeft2;
    public GameObject buttonTurm;
    public GameObject buttonDach;
    public GameObject buttonGeschaeft1;
    public GameObject buttonGeschaeft2;

    //Array mit allen Spielfeldern
    public GameObject[] fields;

    //Texture Arrays die die Unterschiedlichen Geländetexturen bereithalten
    public Texture[] fieldImages;
    public Texture[] staticTextures;

    //Timer Relevante variablen
    float count;
    public float timer;
    Vector3 defaultPosition;
    bool timerState;

    //Statuswerte
    bool fieldBuild;
    bool chooseField=false;
    
    //Hilfsvariablen für das gewählte Geländefeld und das gegenüberliegende feld
    GameObject select;
    GameObject selectOpposit;

    //Schreibt die beiden Geländefelder sowie die Bank in die Datenbank und setzt die Texturen
    private void Start()
    {
        dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 0 + 1 + ",'Bank',0)");
        dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 0 + 2 +  ",'Fabrik',99)");
        dbc.WriteToDB("INSERT INTO Gelaendefelder(ID, Name, Bonus) VALUES(" + 0 + 3 + ",'Fabrik',99)");
        fields[0].GetComponent<Renderer>().material.mainTexture = staticTextures[0];
        fields[0].GetComponent<FieldHelper>().id = 2;
        fields[23].GetComponent<Renderer>().material.mainTexture = staticTextures[0];
        fields[23].GetComponent<FieldHelper>().id = 3;
        fields[22].GetComponent<Renderer>().material.mainTexture = staticTextures[1];
        fields[22].GetComponent<FieldHelper>().id = 1;
    }

    void Initialise()
    {
        //Wenn das Menu aktiviert wird wird der Aktuelle Spieler dargestellt und der Timer Gestartet
        if (isActiveAndEnabled == true)
        {
            playerText.GetComponent<Text>().text = dbc.GetName(PassthroughData.currentPlayer);
            defaultPosition = timeLine.transform.localScale;
            count = timer;
            timerState = true;
            chooseField = true;
            //InvokeRepeating führt nach 1 Sekunde jede Sekunde die funktion TimeLine aus
            InvokeRepeating("TimeLine", 1.0f, 1.0f);
        }
    }

    //Ein- und Ausblenden des Menus
    public void PanelState(bool state)
    {
        fieldBuilderMenu.SetActive(state);
        Initialise();
    }

    //TimeLine funktion beinhaltet den Timer der die gegebene Zeitspanne(10s) ablaufen lässt
    void TimeLine()
    {
        if (timerState)
        {
            if (count > 0)
            {
                timerText.GetComponent<Text>().text = count.ToString();
                timeLine.transform.localScale += new Vector3(-1 / (timer + 1), 0, 0);
            }
            else if (count == 0)
            {
                Reset();
            }
            count--;
        }
    }

    //Setzt den Timer zurück 
    private void Reset()
    {
        timeLine.transform.localScale = defaultPosition;
        timerState = false;
        chooseField = false;
        CancelInvoke();
        EndTurn();
    }

    //Beendet den aktuellen Zug wenn Timer abgelaufen ist oder ein feld gesetzt wurde
    void EndTurn()
    {
        select = null;
        selectOpposit = null;

        fieldBuild = true;
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].GetComponent<FieldHelper>().isSet == false)
            {
                fieldBuild = false;
            }
        }

        if (fieldBuild)
        {
            for(int i = 0; i < fields.Length; i++)
            {
                fields[i].GetComponent<Outline>().enabled = false;
                
            }
        }
        else
        {
            if (PassthroughData.currentPlayer == 1)
            {
                PassthroughData.currentPlayer = 2;
                sm.SetPlayer(PassthroughData.player2);
            }
            else
            {
                PassthroughData.currentPlayer = 1;
                sm.SetPlayer(PassthroughData.player1);
            }
        }

        PanelState(false);
        sm.SetFieldBuild(fieldBuild);
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
        else
        {

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

        if (index == 0)
        {
            anzahlGeschaeft1.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzahlGeschaeft1.GetComponent<Text>().text) - 2);
            if (Convert.ToInt32(anzahlGeschaeft1.GetComponent<Text>().text) == 0)
            {
                buttonGeschaeft1.SetActive(false);
            }
        }
        if (index == 1)
        {
            anzahlGeschaeft2.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzahlGeschaeft2.GetComponent<Text>().text) - 2);
            if (Convert.ToInt32(anzahlGeschaeft2.GetComponent<Text>().text) == 0)
            {
                buttonGeschaeft2.SetActive(false);
            }
        }
        if (index == 5)
        {
            anzahlDach.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzahlDach.GetComponent<Text>().text) - 2);
            if (Convert.ToInt32(anzahlDach.GetComponent<Text>().text) == 0)
            {
                buttonDach.SetActive(false);
            }
        }
        if (index == 6)
        {
            anzahlTurm.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzahlTurm.GetComponent<Text>().text) - 2);
            if (Convert.ToInt32(anzahlTurm.GetComponent<Text>().text) == 0)
            {
                buttonTurm.SetActive(false);
            }
        }

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

    //Funktion für Testzwecke füllt das feld zufällig auf
    public void AutoComplete()
    {
        for(int i = 1; i < 22; i++)
        {
            select = fields[i];
            selectOpposit = fields[i + 23];
            SetFieldImage(UnityEngine.Random.Range(0, 7));
        }
    }
}
