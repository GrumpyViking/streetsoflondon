using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * MoveUnit Skript
 * 
 * - Stellt Funktionen für die Einheitenbewegung und die Initialisierung für einen Angriff bereit
 * 
 * 
 * Autor: Martin Schuster
 */


public class MoveUnit : MonoBehaviour
{
    GameObject select;
    GameObject unit;
    public Ressources rm;
    GameObject gegner = null;
    public Texture[] fabrikDamage; 
    GameObject feld=null;
    GameObject fabrik;
    GameObject aktionsMenue;
    GameObject unitField;
    GameObject gegnerField;
    List<GameObject> usedUnits = new List<GameObject>();
    public GameObject[] grid;
    public GameObject[] units;
    public GameObject[] spawnL;
    public GameObject[] spawnR;
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

    //UI-Objekte für die Statuswerte der einzelnen Einheiten
    public GameObject nameText;
    public GameObject apText;
    public GameObject lpText;
    public GameObject atText;
    public GameObject dfText;
    public GameObject rwText;
    
    //Hilfsvariablen um gewisse bedingungen zu Kontrollieren
    bool unitSelected = false;
    bool feldSelected = false;
    bool zielFeldSuche = false;
    bool buttonClicked = false;
    bool waehleGegner = false;
    bool waehleFabrik = false;
    bool gegnerGewaehlt = false;
    int phase = 0;
    GameObject gewinner;


    private void Start()
    {
        aktionsMenue = GameObject.Find("UI/Panels/Aktionsmenue");
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
            aktionsMenue.SetActive(false);
        }
        gegnerGewaehlt = false;
        waehleGegner = false;
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
            unitSelected = false;
            aktionsMenue.SetActive(false);
        }
        unitSelected = false;
        unit = null;
        select = null;
        gewinner = null;
        bewegenButton.SetActive(true);
        angriffButton.SetActive(true);
        gegnerGewaehlt = false;
    }
    public void DeselectFeld()
    {
        if (feld != null)
        {
            feld.GetComponent<Outline>().enabled = false;
            feld = null;
            feldSelected = false;
            zielFeldSuche = false;
            
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
            waehleFabrik = false;
        }
    }

    void SelectUnit()
    {
        RaycastHit hitInfo;
        select = ReturnClickedObject(out hitInfo);
        if (select == unit && unitSelected && (!waehleGegner || !waehleFabrik))
        {
            DeselectUnit();
            unitSelected = false;
            select = null;
        }
        if(select != unit && unitSelected && select!=null && !zielFeldSuche && !waehleGegner)
        {
            DeselectUnit();
            SelectUnit();
        }
        if (select == feld && feldSelected)
        {
            DeselectFeld();
            feldSelected = false;
            select = null;
        }
        if (select != null)
        {
            
            if (select.tag == "Einheit" && unit == null && !unitSelected && !waehleGegner && dbc.GetUnitPlayerID(select.GetComponent<UnitHelper>().unitID)==PassthroughData.currentPlayer)
            {
                unit = select;
                aktionsMenue.SetActive(true);
                if (unit.GetComponent<UnitHelper>().fieldID == 0)
                {
                    angriffButton.SetActive(false);
                }
                else
                {
                    if (unit.GetComponent<UnitHelper>().unitAP == 0)
                    {
                        angriffButton.SetActive(false);
                        bewegenButton.SetActive(false);
                    }
                    else
                    {
                        angriffButton.SetActive(true);
                        bewegenButton.SetActive(true);
                    }
                }
                
                SetUnitStatText(unit);
                unit.GetComponent<Outline>().OutlineColor = Color.white;
                unit.GetComponent<Outline>().enabled = true;
                unitSelected = true;
                //select = null;
                
            }

            if (select.tag == "Einheit" && gegner == null && waehleGegner == true && dbc.GetUnitPlayerID(select.GetComponent<UnitHelper>().unitID) != PassthroughData.currentPlayer)
            {
               
                waehleFabrik = false;
                gegner = select;
                gegner.GetComponent<Outline>().OutlineColor = Color.red;
                gegner.GetComponent<Outline>().enabled = true;
                gegnerGewaehlt = true;
                //select = null;
            }

            if (PassthroughData.currentPlayer == 1)
            {
                if (select.tag == "Fabrik_R" && fabrik == null && waehleFabrik)
                {
                    
                    waehleGegner = false;
                    fabrik = select;
                    fabrik.GetComponent<Outline>().OutlineColor = Color.red;
                    fabrik.GetComponent<Outline>().enabled = true;

                    select = null;
                }
            }
            else
            {
                if (select.tag == "Fabrik_L" && fabrik == null && waehleFabrik)
                {
                    waehleGegner = false;
                    fabrik = select;
                    fabrik.GetComponent<Outline>().OutlineColor = Color.red;
                    fabrik.GetComponent<Outline>().enabled = true;

                    select = null;
                }
            }

            if (select.tag == "HexFields" && feld == null && !feldSelected && unitSelected && zielFeldSuche && select.GetComponent<FieldHelper>().isSelectable)
            {
                feld = select;
                if(feld.GetComponent<Outline>().enabled == true)
                {
                    feld.GetComponent<Outline>().enabled = false;
                }
                feld.GetComponent<Outline>().OutlineColor = Color.cyan;
                feld.GetComponent<Outline>().enabled = true;
                feldSelected = true;
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

        if (!buttonClicked && phase == 0 && unit.GetComponent<UnitHelper>().unitAP>0)
        {
            anweisungText.GetComponent<TextMeshProUGUI>().text = "Zielfeld wählen!";
            bewegenButtonText.GetComponent<Text>().text = "Bestätigen";
            zielFeldSuche = true;
            waehleGegner = false;
            angriffButton.SetActive(false);
            buttonClicked = true;
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
            buttonClicked = false;
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
                    if (PassthroughData.currentPlayer == 1)
                    {
                        for(int i = 0; i < spawnL.Length; i++)
                        {
                            if (Convert.ToInt32(unit.name.Substring(2, 1)) == (i + 1)){
                                spawnL[i].GetComponent<SpawnHelper>().numOfUnits--;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < spawnR.Length; i++)
                        {
                            if (Convert.ToInt32(unit.name.Substring(2, 1)) == (i + 1))
                            {
                                spawnR[i].GetComponent<SpawnHelper>().numOfUnits--;
                            }
                        }
                    }
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
                            unit.GetComponent<UnitHelper>().unitAP -= Distance(oldfeld, feld);
                            usedUnits.Add(unit);
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
        if (!buttonClicked && phase == 0 && unit.GetComponent<UnitHelper>().unitAP>0)
        {
            anweisungText.GetComponent<TextMeshProUGUI>().text = "Ziel wählen!";
            waehleGegner = true;
            waehleFabrik = true;
            buttonClicked = true;
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
            if(waehleGegner && !waehleFabrik)
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
                if (gegnerGewaehlt && validtarget)
                {
                    km.SetAngreifer(unit);
                    km.SetAttField(unitField);
                    km.SetVerteidiger(gegner);
                    km.SetDefField(gegnerField);
                    km.SetDistance(distance);
                    km.ShowKampfMenu();
                    unit.GetComponent<UnitHelper>().unitAP = 0;
                    usedUnits.Add(unit);
                }
                else
                {
                    DeselectFeld();
                    DeselectUnit();
                    DeselectGegner();
                }
               
                bewegenButton.SetActive(true);

            }
            else if(waehleFabrik && fabrik!=null)
            {

                int schaden = dbc.GetAtt(unit.GetComponent<UnitHelper>().unitID);
                rw = dbc.GetRW(unit.GetComponent<UnitHelper>().unitID);
                for (int i = 0; i < grid.Length; i++)
                {
                    if (grid[i].GetComponent<FieldHelper>().id == unit.GetComponent<UnitHelper>().fieldID)
                    {
                        unitField = grid[i];
                    }
                }
                if (Distance(unitField, fabrik) <= rw)
                {
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
                    unit.GetComponent<UnitHelper>().unitAP = 0;
                    usedUnits.Add(unit);
                }
               
                DeselectUnit();
                DeselectFeld();
                DeselectGegner();
                DeselectFabrik();
                bewegenButton.SetActive(true);
            }
            else if(gegner==null && fabrik == null)
            {
                DeselectUnit();
                DeselectFeld();
                DeselectGegner();
                DeselectFabrik();
                bewegenButton.SetActive(true);
            }
            buttonClicked = false;
            phase = -1;
        }
        phase++;
    }

    public void Continue()
    {
        DeselectUnit();
        DeselectGegner();
        aktionsMenue.SetActive(false);
    }
    
    public void FightWinner(GameObject winner)
    {
        this.gewinner = winner;

        if (gewinner == unit)
        {
            usedUnits.Remove(gegner);
            Destroy(gegner);
            DeselectGegner();
            DeselectUnit();
        }
        else
        {
            usedUnits.Remove(unit);
            Destroy(unit);
            DeselectUnit();
            DeselectGegner();
        }
        DeselectFeld();
        DeselectUnit();
        DeselectGegner();
        aktionsMenue.SetActive(false);
    }

    void SetUnitStatText(GameObject unit)
    {
        nameText.GetComponent<TextMeshProUGUI>().text = dbc.GetUnitNamedif(unit.GetComponent<UnitHelper>().unitID);
        apText.GetComponent<TextMeshProUGUI>().text = dbc.GetAP(unit.GetComponent<UnitHelper>().unitID).ToString();
        lpText.GetComponent<TextMeshProUGUI>().text = dbc.GetLP(unit.GetComponent<UnitHelper>().unitID).ToString();
        atText.GetComponent<TextMeshProUGUI>().text = dbc.GetAtt(unit.GetComponent<UnitHelper>().unitID).ToString();
        dfText.GetComponent<TextMeshProUGUI>().text = dbc.GetDef(unit.GetComponent<UnitHelper>().unitID).ToString();
        rwText.GetComponent<TextMeshProUGUI>().text = dbc.GetRW(unit.GetComponent<UnitHelper>().unitID).ToString();
    }

    public void ResetAP()
    {
        for(int i = 0; i < usedUnits.Count; i++)
        {
            usedUnits[i].GetComponent<UnitHelper>().unitAP = usedUnits[i].GetComponent<UnitHelper>().unitDefaultAP;
        }
        usedUnits.Clear();
        
    }

    public void ResetKampfAnzeige()
    {
        DeselectFeld();
        DeselectUnit();
        DeselectGegner();
    }

    public void ResetAktionsMenu()
    {
        bewegenButton.SetActive(true);
        bewegenButtonText.GetComponent<Text>().text = "Einheitbewegen";
        angriffButton.SetActive(true);
        angriffButtonText.GetComponent<Text>().text = "Angriff";
    }
}