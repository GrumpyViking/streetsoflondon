using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTypCards : MonoBehaviour {
    public Material[] materials;
    public GameObject[] card;
    public DataBaseController dbc;

	public void Initialise()
    {
        int[] ids = new int[5];
        ids = dbc.GetUnitIds(1);
        for(int i=0; i < card.Length - 5; i++)
        {
            if (ids[i] == 1 || ids[i] == 2)
            {
                //material vom Boss zuweisen
                card[i].GetComponent<Renderer>().material = materials[0];
            }
            if (ids[i] == 3 || ids[i] == 4)
            {
                //material vom Diebin zuweisen
                card[i].GetComponent<Renderer>().material = materials[1];
            }
            if (ids[i] == 5 || ids[i] == 6)
            {
                //material vom Meuchelmörder zuweisen
                card[i].GetComponent<Renderer>().material = materials[2];
            }
            if (ids[i] == 7 || ids[i] == 8)
            {
                //material vom pestarzt zuweisen
                card[i].GetComponent<Renderer>().material = materials[3];
            }
            if (ids[i] == 9 || ids[i] == 10)
            {
                //material vom polizist zuweisen
                card[i].GetComponent<Renderer>().material = materials[4];
            }
            if (ids[i] == 11 || ids[i] == 12)
            {
                //material vom raufbold zuweisen
                card[i].GetComponent<Renderer>().material = materials[5];
            }
            if (ids[i] == 13 || ids[i] == 14)
            {
                //material vom scharfschuetze zuweisen
                card[i].GetComponent<Renderer>().material = materials[6];
            }
            if (ids[i] == 15 || ids[i] == 16)
            {
                //material vom schlaeger zuweisen
                card[i].GetComponent<Renderer>().material = materials[7];
            }
            if (ids[i] == 17 || ids[i] == 18)
            {
                //material vom taschendieb zuweisen
                card[i].GetComponent<Renderer>().material = materials[8];
            }
            if (ids[i] == 19 || ids[i] == 20)
            {
                //material vom tueftler zuweisen
                card[i].GetComponent<Renderer>().material = materials[9];
            }
        }

        ids = dbc.GetUnitIds(2);
        for(int i = 5; i < card.Length; i++)
        {
            if(ids[i-5] == 1 || ids[i - 5] == 2)
            {
                //material vom Boss zuweisen
                card[i].GetComponent<Renderer>().material = materials[0];
            }
            if (ids[i - 5] == 3 || ids[i - 5] == 4)
            {
                //material vom Diebin zuweisen
                card[i].GetComponent<Renderer>().material = materials[1];
            }
            if (ids[i - 5] == 5 || ids[i - 5] == 6)
            {
                //material vom Meuchelmörder zuweisen
                card[i].GetComponent<Renderer>().material = materials[2];
            }
            if (ids[i - 5] == 7 || ids[i - 5] == 8)
            {
                //material vom pestarzt zuweisen
                card[i].GetComponent<Renderer>().material = materials[3];
            }
            if (ids[i - 5] == 9 || ids[i - 5] == 10)
            {
                //material vom polizist zuweisen
                card[i].GetComponent<Renderer>().material = materials[4];
            }
            if (ids[i - 5] == 11 || ids[i - 5] == 12)
            {
                //material vom raufbold zuweisen
                card[i].GetComponent<Renderer>().material = materials[5];
            }
            if (ids[i - 5] == 13 || ids[i - 5] == 14)
            {
                //material vom scharfschuetze zuweisen
                card[i].GetComponent<Renderer>().material = materials[6];
            }
            if (ids[i - 5] == 15 || ids[i - 5] == 16)
            {
                //material vom schlaeger zuweisen
                card[i].GetComponent<Renderer>().material = materials[7];
            }
            if (ids[i - 5] == 17 || ids[i - 5] == 18)
            {
                //material vom taschendieb zuweisen
                card[i].GetComponent<Renderer>().material = materials[8];
            }
            if (ids[i - 5] == 19 || ids[i - 5] == 20)
            {
                //material vom tueftler zuweisen
                card[i].GetComponent<Renderer>().material = materials[9];
            }

        }
    }
}
