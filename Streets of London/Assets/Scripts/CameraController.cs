﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public bool cameraActiv;
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    public Camera camOverHead;
    public Camera cameraPlayer;

    public float maxZoom = 90f;
    int side;
    public float zoomSpeed = 20f;
    public float minZoom = 20f;
    public float currentZoom = 65f;
    float scroll;

    private void Start()
    {
        SetCameraActiv(false);
        cameraPlayer.enabled = false;
        cameraPlayer.tag = "Untagged";
        camOverHead.tag = "MainCamera";

    }

    public void SetCameraActiv(bool ca)
    {
        cameraActiv = ca;
    }

    void LateUpdate () {
        if (cameraActiv && side == 1)   //Kamera Spieler1
        {
            Vector3 pos = transform.position;

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                pos.z += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                pos.z -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                pos.x += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            if(Input.GetAxis("Mouse ScrollWheel") < 0 && currentZoom <= maxZoom)   //Heraus Zoomen
            {
                scroll = Input.GetAxisRaw("Mouse ScrollWheel");
                
                cameraPlayer.fieldOfView -= scroll * zoomSpeed;
                currentZoom = cameraPlayer.fieldOfView;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && currentZoom >= minZoom  )
            {
                scroll = Input.GetAxisRaw("Mouse ScrollWheel");
                cameraPlayer.fieldOfView -= scroll * zoomSpeed;
                currentZoom = cameraPlayer.fieldOfView;
            }
            transform.position = pos;
        }else if (cameraActiv && side == 2){ //Kamera Spieler 2
            Vector3 pos = transform.position;

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                pos.z -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                pos.z += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                pos.x += panSpeed * Time.deltaTime;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && currentZoom <= maxZoom)   //Heraus Zoomen
            {
                scroll = Input.GetAxisRaw("Mouse ScrollWheel");

                cameraPlayer.fieldOfView -= scroll * zoomSpeed;
                currentZoom = cameraPlayer.fieldOfView;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && currentZoom >= minZoom)
            {
                scroll = Input.GetAxisRaw("Mouse ScrollWheel");
                cameraPlayer.fieldOfView -= scroll * zoomSpeed;
                currentZoom = cameraPlayer.fieldOfView;
            }
            transform.position = pos;
        }

    }

    public void SwitchSide(int side)
    {
        this.side = side;
        if(side == 1)
        {
            camOverHead.enabled = false;
            camOverHead.tag = "Untagged";
            cameraPlayer.enabled = true;
            cameraPlayer.tag = "MainCamera";
            cameraPlayer.transform.position = new Vector3(-20, 1000, -1625);
            cameraPlayer.transform.rotation = Quaternion.Euler(65, 0, 0);
        }
        if (side == 2)
        {
            camOverHead.enabled = false;
            cameraPlayer.enabled = true;
            cameraPlayer.transform.position = new Vector3(-20, 1000, 1625);
            cameraPlayer.transform.rotation = Quaternion.Euler(65, 180, 0);

        }
    }
}
