using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public bool cameraActiv;
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    public Camera camera;
    public float maxZoom = 90f;
    public float zoomSpeed = 20f;
    public float minZoom = 20f;
    public float currentZoom = 65f;
    float scroll;

    private void Start()
    {
        SetCameraActiv(false);
    }

    public void SetCameraActiv(bool ca)
    {
        cameraActiv = ca;
    }

    void LateUpdate () {
        if (cameraActiv)
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
                
                camera.fieldOfView -= scroll * zoomSpeed;
                currentZoom = camera.fieldOfView;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && currentZoom >= minZoom  )
            {
                scroll = Input.GetAxisRaw("Mouse ScrollWheel");
                camera.fieldOfView -= scroll * zoomSpeed;
                currentZoom = camera.fieldOfView;
            }

            

            //pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
            //pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);


            transform.position = pos;
        }
        
	}

    public void SwitchSide(int side)
    {
        if(side == 0)
        {
            camera.transform.localPosition = new Vector3(80, 1000, -100);
            camera.transform.localRotation = Quaternion.Euler(65, 0, 0);
        }
        if (side == 1)
        {
            camera.transform.localPosition = new Vector3(80, 1000, 1350);
            camera.transform.localRotation = Quaternion.Euler(65, 180, 0);
        }
    }
}
