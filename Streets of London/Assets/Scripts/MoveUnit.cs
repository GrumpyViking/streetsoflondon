using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit : MonoBehaviour {
    GameObject select;
    GameObject unit;
    GameObject feld;
    GameObject aktionsmenue;
    bool unitselected=false;


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

    void SelectUnit()
    {
        RaycastHit hitInfo;
        select = ReturnClickedObject(out hitInfo);
        if(select == unit)
        {         
            DeselectUnit();
            select = null;
        }
        if (select != null)
        {
            
            if (select.tag == "Einheit" && unit == null)
            {
                unit = select;
                aktionsmenue.SetActive(true);
                unit.GetComponent<Outline>().enabled = true;
            }
            //}else if(select.tag == "HexFields" && feld == null){
            //    feld = select;
            //    feld.GetComponent<Outline>().enabled = true;
            //}
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




}
