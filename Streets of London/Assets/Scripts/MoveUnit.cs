using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveUnit : MonoBehaviour {
    GameObject select;
    GameObject unit;
    GameObject feld;
    GameObject aktionsmenue;
    public GameObject anweisungText;
    public GameObject bewegenButtonText;
    public GameObject angriffButtonText;
    public GameObject bewegenButton;
    public GameObject angriffButton;

    bool unitselected = false;
    bool feldselected = false;
    bool buttonclicked = false;
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

    void DeselectUnit()
    {
        unit.GetComponent<Outline>().enabled = false;
        unit = null;
        aktionsmenue.SetActive(false);
        
    }
    void DeselectFeld()
    {
        feld.GetComponent<Outline>().enabled = false;
        feld = null;
    }
    void SelectUnit()
    {
        RaycastHit hitInfo;
        select = ReturnClickedObject(out hitInfo);
        if(select == unit && unitselected)
        {         
            DeselectUnit();
            unitselected = false;
            select = null;
        }
        if(select == feld && feldselected)
        {
            DeselectFeld();
            feldselected = false;
            select = null;
        }
        if (select != null)
        {
            if (select.tag == "Einheit" && unit == null && !unitselected)
            {
                unit = select;
                aktionsmenue.SetActive(true);
                unit.GetComponent<Outline>().enabled = true;
                unitselected = true;
            }

            if(select.tag == "HexFields" && feld == null && !feldselected && unitselected)
            {
                feld = select;
                feld.GetComponent<Outline>().enabled = true;
            }
            //if (select.tag == "Einheit" && select == unit)
            //{
            //    unit.GetComponent<Outline>().enabled = false;
            //    aktionsmenue.SetActive(false);
            //    unit = null;
            //    select = null;
            //}
            //if(unit != null && feld != null)
            //{
            //    unit.transform.position = feld.transform.position + (new Vector3(0, +10,0));
            //    unit.GetComponent<Outline>().enabled = false;
            //    unit = null;
            //    feld.GetComponent<Outline>().enabled = false;
            //    feld = null;
            //}
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
            angriffButton.SetActive(false);
            buttonclicked = true;
            
        }

        if (phase != 0)
        {
            anweisungText.GetComponent<TextMeshProUGUI>().text = "";
            bewegenButtonText.GetComponent<Text>().text = "Einheitbewegen";
            angriffButton.SetActive(true);
            buttonclicked = false;
            if (unit != null && feld != null)
            {
                unit.transform.position = feld.transform.position + (new Vector3(0, 10, 0));
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
            angriffButtonText.GetComponent<Text>().text = "Bestätigen";
            bewegenButton.SetActive(false);
        }
        if(phase != 0)
        {
            anweisungText.GetComponent<TextMeshProUGUI>().text = "";
            angriffButtonText.GetComponent<Text>().text = "Angriff";
            bewegenButton.SetActive(true);
            buttonclicked = false;
            phase = -1;
        }
        phase++;

    }




}
