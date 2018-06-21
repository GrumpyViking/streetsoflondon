using UnityEngine;

/*
 * CameraController Skript beinhaltet die Bewegung der Camera sowie das wechseln der Perspektiven für die Verschiedenen Aktionen.
 * 
 * Autor: Martin Schuster
 * Nutzung der Vorlage von Brackeys https://www.youtube.com/watch?v=cfjLQrMGEb4
 * erweiterung um eigene anpassung
 */

public class CameraController : MonoBehaviour {

    //Die beiden Kameraobjekte camOverHead dient zum Spielfeldaufbau cameraPlayer dient zur eigentlichen Spielansicht
    public Camera camOverHead;
    public Camera cameraPlayer;

    //bietet möglichkeit die Kamera zu Aktivieren bzw Deaktivieren
    public bool cameraActiv;
    
    //Geschwindigkeit der Kamera
    public float panSpeed = 40f;
    public float panBorderThickness = 10f;

    //Zoom einstellungen
    public float maxZoom = 90f;
    public float zoomSpeed = 20f;
    public float minZoom = 20f;
    public float currentZoom = 65f;

    //Hilfsvariablen side um zu Bestimmen welcher Spieler Aktiv ist und scroll zur vewendung des Mausrads bei der Zoom Funktion.
    float scroll;
    int side;

    /* 
     * Deaktiviert das Bewegen der Kamera beim Start der Szene
     * 
     * Deaktiviert die SpielerKamera und setzt die Kamera für die draufansicht als Hauptkamera
     */ 

    private void Start()
    {
        SetCameraActiv(false);
        cameraPlayer.enabled = false;
        cameraPlayer.tag = "Untagged";
        camOverHead.tag = "MainCamera";
    }

    //Zusändig für das Aktivieren und Deaktivieren der Kamerabewegung
    public void SetCameraActiv(bool ca)
    {
        cameraActiv = ca;
    }

    /*
     * LateUpdate führt den Code nach allen anderen Update funktionen aus.
     * 
     * Verhindert so das unerwünschte anzeigen enstehen können z.B.: texturen noch nicht geladen, alte Anzeigen etc.
     * 
     * Führt entsprechend des Tastendruckes bzw der MousePosition die Bewegung der Kamera aus falls diese aktiv ist.
     */
     
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

    /*
     * Wechselt die Kamerasicht entsprechend welcher Spieler am zug ist. 
     */
     
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
