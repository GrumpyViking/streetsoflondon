using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveUnit : MonoBehaviour
{
    GameObject select;
    public GameObject unit;
    public GameObject gegner;
    GameObject feld;
    GameObject aktionsmenue;
    public GameObject[] grid;

    Unit test;
    public GameObject anweisungText;
    public GameObject bewegenButtonText;
    public GameObject angriffButtonText;
    public GameObject bewegenButton;
    public GameObject angriffButton;
    public GameObject beweglicheEinheit;
    public KampfMenu km;
    public DataBaseController dbc;

    public GameObject gewinner;
    bool unitselected = false;
    bool feldselected = false;
    bool zielfeldsuche = false;
    bool buttonclicked = false;
    bool waehlegegner = false;
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
                unit.GetComponent<Outline>().OutlineColor = Color.white;
                unit.GetComponent<Outline>().enabled = true;
                unitselected = true;
                select = null;
            }

            if (select.tag == "Einheit" && gegner == null && waehlegegner)
            {
                gegner = select;
                gegner.GetComponent<Outline>().OutlineColor = Color.red;
                gegner.GetComponent<Outline>().enabled = true;
                gegnergewaehlt = true;
                select = null;
            }

            if (select.tag == "HexFields" && feld == null && !feldselected && unitselected && zielfeldsuche)
            {
                feld = select;
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
            angriffButton.SetActive(false);
            buttonclicked = true;
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
                            beweglicheEinheit.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(beweglicheEinheit.GetComponent<Text>().text) - 1);
                            feld.GetComponent<FieldHelper>().hasUnit = true;
                        }
                    }
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

    public void Angriff()
    {
        if (!buttonclicked && phase == 0)
        {
            anweisungText.GetComponent<TextMeshProUGUI>().text = "Ziel wählen!";
            waehlegegner = true;
            buttonclicked = true;
            angriffButtonText.GetComponent<Text>().text = "Bestätigen";
            bewegenButton.SetActive(false);
        }
        if (phase != 0)
        {
            anweisungText.GetComponent<TextMeshProUGUI>().text = "";
            angriffButtonText.GetComponent<Text>().text = "Angriff";
            if (gegnergewaehlt)
            {
                km.SetAngreifer(unit);
                km.SetVerteidiger(gegner);
                km.ShowKampfMenu();
            }
            else
            {
                DeselectFeld();
                DeselectUnit();
                DeselectGegner();
            }
            bewegenButton.SetActive(true);
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
}