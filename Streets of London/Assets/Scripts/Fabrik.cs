using UnityEngine;

/*
 * Fabrik Skript
 * Verwaltet die Lebenspunkte der Beiden Fabriken
 * 
 * Autor: Martin Schuster
 */ 

public class Fabrik : MonoBehaviour {

    //Beide Fabriken
    public GameObject fabrikL;
    public GameObject fabrikR;
    
    //GameManger Skript zum Aufruf des GameOver Bildschirms
    public GameManager gm;
    
    //Variablen für die Lebenspunkte der Fabriken
    public int fabrikLPL;
    public int fabrikLPR;

    //Texturen mit den Schadenswerten
    public Texture[] fabrikDamage;
    

	// Bei Spielstart beide Fabriken volle Lebenspunkte
	void Start () {
        fabrikLPL = 10;
        fabrikLPR = 10;
    }

    // Wird bei einem Angriff auf die Fabrik des Spieler 1 aufgerufen
    // der Schaden wird entsprechend von den gesamt Lebenspunkten abgezogen und die Texture geändert
    // es wird geprüft ob Fabrik zerstört ist
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

    // Wird bei einem Angriff auf die Fabrik des Spieler 2 aufgerufen
    // der Schaden wird entsprechend von den gesamt Lebenspunkten abgezogen und die Texture geändert
    // es wird geprüft ob Fabrik zerstört ist
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
