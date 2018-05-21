using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KampfMenu : MonoBehaviour {

    public Sprite[] wuerfel;
    public GameObject[] attackDice;
    public GameObject[] defendDice;
    public GameObject kampfbutton;
    public GameObject stoppButton;
    public GameObject km;
    public GameObject[] heartsatk;
    public GameObject[] heartsdef;


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
    }

    
    void ChangeDiceTexture()
    { 

        int rnddice;
        for(int i = 0; i < attackDice.Length; i++)
        {
            rnddice = Random.Range(0, 5);
            attackDice[i].GetComponent<DiceValue>().setDiceValue(rnddice);
            attackDice[i].GetComponent<Image>().sprite = wuerfel[rnddice];
        }
        for (int i = 0; i < defendDice.Length; i++)
        {
            rnddice = Random.Range(0, 5);
            defendDice[i].GetComponent<DiceValue>().setDiceValue(rnddice);
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
        int tempatk=0;
        int tempdef=0;

        for(int i = 0; i < attackDice.Length; i++)
        {
           if(attackDice[i].GetComponent<DiceValue>().getDiceValue() > tempatk)
           {
                tempatk = attackDice[i].GetComponent<DiceValue>().getDiceValue();
           }
        }

        for (int i = 0; i <defendDice.Length; i++)
        {
            if (defendDice[i].GetComponent<DiceValue>().getDiceValue() > tempatk)
            {
                tempdef = attackDice[i].GetComponent<DiceValue>().getDiceValue();
            }
        }

        if(tempdef >= tempatk)
        {
            heartsatk[0].SetActive(false);
        }
        else
        {
            heartsdef[0].SetActive(false);
        }


    }
}
