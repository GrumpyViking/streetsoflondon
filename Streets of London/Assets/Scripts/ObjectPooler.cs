using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ObjectPooler Skript
 * 
 * Objectpools werden genutzt um schon bei initialisierung des Spieles alle nötigen objekte zu erstellen.
 * Dies verhindert im späteren Spielgeschen das auftretten von lags wenn viele Einheiten aufeinmal erstellt werden müssten.
 * Zudem beschränkt es das maximale vorkommen der Einheiten sodass nur eine Gewisse anzahl von Einheit x existieren kann wenn der Pool leer ist.
 * 
 * Autor: Martin Schuster
 * 
 * Nutzung der vorlage von Youtuber Brackeys https://www.youtube.com/watch?v=tdSmKaJvCoA
 */

public class ObjectPooler : MonoBehaviour
{
    //Verzeichniss aller vorhandenen Pools
    public Dictionary<string, Queue<GameObject>> poolDictonary;
    //Array für die einzelnen Texturen der Einheitensteine
    public Texture[] texArray;

    //Datenbank Skript
    public DataBaseController dbc;

    //Pool Objekt  
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;

    //Wird bei Spielstart ausgeführt Initialisiert die Pools und erstellt die einheiten
    void Start()
    {
        poolDictonary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false); //neue Einheit wird deaktiviert
                obj.tag = "Einheit"; //fügt der Einheit den tag Einheit zu später relevant für die Einheiten bewegung
                obj.AddComponent<UnitHelper>(); //fügt der Einheit das UnitHelper Skript hinzu
                obj.AddComponent<Outline>(); // fügt der Einheit das Outline Skript hinzu
                obj.GetComponent<Outline>().enabled = false;
                obj.AddComponent<Rigidbody>(); //für der Einheit ein Rigidbody hinzu wichtig für auswahl
                obj.GetComponent<Rigidbody>().useGravity = false;
                obj.GetComponent<Rigidbody>().isKinematic = true;
                objectPool.Enqueue(obj); // fügt objekt dem Pool hinzu
            }
            poolDictonary.Add(pool.tag, objectPool);
        }
    }

    //Spawnt(Aktiviert Einheiten an der Übergebenen Position 
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, int spawnPoint)
    {
        //Sicherheits überprüfung ob Pool existiert
        if (!poolDictonary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " doesn't exist");
            return null;
        }
        GameObject objectToSpawn = poolDictonary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        //Setzt die Texture auf die Einheit entsprechend des Teams und des Spawnpools
        if (PassthroughData.currentPlayer == 1)
        {
            if (tag == "Boss")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[0];
            }
            if (tag == "Diebin")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[1];
            }
            if (tag == "Meuchelmoerder")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[2];
            }
            if (tag == "Pestarzt")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[3];
            }
            if (tag == "Polizist")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[4];
            }
            if (tag == "Raufbold")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[5];
            }
            if (tag == "Scharfschuetze")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[6];
            }
            if (tag == "Schlaeger")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[7];
            }
            if (tag == "Taschendieb")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[8];
            }
            if (tag == "Tueftler")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[9];
            }
        }
        else
        {
            if (tag == "Boss")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[10];
            }
            if (tag == "Diebin")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[11];
            }
            if (tag == "Meuchelmoerder")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[12];
            }
            if (tag == "Pestarzt")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[13];
            }
            if (tag == "Polizist")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[14];
            }
            if (tag == "Raufbold")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[15];
            }
            if (tag == "Scharfschuetze")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[16];
            }
            if (tag == "Schlaeger")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[17];
            }
            if (tag == "Taschendieb")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[18];
            }
            if (tag == "Tueftler")
            {
                objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[19];
            }
        }
        objectToSpawn.transform.position = position; //weißt neuer Einheit die übergebene Position zu
        objectToSpawn.transform.rotation = rotation; //weißt neuer Einheit die übergebene Rotation zu
        objectToSpawn.GetComponent<UnitHelper>().unitDefaultAP = dbc.GetAP(dbc.GetUnitID(tag)); //Weißt die AktionsPunkte zu 
        objectToSpawn.GetComponent<UnitHelper>().unitAP = dbc.GetAP(dbc.GetUnitID(tag)); //Weißt die aktuellen Aktionspunkte zu diese werden abgezogen und am Runden ende wieder auf den Defaultwert zurückgesetzt
        objectToSpawn.name = PassthroughData.currentPlayer + "_" +spawnPoint+"_"+ tag + "_"+ dbc.GetUnitID(tag); //Setzt Name bestehend aus Spieler ID dem spawn point dem eigentlichen namen und er ID
        objectToSpawn.GetComponent<UnitHelper>().unitID = dbc.GetUnitID(tag); // Weißt ID der Einheit zu
        poolDictonary[tag].Enqueue(objectToSpawn); //Entnimmt dem Pool die Einheit
        
        return objectToSpawn;
    }
  
}
