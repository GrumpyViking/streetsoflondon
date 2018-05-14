using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour {

    
	// Update is called once per frame
	void Start() {
        
        for(int i = 0; i < 10; i++)
        {       
            //Koordinate Einheit1_Ls
            ObjectPooler.Insatnce.SpawnFromPool("Boss", new Vector3(-383, -42+ i * 10f, 1233), Quaternion.Euler(-90, 0, 0), 0);
            //Koordinate Einheit2_Ls
            ObjectPooler.Insatnce.SpawnFromPool("Diebin", new Vector3(-653, -42 + i * 10f, 1233), Quaternion.Euler(-90, 0, 0),1);
            //Koordinate Einheit3_Ls
            ObjectPooler.Insatnce.SpawnFromPool("Scharfschuetze", new Vector3(-895, -42 + i * 10f, 1233), Quaternion.Euler(-90, 0, 0),6);
            //Koordinate Einheit4_Ls
            ObjectPooler.Insatnce.SpawnFromPool("Raufbold", new Vector3(-508.5f, -42 + i * 10f, 1394), Quaternion.Euler(-90, 0, 0),5);
            //Koordinate Einheit5_Ls
            ObjectPooler.Insatnce.SpawnFromPool("Tueftler", new Vector3(-783.5f, -42 + i * 10f, 1393), Quaternion.Euler(-90, 0, 0),9);

            //Koordinate Einheit1_Rs
            ObjectPooler.Insatnce.SpawnFromPool("Tueftler_w", new Vector3(-900, -42 + i * 10f, -1239), Quaternion.Euler(-90, 180, 0), 19);
            //Koordinate Einheit2_Rs
            ObjectPooler.Insatnce.SpawnFromPool("Meuchelmörder", new Vector3(-627, -42 + i * 10f, -1239), Quaternion.Euler(-90, 180, 0), 12);
            //Koordinate Einheit3_Rs
            ObjectPooler.Insatnce.SpawnFromPool("Polizist", new Vector3(-383, -42 + i * 10f, -1243.6f), Quaternion.Euler(-90, 180, 0), 14);
            //Koordinate Einheit4_Rs
            ObjectPooler.Insatnce.SpawnFromPool("Pestarzt", new Vector3(-779, -42 + i * 10f, -1400), Quaternion.Euler(-90, 180, 0), 13);
            //Koordinate Einheit5_Rs
            ObjectPooler.Insatnce.SpawnFromPool("Taschendieb", new Vector3(-501, -42 + i * 10f, -1400), Quaternion.Euler(-90, 180, 0), 18);




        }

    }
    
   
}
