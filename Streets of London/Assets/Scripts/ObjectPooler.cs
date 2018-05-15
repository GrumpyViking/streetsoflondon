using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public Dictionary<string, Queue<GameObject>> poolDictonary;
    public Texture[] texArray;
    public string name="Test";
    public int count=0;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Insatnce;
    private void Awake()
    {
        Insatnce = this;
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
                obj.name = name+count;
                count++;
                
                //obj.AddComponent<DragDropScript>();
                obj.AddComponent <Outline> ();
                obj.AddComponent<Rigidbody>();
                obj.GetComponent<Rigidbody>().useGravity = false;
                obj.GetComponent<Rigidbody>().isKinematic = true;

                objectPool.Enqueue(obj);
            }
            poolDictonary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, int textureindex)
    {
        if (!poolDictonary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " doesn't exist");
            return null;
        }
        GameObject objectToSpawn = poolDictonary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.GetComponent<Renderer>().material.mainTexture = texArray[textureindex];
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        
        poolDictonary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;

    }
  
}
