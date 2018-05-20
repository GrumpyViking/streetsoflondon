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

    
    void ChangeDiceTexture()
    { 

        int rnddice;
        for(int i = 0; i < attackDice.Length; i++)
        {
            rnddice = Random.Range(0, 5);
            attackDice[i].GetComponent<Image>().sprite = wuerfel[rnddice];
        }
        for (int i = 0; i < defendDice.Length; i++)
        {
            rnddice = Random.Range(0, 5);
            defendDice[i].GetComponent<Image>().sprite = wuerfel[rnddice];
        }
    }

    public void KampfStopp()
    {
        CancelInvoke();
        kampfbutton.SetActive(true);
        stoppButton.SetActive(false);
    }
}
