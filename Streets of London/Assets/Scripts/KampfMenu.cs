using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KampfMenu : MonoBehaviour {

    public Sprite[] wuerfel;
    public GameObject[] attackDice;
    int[] attackvalues;
    int[] defencevalues;
    public GameObject[] defendDice;
    public GameObject kampfbutton;
    public GameObject stoppButton;
    public GameObject km;
    public GameObject[] heartsatk;
    public GameObject[] heartsdef;
    public GameManager gm;
    public GameObject mu;
    GameObject angreifer;
    GameObject verteidiger;
    int gewinner=2;


    private void Start()
    {
        kampfbutton.SetActive(true);
        stoppButton.SetActive(false);
    }

    public void KampfStart()
    {
        kampfbutton.SetActive(false);
        stoppButton.SetActive(true);
        InvokeRepeating("ChangeDiceTexture", 0f, 0.1f);
    }

    public void ShowKampfMenu()
    {
        km.SetActive(true);
        gm.Paused();
    }
    
    void ChangeDiceTexture()
    {
        attackvalues = new int[6];
        defencevalues = new int[6];

        int rnddice;
        for(int i = 0; i < attackDice.Length; i++)
        {
            rnddice = UnityEngine.Random.Range(1, 6);
            attackDice[i].GetComponent<DiceValue>().setDiceValue(rnddice);
            attackvalues[i] = rnddice;
            attackDice[i].GetComponent<Image>().sprite = wuerfel[rnddice];
        }
        
        for (int i = 0; i < defendDice.Length; i++)
        {
            rnddice = UnityEngine.Random.Range(1, 6);
            defendDice[i].GetComponent<DiceValue>().setDiceValue(rnddice);
            defencevalues[i] = rnddice;
            defendDice[i].GetComponent<Image>().sprite = wuerfel[rnddice];
        }
        
    }

    public void KampfStopp()
    {
        CancelInvoke();
        kampfbutton.SetActive(true);
        stoppButton.SetActive(false);
        ergebnis();
    }

    void ergebnis()
    {
        int lpa=0;
        int lpv=0;

        Array.Sort(attackvalues);
        Array.Sort(defencevalues);

        for(int i = 0; i < attackvalues.Length; i++)
        {
            if (defencevalues[i] >= attackvalues[i])
            {
                heartsatk[i].SetActive(false);
                lpa++;
            }
            else
            {
                heartsdef[i].SetActive(false);
                lpv++;
            }
        }

        if (lpa <= lpv)
        {
            
        }
        else
        {
            gewinner = 1;
        }
    }
}
