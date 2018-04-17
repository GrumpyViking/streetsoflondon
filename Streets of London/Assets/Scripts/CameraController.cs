using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float cameraCurrentZoom = 50f;
    public float cameraZoomMax = 50f;
    public float cameraZoomMin = 10f;
    public Vector2 panLimit;
    public bool cameraActiv = true;

    void Start()
    {
        Camera.main.orthographicSize = cameraCurrentZoom;
    }

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
            if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
            {
                if (cameraCurrentZoom < cameraZoomMax)
                {
                    cameraCurrentZoom += 1;
                    Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize + 1);
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
            {
                if (cameraCurrentZoom > cameraZoomMin)
                {
                    cameraCurrentZoom -= 1;
                    Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize - 1);
                }
            }

            pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
            pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);


            transform.position = pos;
        }
        
	}

}
