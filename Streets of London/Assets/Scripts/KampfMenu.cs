using System;
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
    public MoveUnit mu;
    public GameObject rwAtt;
    public GameObject rwDef;
    public GameObject gwAtt;
    public GameObject gwDef;
    GameObject angreifer;
    GameObject verteidiger;
    int gewinner=2;
    int attdice;
    int defdice;

    private void Start()
    {
        
    }

    public void KampfStart()
    {
        kampfbutton.SetActive(false);
        stoppButton.SetActive(true);
        InvokeRepeating("ChangeDiceTexture", 0f, 0.1f);
    }

    public void Reset()
    {
        for(int i = 0; i < 6; i++)
        {
            attackDice[i].SetActive(false);
            defendDice[i].SetActive(false);
            heartsatk[i].SetActive(false);
            heartsdef[i].SetActive(false);
        }
        angreifer = null;
        verteidiger = null;
    }

    void Init()
    {
        //Angreifer
        
        for(int i = 0; i < dbc.GetLP(angreifer.GetComponent<UnitHelper>().unitID); i++)
        {
            heartsatk[i].SetActive(true);
        }
        rwAtt.GetComponent<Text>().text = "RW: " + Convert.ToString(dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID));
        gwAtt.GetComponent<Text>().text = "GW: " + Convert.ToString(dbc.GetFieldBonus(angreifer.GetComponent<UnitHelper>().fieldID));
        attdice = dbc.GetAtt(angreifer.GetComponent<UnitHelper>().unitID);
        for (int i = 0; i < attdice; i++)
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
        gwDef.GetComponent<Text>().text = "GW: " + Convert.ToString(dbc.GetFieldBonus(verteidiger.GetComponent<UnitHelper>().fieldID));
        defdice = dbc.GetDef(verteidiger.GetComponent<UnitHelper>().unitID);
        for (int i = 0; i < defdice; i++)
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
        attackvalues = new int[attdice];
        defencevalues = new int[defdice];

        int rnddice;
       
        for (int i = 0; i < attdice; i++)
        {
            rnddice = UnityEngine.Random.Range(1, 6);
            attackDice[i].SetActive(true);
            attackDice[i].GetComponent<DiceValue>().setDiceValue(rnddice);
            attackvalues[i] = rnddice;
            attackDice[i].GetComponent<Image>().sprite = wuerfel[rnddice];
        }

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
        Array.Sort(attackvalues);
        Array.Sort(defencevalues);

        int lostlpatk = 0;
        int lostlpdef = 0;

        if(attdice == defdice)
        {
            for(int i = attdice-1; i >= 0; i--)
            {
                if (defencevalues[i] < attackvalues[i])
                {
                    heartsdef[i].SetActive(false);
                    lostlpdef++;
                }
                else if(defencevalues[i] > attackvalues[i] && dbc.GetRW(verteidiger.GetComponent<UnitHelper>().unitID)>= dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID))
                {
                    heartsatk[i].SetActive(false);
                    lostlpatk++;
                }
                else
                {
                    if(dbc.GetFieldBonus(verteidiger.GetComponent<UnitHelper>().fieldID)> dbc.GetFieldBonus(angreifer.GetComponent<UnitHelper>().fieldID) && dbc.GetRW(verteidiger.GetComponent<UnitHelper>().unitID) >= dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID))
                    {
                        heartsatk[i].SetActive(false);
                        lostlpatk++;
                    }else if(dbc.GetFieldBonus(verteidiger.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(angreifer.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                    }
                    else
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                        if(dbc.GetRW(verteidiger.GetComponent<UnitHelper>().unitID) >= dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID))
                        {
                            heartsatk[i].SetActive(false);
                            lostlpatk++;
                        }
                        
                    }
                    
                }
            }
        }
        else if(attdice > defdice)
        {
            int diff = attdice - defdice;
            for (int i = defdice-1; i >= 0; i--)
            {
                if (defencevalues[i] < attackvalues[i+diff])
                {
                    heartsdef[i].SetActive(false);
                    lostlpdef++;
                }
                else if (defencevalues[i] > attackvalues[i+diff] && dbc.GetRW(verteidiger.GetComponent<UnitHelper>().unitID) >= dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID))
                {
                    heartsatk[i].SetActive(false);
                    lostlpatk++;
                }
                else
                {

                    if (dbc.GetFieldBonus(verteidiger.GetComponent<UnitHelper>().fieldID) > dbc.GetFieldBonus(angreifer.GetComponent<UnitHelper>().fieldID) && dbc.GetRW(verteidiger.GetComponent<UnitHelper>().unitID) >= dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID))
                    {
                        heartsatk[i].SetActive(false);
                        lostlpatk++;
                    }
                    else if (dbc.GetFieldBonus(verteidiger.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(angreifer.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                    }
                    else
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                        if(dbc.GetRW(verteidiger.GetComponent<UnitHelper>().unitID) >= dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID))
                        {
                            heartsatk[i].SetActive(false);
                            lostlpatk++;
                        }
                        
                    }
                }
            }
        }
        else if(attdice < defdice)
        {
            int diff = defdice - attdice;
            for (int i = attdice - 1; i >= 0; i--)
            {
                if (defencevalues[i + diff] < attackvalues[i])
                {
                    heartsdef[i].SetActive(false);
                    lostlpdef++;
                }
                else if (defencevalues[i + diff] > attackvalues[i] && dbc.GetRW(verteidiger.GetComponent<UnitHelper>().unitID) >= dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID))
                {
                    heartsatk[i].SetActive(false);
                    lostlpatk++;
                }
                else
                {
                    if (dbc.GetFieldBonus(verteidiger.GetComponent<UnitHelper>().fieldID) > dbc.GetFieldBonus(angreifer.GetComponent<UnitHelper>().fieldID) && dbc.GetRW(verteidiger.GetComponent<UnitHelper>().unitID) >= dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID))
                    {
                        heartsatk[i].SetActive(false);
                        lostlpatk++;
                    }
                    else if (dbc.GetFieldBonus(verteidiger.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(angreifer.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                    }
                    else
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                        if(dbc.GetRW(verteidiger.GetComponent<UnitHelper>().unitID) >= dbc.GetRW(angreifer.GetComponent<UnitHelper>().unitID))
                        {
                            heartsatk[i].SetActive(false);
                            lostlpatk++;
                        }
                        
                    }
                }
            }
        }
        if ((dbc.GetLP(angreifer.GetComponent<UnitHelper>().unitID) - lostlpatk) <= 0)
        {
            dbc.WriteToDB("Delete From Einheit Where ID = "+ angreifer.GetComponent<UnitHelper>().unitID);
            mu.FightWinner(verteidiger);
        }
        else if ((dbc.GetLP(verteidiger.GetComponent<UnitHelper>().unitID) - lostlpdef) <= 0)
        {
            dbc.WriteToDB("Delete From Einheit Where ID = " + verteidiger.GetComponent<UnitHelper>().unitID);
            mu.FightWinner(angreifer);
        }
        else
        {
            dbc.WriteToDB("Update Einheit SET Lebenspunkte = " + (dbc.GetLP(angreifer.GetComponent<UnitHelper>().unitID) - lostlpatk) + " Where ID = " + angreifer.GetComponent<UnitHelper>().unitID);
            dbc.WriteToDB("Update Einheit SET Lebenspunkte = " + (dbc.GetLP(verteidiger.GetComponent<UnitHelper>().unitID) - lostlpdef) + " Where ID = " + verteidiger.GetComponent<UnitHelper>().unitID);

            mu.Continue();
        }
    }
}
