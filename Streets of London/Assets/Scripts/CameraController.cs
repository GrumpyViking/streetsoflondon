using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

    public bool cameraActiv = true;
    public Camera camera;

    public float maxZoom = 90f;
    public float zoomSpeed = 20f;
    public float minZoom = 20f;
    public float currentZoom = 65f;

    float scroll;



    //
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
                Debug.Log(Input.GetAxisRaw("Mouse ScrollWheel"));
                camera.fieldOfView -= scroll * zoomSpeed;
                currentZoom = camera.fieldOfView;
                Debug.Log(currentZoom);
            }

            

            pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
            pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);


            transform.position = pos;
        }
        
	}

}
