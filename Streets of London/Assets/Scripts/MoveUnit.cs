using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveUnit : MonoBehaviour
{
    GameObject select;
    GameObject unit;
    public Ressources rm;
    GameObject gegner;
    public Texture[] fabrikDamage; 
    GameObject feld;
    GameObject fabrik;
    GameObject aktionsmenue;
    public GameObject[] grid;
    public Fabrik fb;

    Unit test;
    public GameObject anweisungText;
    public GameObject bewegenButtonText;
    public GameObject angriffButtonText;
    public GameObject bewegenButton;
    public GameObject angriffButton;
    public GameObject beweglicheEinheit;
    public KampfMenu km;
    public DataBaseController dbc;
    public GameManager gm;

    public GameObject nametext;
    public GameObject aptext;
    public GameObject lptext;
    public GameObject attext;
    public GameObject dftext;
    public GameObject rwtext;

    public GameObject gewinner;
    
    bool unitselected = false;
    bool feldselected = false;
    bool zielfeldsuche = false;
    bool buttonclicked = false;
    bool waehlegegner = false;
    bool waehlefabrik = false;
    bool gegnergewaehlt = false;
    int phase = 0;


    private void Start()
    {
        aktionsmenue = GameObject.Find("UI/Panels/Aktionsmenue");
    }

    private void Update()
    {
        if (PassthrougData.gameactiv)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectUnit();
            }
        }
    }

    public void DeselectGegner()
    {
        if (gegner != null)
        {
            gegner.GetComponent<Outline>().enabled = false;
            gegner = null;
            gegnergewaehlt = false;
            waehlegegner = true;
            aktionsmenue.SetActive(false);
        }
        gegnergewaehlt = false;
        waehlegegner = false;
        gegner = null;
        select = null;
        gewinner = null;
    }
    public void DeselectUnit()
    {
        if (unit != null)
        {
            unit.GetComponent<Outline>().enabled = false;
            unit = null;
            unitselected = false;
            aktionsmenue.SetActive(false);
        }
        unitselected = false;
        unit = null;
        select = null;
        gewinner = null;
        gegnergewaehlt = false;
    }
    public void DeselectFeld()
    {
        if (feld != null)
        {
            feld.GetComponent<Outline>().enabled = false;
            feld = null;
            feldselected = false;
            zielfeldsuche = false;
            for(int i = 0; i < grid.Length; i++)
            {
                grid[i].GetComponent<Outline>().enabled = false;
            }
        }
    }
    public void DeselectFabrik()
    {
        if (fabrik != null)
        {
            fabrik.GetComponent<Outline>().enabled = false;
            fabrik = null;
            waehlefabrik = false;
        }
    }
    void SelectUnit()
    {
        RaycastHit hitInfo;
        select = ReturnClickedObject(out hitInfo);
        if (select == unit && unitselected)
        {
            DeselectUnit();
            unitselected = false;
            select = null;
        }
        if(select != unit && unitselected && select!=null && !zielfeldsuche && !waehlegegner)
        {
            DeselectUnit();
            SelectUnit();
        }
        if (select == feld && feldselected)
        {
            DeselectFeld();
            feldselected = false;
            select = null;
        }
        if (select != null)
        {
            if (select.tag == "Einheit" && unit == null && !unitselected && !waehlegegner)
            {
                unit = select;
                aktionsmenue.SetActive(true);
                SetUnitStatText(unit);
                unit.GetComponent<Outline>().OutlineColor = Color.white;
                unit.GetComponent<Outline>().enabled = true;
                unitselected = true;
                select = null;
            }

            if (select.tag == "Einheit" && gegner == null && waehlegegner)
            {
                waehlefabrik = false;
                gegner = select;
                gegner.GetComponent<Outline>().OutlineColor = Color.red;
                gegner.GetComponent<Outline>().enabled = true;
                gegnergewaehlt = true;
                select = null;
            }

            if(select.tag == "HexFields" && fabrik==null && waehlefabrik)
            {
                waehlegegner = false;
                fabrik = select;
                fabrik.GetComponent<Outline>().OutlineColor = Color.red;
                fabrik.GetComponent<Outline>().enabled = true;

                select = null;
            }


            if (select.tag == "HexFields" && feld == null && !feldselected && unitselected && zielfeldsuche)
            {
                feld = select;
                if(feld.GetComponent<Outline>().enabled == true)
                {
                    feld.GetComponent<Outline>().enabled = false;
                }
                feld.GetComponent<Outline>().OutlineColor = Color.cyan;
                feld.GetComponent<Outline>().enabled = true;
                feldselected = true;
            }
        }
    }
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

    public void MoveUnits()
    {

        if (!buttonclicked && phase == 0)
        {
            anweisungText.GetComponent<TextMeshProUGUI>().text = "Zielfeld wählen!";
            bewegenButtonText.GetComponent<Text>().text = "Bestätigen";
            zielfeldsuche = true;
            waehlegegner = false;
            angriffButton.SetActive(false);
            buttonclicked = true;
            //Markierung der Felder wenn einheit nicht auf dem Spielfeld ist
            if (unit.GetComponent<UnitHelper>().fieldID == 0)
            {
                if (PassthrougData.currentPlayer == 1)
                {
                    for (int i = 2; i < 9; i++)
                    {
                        if (grid[i].GetComponent<FieldHelper>().hasUnit == false)
                        {
                            grid[i].GetComponent<Outline>().OutlineColor = Color.white;
                            grid[i].GetComponent<Outline>().enabled = true;
                        }
                        
                    }
                }
                else
                {
                    for (int i = 24; i < 31; i++)
                    {
                        if (grid[i].GetComponent<FieldHelper>().hasUnit == false)
                        {
                            grid[i].GetComponent<Outline>().OutlineColor = Color.white;
                            grid[i].GetComponent<Outline>().enabled = true;
                        }
                    }
                }
            }
            //Markierung der Felder in reichweite
            else
            {
                int aktionsp = dbc.GetAP(unit.GetComponent<UnitHelper>().unitID);
                GameObject unitfield=null;
                for(int i = 0; i < grid.Length; i++)
                {
                    if(unit.GetComponent<UnitHelper>().fieldID == grid[i].GetComponent<FieldHelper>().id)
                    {
                        unitfield = grid[i];
                    }
                }
                for (int x = (-1 * aktionsp); x <= aktionsp; x++)
                {
                    for (int y = (-1 * aktionsp); y <= aktionsp; y++)
                    {
                        for (int z = (-1 * aktionsp); z <= aktionsp; z++)
                        {
                            foreach(GameObject e in grid)
                            {
                                if((((e.GetComponent<FieldHelper>().x + x) + (e.GetComponent<FieldHelper>().y + y) + (e.GetComponent<FieldHelper>().z + z)) == 0) && !e.GetComponent<FieldHelper>().isfabrik)
                                {
                                    if(Distance(unitfield, e) <= aktionsp)
                                    {
                                        e.GetComponent<Outline>().OutlineColor = Color.white;
                                        e.GetComponent<Outline>().enabled = true;
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        if (phase != 0)
        {
            GameObject oldfeld=null;
            anweisungText.GetComponent<TextMeshProUGUI>().text = "";
            bewegenButtonText.GetComponent<Text>().text = "Einheitbewegen";
            angriffButton.SetActive(true);
            buttonclicked = false;
            if (unit != null && feld != null && (Convert.ToInt32(beweglicheEinheit.GetComponent<Text>().text) > 0) && dbc.GetFieldBonus(feld.GetComponent<FieldHelper>().id) < 10)
            {
                unit.transform.position = feld.transform.position + (new Vector3(0, 10, 0));
                string buff = unit.name.Substring(unit.name.Length - 2);
                unit.GetComponent<UnitHelper>().unitID = Convert.ToInt32(buff);
                dbc.WriteToDB("Update Gelaendefelder Set EinheitID = " + Convert.ToInt32(buff) + " Where ID=" + feld.GetComponent<FieldHelper>().id + " ");

                if(unit.GetComponent<UnitHelper>().fieldID == 0 && !feld.GetComponent<FieldHelper>().hasUnit)
                {
                    unit.GetComponent<UnitHelper>().fieldID = feld.GetComponent<FieldHelper>().id;
                    feld.GetComponent<FieldHelper>().unitID = unit.GetComponent<UnitHelper>().unitID;
                    beweglicheEinheit.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(beweglicheEinheit.GetComponent<Text>().text) - 1);
                    feld.GetComponent<FieldHelper>().hasUnit = true;
                    //CheckVictory(unit, feld);
                    rm.RefreshDisplay(PassthrougData.currentPlayer);
                }
                else if(unit.GetComponent<UnitHelper>().fieldID != feld.GetComponent<FieldHelper>().id)
                {
                    for(int i = 0; i < grid.Length; i++)
                    {
                        if(grid[i].GetComponent<FieldHelper>().id == unit.GetComponent<UnitHelper>().fieldID)
                        {
                            oldfeld = grid[i];
                            oldfeld.GetComponent<FieldHelper>().hasUnit = false;
                            oldfeld.GetComponent<FieldHelper>().unitID = 0;
                            unit.GetComponent<UnitHelper>().fieldID = feld.GetComponent<FieldHelper>().id;
                            feld.GetComponent<FieldHelper>().unitID = unit.GetComponent<UnitHelper>().unitID;
                            feld.GetComponent<FieldHelper>().hasUnit = true;
                            //CheckVictory(unit, feld);
                            rm.RefreshDisplay(PassthrougData.currentPlayer);
                        }
                    }
                    beweglicheEinheit.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(beweglicheEinheit.GetComponent<Text>().text) - 1);

                }

                DeselectFeld();
                DeselectUnit();
            }
            else
            {
                DeselectFeld();
                DeselectUnit();
            }
            phase = -1;
        }
        phase++;
    }

    int Distance(GameObject unitfield, GameObject fieldinreach)
    {
        int maxxy = Math.Max(Math.Abs(unitfield.GetComponent<FieldHelper>().x - fieldinreach.GetComponent<FieldHelper>().x), Math.Abs(unitfield.GetComponent<FieldHelper>().y - fieldinreach.GetComponent<FieldHelper>().y));
        return Math.Max(maxxy, Math.Abs(unitfield.GetComponent<FieldHelper>().z - fieldinreach.GetComponent<FieldHelper>().z));
    }
    

    void CheckVictory(GameObject unit, GameObject feld)
    {
        GameObject fabrik=null;
        if(PassthrougData.currentPlayer == 1)
        {
            for(int i = 0; i < grid.Length; i++)
            {
                if(grid[i].GetComponent<FieldHelper>().x==8 && grid[i].GetComponent<FieldHelper>().y == 0)
                {
                    fabrik=grid[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < grid.Length; i++)
            {
                if (grid[i].GetComponent<FieldHelper>().x == 1 && grid[i].GetComponent<FieldHelper>().y == 0)
                {
                    fabrik = grid[i];
                }
            }
        }
        if (fabrik.GetComponent<FieldHelper>().hasUnit)
        {
            if(dbc.GetUnitPlayerID(fabrik.GetComponent<FieldHelper>().unitID) == PassthrougData.currentPlayer)
            {
                gm.GameOver();   
            }
        }
    }

   

    public void Angriff()
    {
        bool validtarget;
        GameObject fieldunit = null;
        GameObject fieldopponent = null;
        int rw;
        if (!buttonclicked && phase == 0)
        {
            anweisungText.GetComponent<TextMeshProUGUI>().text = "Ziel wählen!";
            waehlegegner = true;
            waehlefabrik = true;
            buttonclicked = true;
            angriffButtonText.GetComponent<Text>().text = "Bestätigen";
            bewegenButton.SetActive(false);
        }
        if (phase != 0)
        {
            anweisungText.GetComponent<TextMeshProUGUI>().text = "";
            angriffButtonText.GetComponent<Text>().text = "Angriff";
            if(waehlegegner && !waehlefabrik)
            {
                for (int i = 0; i < grid.Length; i++)
                {
                    if (grid[i].GetComponent<FieldHelper>().id == unit.GetComponent<UnitHelper>().fieldID)
                    {
                        fieldunit = grid[i];
                    }

                    if (grid[i].GetComponent<FieldHelper>().id == gegner.GetComponent<UnitHelper>().fieldID)
                    {
                        fieldopponent = grid[i];
                    }
                }

                rw = dbc.GetRW(unit.GetComponent<UnitHelper>().unitID);

                if ((Math.Abs(fieldunit.GetComponent<FieldHelper>().x - fieldunit.GetComponent<FieldHelper>().y) - Math.Abs(fieldopponent.GetComponent<FieldHelper>().x - fieldopponent.GetComponent<FieldHelper>().y)) < rw + 1)
                {
                    validtarget = true;
                }
                else
                {
                    validtarget = false;
                }

                int distance = (Math.Abs(fieldunit.GetComponent<FieldHelper>().x - fieldunit.GetComponent<FieldHelper>().y) - Math.Abs(fieldopponent.GetComponent<FieldHelper>().x - fieldopponent.GetComponent<FieldHelper>().y));

                if (gegnergewaehlt && validtarget)
                {
                    km.SetAngreifer(unit);
                    km.SetVerteidiger(gegner);
                    km.SetDistance(distance);
                    km.ShowKampfMenu();
                }
                else
                {
                    DeselectFeld();
                    DeselectUnit();
                    DeselectGegner();
                }
                bewegenButton.SetActive(true);

            }
            else
            {
                int schaden = dbc.GetAtt(unit.GetComponent<UnitHelper>().unitID);
                if(PassthrougData.currentPlayer == 1)
                {
                    fb.SetLPFabrikR(schaden);
                }
                else
                {
                    fb.SetLPFabrikL(schaden);
                }
                DeselectUnit();
                DeselectFabrik();
            }
            buttonclicked = false;
            phase = -1;
        }
        phase++;
    }

    public void Continue()
    {
        DeselectUnit();
        DeselectGegner();
        aktionsmenue.SetActive(false);
    }
    
    public void FightWinner(GameObject winner)
    {
        this.gewinner = winner;

        if (gewinner == unit)
        {
            Destroy(gegner);
            DeselectGegner();
            DeselectUnit();
        }
        else
        {
            Destroy(unit);
            DeselectUnit();
            DeselectGegner();
        }
        aktionsmenue.SetActive(false);
    }

    void SetUnitStatText(GameObject unit)
    {
        nametext.GetComponent<TextMeshProUGUI>().text = dbc.GetUnitNamedif(unit.GetComponent<UnitHelper>().unitID);
        aptext.GetComponent<TextMeshProUGUI>().text = dbc.GetAP(unit.GetComponent<UnitHelper>().unitID).ToString();
        lptext.GetComponent<TextMeshProUGUI>().text = dbc.GetLP(unit.GetComponent<UnitHelper>().unitID).ToString();
        attext.GetComponent<TextMeshProUGUI>().text = dbc.GetAtt(unit.GetComponent<UnitHelper>().unitID).ToString();
        dftext.GetComponent<TextMeshProUGUI>().text = dbc.GetDef(unit.GetComponent<UnitHelper>().unitID).ToString();
        rwtext.GetComponent<TextMeshProUGUI>().text = dbc.GetRW(unit.GetComponent<UnitHelper>().unitID).ToString();
    }
}