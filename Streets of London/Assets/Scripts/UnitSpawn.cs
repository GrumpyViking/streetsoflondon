using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour {

    public int amount;
    public GameObject prefab;
    public Texture[] texArray;


    // Use this for initialization
    void Start () {
        SpawnUnit();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void SpawnUnit()
    {
        
        for(int i = 0; i < amount; i++)
        {
            Instantiate(prefab, new Vector3(i * 10F, 0, 0), Quaternion.Euler(-90,180,0));
            prefab.GetComponent<Renderer>().sharedMaterial.SetTexture("_MainTex",texArray[i]);
        }
        
    }
}
