using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public Dictionary<string, Queue<GameObject>> poolDictonary;
    public Texture[] texArray;
    public string name="";
    public int count=0;
    public DataBaseController dbc;

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
    // Use this for initialization
    void Start()
    {
        poolDictonary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                
                obj.tag = "Einheit";
                //obj.AddComponent<MoveUnit>();
                obj.AddComponent<UnitHelper>();
                obj.AddComponent<Outline>();
                obj.GetComponent<Outline>().enabled = false;
                obj.AddComponent<Rigidbody>();
                obj.GetComponent<Rigidbody>().useGravity = false;
                obj.GetComponent<Rigidbody>().isKinematic = true;

                objectPool.Enqueue(obj);
            }
            poolDictonary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictonary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " doesn't exist");
            return null;
        }
        GameObject objectToSpawn = poolDictonary[tag].Dequeue();
        objectToSpawn.SetActive(true);
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
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        
        objectToSpawn.name = PassthroughData.currentPlayer + "_" + tag + "_"+ dbc.GetUnitID(tag);
        objectToSpawn.GetComponent<UnitHelper>().unitID = dbc.GetUnitID(tag);
        poolDictonary[tag].Enqueue(objectToSpawn);
        count++;
        return objectToSpawn;

    }
  
}
