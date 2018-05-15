using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropScript : MonoBehaviour
{
    //Initialize Variables
    GameObject getTarget;
    bool isMouseDragging;
    bool validPosition = false;
    Vector3 offsetValue;
    Vector3 positionOfScreen;
    Vector3 originalPosition;
    Vector3 currentPosition;
    
    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        //Debug.Log(PassthrougData.gameactiv == true);
        if (PassthrougData.gameactiv)
        {
            //Mouse Button Press Down
            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit hitInfo;
                getTarget = ReturnClickedObject(out hitInfo);
                if (getTarget != null)
                {
                    isMouseDragging = true;
                    originalPosition = getTarget.transform.position;
                    //Converting world position to screen position.

                    positionOfScreen = Camera.main.WorldToScreenPoint(getTarget.transform.position);
                    offsetValue = getTarget.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z));
                }
            }

            //Mouse Button Up
            if (Input.GetMouseButtonUp(0) && isMouseDragging)
            {
                isMouseDragging = false;
                if (!validPosition)
                {
                    getTarget.transform.position = originalPosition;
                }
                else
                {
                    getTarget.transform.position = currentPosition;
                }

            }

            //Is mouse Moving
            if (isMouseDragging)
            {
                //tracking mouse position.
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z);

                //converting screen position to world position with offset changes.
                currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offsetValue;

                //It will update target gameobject's current postion.
                getTarget.transform.position = currentPosition;
                if (validPosition)
                    Debug.Log("Gültige Position");

            }
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HexFields")
        {
            validPosition = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "HexFields")
        {
            validPosition = false;
        }
    }

    //Method to Return Clicked Object
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