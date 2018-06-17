using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * TODO:
 * 
 * - AP beachten
 * - Anzeige von Gegnern/Fabrik in reichweite wenn angriffs option gewählt
 * - Fehler behebung wenn angriffsoption nicht gültig (nur unter bestimmten bedingungen)
 * - Kommentieren
 */


public class MoveUnit : MonoBehaviour
{
    GameObject select;
    GameObject unit;
    public Ressources rm;
    GameObject gegner = null;
    public Texture[] fabrikDamage; 
    GameObject feld;
    GameObject fabrik;
    GameObject aktionsmenue;
    GameObject unitField;
    GameObject gegnerField;
    List<GameObject> usedUnits = new List<GameObject>();
    public GameObject[] grid;
    public GameObject[] units;
    public Fabrik fb;

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
        if (PassthroughData.gameActiv)
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
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i].GetComponent<Outline>().enabled = false;
            grid[i].GetComponent<FieldHelper>().isSelectable = false;
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
        if (select == unit && unitselected && (!waehlegegner || !waehlefabrik))
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
            
            if (select.tag == "Einheit" && unit == null && !unitselected && !waehlegegner && dbc.GetUnitPlayerID(select.GetComponent<UnitHelper>().unitID)==PassthroughData.currentPlayer)
            {
                unit = select;
                aktionsmenue.SetActive(true);
                SetUnitStatText(unit);
                unit.GetComponent<Outline>().OutlineColor = Color.white;
                unit.GetComponent<Outline>().enabled = true;
                unitselected = true;
                //select = null;
                
            }

            if (select.tag == "Einheit" && gegner == null && waehlegegner == true && dbc.GetUnitPlayerID(select.GetComponent<UnitHelper>().unitID) != PassthroughData.currentPlayer)
            {
               
                waehlefabrik = false;
                gegner = select;
                gegner.GetComponent<Outline>().OutlineColor = Color.red;
                gegner.GetComponent<Outline>().enabled = true;
                gegnergewaehlt = true;
                //select = null;
            }

            if (PassthroughData.currentPlayer == 1)
            {
                if (select.tag == "Fabrik_R" && fabrik == null && waehlefabrik)
                {
                    
                    waehlegegner = false;
                    fabrik = select;
                    fabrik.GetComponent<Outline>().OutlineColor = Color.red;
                    fabrik.GetComponent<Outline>().enabled = true;

                    select = null;
                }
            }
            else
            {
                if (select.tag == "Fabrik_L" && fabrik == null && waehlefabrik)
                {
                    waehlegegner = false;
                    fabrik = select;
                    fabrik.GetComponent<Outline>().OutlineColor = Color.red;
                    fabrik.GetComponent<Outline>().enabled = true;

                    select = null;
                }
            }
            


            if (select.tag == "HexFields" && feld == null && !feldselected && unitselected && zielfeldsuche && select.GetComponent<FieldHelper>().isSelectable)
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
            select = null;
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
            //Markierung der Felder wenn die Einheit nicht auf dem Spielfeld ist
            if (unit.GetComponent<UnitHelper>().fieldID == 0)
            {
                if (PassthroughData.currentPlayer == 1)
                {
                    for (int i = 2; i < 9; i++)
                    {
                        if (grid[i].GetComponent<FieldHelper>().hasUnit == false)
                        {
                            grid[i].GetComponent<FieldHelper>().isSelectable = true;
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
                            grid[i].GetComponent<FieldHelper>().isSelectable = true;
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
                                        e.GetComponent<FieldHelper>().isSelectable = true;
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
                //Bewegen der Einheit und Datenbank update
                unit.transform.position = feld.transform.position + (new Vector3(0, 10, 0));
                string buff = unit.name.Substring(unit.name.Length - 2);
                unit.GetComponent<UnitHelper>().unitID = Convert.ToInt32(buff);
                dbc.WriteToDB("Update Gelaendefelder Set EinheitID = " + Convert.ToInt32(buff) + " Where ID=" + feld.GetComponent<FieldHelper>().id + " ");

                //Einheit kommt vom Stapel
                if(unit.GetComponent<UnitHelper>().fieldID == 0 && !feld.GetComponent<FieldHelper>().hasUnit)
                {
                    unit.GetComponent<UnitHelper>().fieldID = feld.GetComponent<FieldHelper>().id;
                    feld.GetComponent<FieldHelper>().unitID = unit.GetComponent<UnitHelper>().unitID;
                    beweglicheEinheit.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(beweglicheEinheit.GetComponent<Text>().text) - 1);
                    feld.GetComponent<FieldHelper>().hasUnit = true;
                    rm.RefreshDisplay(PassthroughData.currentPlayer);
                    unit.GetComponent<UnitHelper>().unitAP = 0;
                    usedUnits.Add(unit);
                }
                else if(unit.GetComponent<UnitHelper>().fieldID != feld.GetComponent<FieldHelper>().id )
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
                            rm.RefreshDisplay(PassthroughData.currentPlayer);
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
            //Markiere gültige Ziele
            int unitRW = dbc.GetRW(unit.GetComponent<UnitHelper>().unitID);
            GameObject unitfield = null;
            for (int i = 0; i < grid.Length; i++)
            {
                if (unit.GetComponent<UnitHelper>().fieldID == grid[i].GetComponent<FieldHelper>().id)
                {
                    unitfield = grid[i];
                }
            }
            for (int x = (-1 * unitRW); x <= unitRW; x++)
            {
                for (int y = (-1 * unitRW); y <= unitRW; y++)
                {
                    for (int z = (-1 * unitRW); z <= unitRW; z++)
                    {
                        foreach (GameObject e in grid)
                        {
                            if ((((e.GetComponent<FieldHelper>().x + x) + (e.GetComponent<FieldHelper>().y + y) + (e.GetComponent<FieldHelper>().z + z)) == 0) && !e.GetComponent<FieldHelper>().isfabrik)
                            {
                                if (Distance(unitfield, e) <= unitRW)
                                {
                                    
                                    e.GetComponent<Outline>().OutlineColor = Color.red;
                                    e.GetComponent<Outline>().enabled = true;
                                }
           
                            }
                        }
                    }
                }
            }
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

                if (Distance(fieldunit, fieldopponent)<=rw)
                {
                    validtarget = true;
                }
                else
                {
                    validtarget = false;
                }

                int distance = (Math.Abs(fieldunit.GetComponent<FieldHelper>().x - fieldunit.GetComponent<FieldHelper>().y) - Math.Abs(fieldopponent.GetComponent<FieldHelper>().x - fieldopponent.GetComponent<FieldHelper>().y));
                for(int i = 0; i < grid.Length; i++)
                {
                    if(grid[i].GetComponent<FieldHelper>().id == unit.GetComponent<UnitHelper>().fieldID)
                    {
                        unitField = grid[i];
                    }
                    if (grid[i].GetComponent<FieldHelper>().id == gegner.GetComponent<UnitHelper>().fieldID)
                    {
                        gegnerField = grid[i];
                    }
                }
                if (gegnergewaehlt && validtarget)
                {
                    km.SetAngreifer(unit);
                    km.SetAttField(unitField);
                    km.SetVerteidiger(gegner);
                    km.SetDefField(gegnerField);
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
                rw = dbc.GetRW(unit.GetComponent<UnitHelper>().unitID);
                if (PassthroughData.currentPlayer == 1)
                {
                    if (fabrik != null)
                    {
                        fb.SetLPFabrikR(schaden);
                    }
                }
                else
                {
                    if (fabrik != null)
                    {
                        fb.SetLPFabrikL(schaden);
                    }
                }
                DeselectUnit();
                DeselectFeld();
                DeselectGegner();
                DeselectFabrik();
                bewegenButton.SetActive(true);
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

    public void ResetAP()
    {
        for(int i = 0; i < usedUnits.Count; i++)
        {
            usedUnits[i].GetComponent<UnitHelper>().unitAP = usedUnits[i].GetComponent<UnitHelper>().unitDefaultAP;
        }
        usedUnits.Clear();
    }
}