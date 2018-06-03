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
    public GameObject beendenButton;
    public GameObject km;
    public GameObject[] heartsatk;
    public GameObject[] heartsdef;
    public GameManager gm;
    public DataBaseController dbc;
    public GameObject mu;
    public GameObject rwAtt;
    public GameObject rwDef;
    GameObject angreifer;
    GameObject verteidiger;
    int gewinner=2;

    private void Start()
    {
        
    }

    public void KampfStart()
    {
        kampfbutton.SetActive(false);
        stoppButton.SetActive(true);
        InvokeRepeating("ChangeDiceTexture", 0f, 0.1f);
    }

    void Init()
    {
        //Angreifer
        
        for(int i = 0; i < dbc.GetLP(angreifer.GetComponent<UnitHelper>().unitID); i++)
        {
            heartsatk[i].SetActive(true);
        }
        rwAtt.GetComponent<Text>().text = "RW " + Convert.ToString(dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID));

        for (int i = 0; i < dbc.GetAtt(angreifer.GetComponent<UnitHelper>().unitID); i++)
        {
            attackDice[i].SetActive(true);
            attackDice[i].GetComponent<DiceValue>().setDiceValue(1);
            attackDice[i].GetComponent<Image>().sprite = wuerfel[0];
        }

        //Verteidiger

        for (int i = 0; i < dbc.GetLP(verteidiger.GetComponent<UnitHelper>().unitID); i++)
        {
            heartsdef[i].SetActive(true);
        }
        rwDef.GetComponent<Text>().text = "RW " + Convert.ToString(dbc.GetRW(verteidiger.GetComponent<UnitHelper>().unitID));

        for (int i = 0; i < dbc.GetDef(verteidiger.GetComponent<UnitHelper>().unitID); i++)
        {
            defendDice[i].SetActive(true);
            defendDice[i].GetComponent<DiceValue>().setDiceValue(1);
            defendDice[i].GetComponent<Image>().sprite = wuerfel[0];
        }

        kampfbutton.SetActive(true);
        stoppButton.SetActive(false);
        beendenButton.SetActive(false);

    }

    public void ShowKampfMenu()
    {
        km.SetActive(true);
        Init();
        gm.Paused();
    }

    public void SetAngreifer(GameObject unit)
    {
        Debug.Log(unit);
        this.angreifer = unit;
    }
    public void SetVerteidiger(GameObject gegner)
    {
        Debug.Log(gegner);
        this.verteidiger = gegner;
    }

    public void HideKampfMenu()
    {
        km.SetActive(false);
        gm.Continue();
    }
    
    void ChangeDiceTexture()
    {
        attackvalues = new int[6];
        defencevalues = new int[6];

        int rnddice;
        int attdice = angreifer.GetComponent<UnitHelper>().unitID;
        for (int i = 0; i < attdice; i++)
        {
            rnddice = UnityEngine.Random.Range(1, 6);
            attackDice[i].SetActive(true);
            attackDice[i].GetComponent<DiceValue>().setDiceValue(rnddice);
            attackvalues[i] = rnddice;
            attackDice[i].GetComponent<Image>().sprite = wuerfel[rnddice];
        }

        int defdice = verteidiger.GetComponent<UnitHelper>().unitID;
        for (int i = 0; i < defdice; i++)
        {
            rnddice = UnityEngine.Random.Range(1, 6);
            defendDice[i].SetActive(true);
            defendDice[i].GetComponent<DiceValue>().setDiceValue(rnddice);
            defencevalues[i] = rnddice;
            defendDice[i].GetComponent<Image>().sprite = wuerfel[rnddice];
        }
        
    }

    public void KampfStopp()
    {
        CancelInvoke();
        kampfbutton.SetActive(false);
        stoppButton.SetActive(false);
        ergebnis();
        beendenButton.SetActive(true);
    }

    void ergebnis()
    {
        int lpa=0;
        int lpv=0;

        Array.Sort(attackvalues);
        Array.Sort(defencevalues);

        for (int i = 0; i < attackvalues.Length; i++)
        {
            if (defencevalues[i] > attackvalues[i])
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
    }
}
