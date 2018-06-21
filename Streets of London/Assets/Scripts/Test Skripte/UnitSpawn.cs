using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Ursprüngliche Spawn funktion 
 * nicht weiter verwendet
 * 
 */
 

public class UnitSpawn : MonoBehaviour {

    
	// Update is called once per frame
	void SpawnUnit() {
        
        for(int i = 0; i < 10; i++)
        {       
            //Koordinate Einheit1_Ls
            ObjectPooler.Instance.SpawnFromPool("Boss", new Vector3(-383, -42+ i * 10f, 1233), Quaternion.Euler(-90, 0, 0),1);
            //Koordinate Einheit2_Ls
            ObjectPooler.Instance.SpawnFromPool("Boss", new Vector3(-653, -42 + i * 10f, 1233), Quaternion.Euler(-90, 0, 0),2);
            //Koordinate Einheit3_Ls
            ObjectPooler.Instance.SpawnFromPool("Boss", new Vector3(-895, -42 + i * 10f, 1233), Quaternion.Euler(-90, 0, 0),3);
            //Koordinate Einheit4_Ls
            ObjectPooler.Instance.SpawnFromPool("Boss", new Vector3(-508.5f, -42 + i * 10f, 1394), Quaternion.Euler(-90, 0, 0),4);
            //Koordinate Einheit5_Ls
            ObjectPooler.Instance.SpawnFromPool("Boss", new Vector3(-783.5f, -42 + i * 10f, 1393), Quaternion.Euler(-90, 0, 0),5);

            //Koordinate Einheit1_Rs
            ObjectPooler.Instance.SpawnFromPool("Tueftler_w", new Vector3(-900, -42 + i * 10f, -1239), Quaternion.Euler(-90, 180, 0),1);
            //Koordinate Einheit2_Rs
            ObjectPooler.Instance.SpawnFromPool("Meuchelmörder", new Vector3(-627, -42 + i * 10f, -1239), Quaternion.Euler(-90, 180, 0),2);
            //Koordinate Einheit3_Rs
            ObjectPooler.Instance.SpawnFromPool("Polizist", new Vector3(-383, -42 + i * 10f, -1243.6f), Quaternion.Euler(-90, 180, 0),3);
            //Koordinate Einheit4_Rs
            ObjectPooler.Instance.SpawnFromPool("Pestarzt", new Vector3(-779, -42 + i * 10f, -1400), Quaternion.Euler(-90, 180, 0),4);
            //Koordinate Einheit5_Rs
            ObjectPooler.Instance.SpawnFromPool("Taschendieb", new Vector3(-501, -42 + i * 10f, -1400), Quaternion.Euler(-90, 180, 0),5);

        }
    }
    
   
}
