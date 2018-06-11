using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabrik : MonoBehaviour {

    public int fabrikLPL;
    public int fabrikLPR;
    public GameObject fabrikL;
    public GameObject fabrikR;
    public Texture[] fabrikDamage;
    public GameManager gm;

	// Use this for initialization
	void Start () {
        fabrikLPL = 10;
        fabrikLPR = 10;
    }

    public void SetLPFabrikL(int damage)
    {
        this.fabrikLPL = this.fabrikLPL - damage;
        if (fabrikLPL <= 0)
        {
            gm.GameOver();
            fabrikLPL = 0;
        }
        fabrikL.GetComponent<Renderer>().material.mainTexture = fabrikDamage[fabrikLPL];
        
    }

    public void SetLPFabrikR(int damage)
    {
        this.fabrikLPR = this.fabrikLPR - damage;
        if (fabrikLPR <= 0)
        {
            gm.GameOver();
            fabrikLPR = 0;

        }
        fabrikR.GetComponent<Renderer>().material.mainTexture = fabrikDamage[fabrikLPR];
        
    }



}
