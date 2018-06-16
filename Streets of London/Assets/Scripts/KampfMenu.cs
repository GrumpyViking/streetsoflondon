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
    GameObject attacker;
    GameObject defField;
    GameObject attField;
    GameObject defender;
    int gewinner=2;
    int attdice;
    int defdice;
    int distance;

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
        attacker = null;
        defender = null;
    }

    void Init()
    {
        //Angreifer
        
        for(int i = 0; i < dbc.GetLP(attacker.GetComponent<UnitHelper>().unitID); i++)
        {
            heartsatk[i].SetActive(true);
        }
        rwAtt.GetComponent<Text>().text = "RW: " + Convert.ToString(dbc.GetRW(attacker.GetComponent<UnitHelper>().unitID));
        gwAtt.GetComponent<Text>().text = "GW: " + Convert.ToString(dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID));
        attdice = dbc.GetAtt(attacker.GetComponent<UnitHelper>().unitID);
        for (int i = 0; i < attdice; i++)
        {
            attackDice[i].SetActive(true);
            attackDice[i].GetComponent<DiceValue>().setDiceValue(1);
            attackDice[i].GetComponent<Image>().sprite = wuerfel[0];
        }

        //Verteidiger

        for (int i = 0; i < dbc.GetLP(defender.GetComponent<UnitHelper>().unitID); i++)
        {
            heartsdef[i].SetActive(true);
        }
        rwDef.GetComponent<Text>().text = "RW " + Convert.ToString(dbc.GetRW(defender.GetComponent<UnitHelper>().unitID));
        gwDef.GetComponent<Text>().text = "GW: " + Convert.ToString(dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID));
        defdice = dbc.GetDef(defender.GetComponent<UnitHelper>().unitID);
        for (int i = 0; i < defdice; i++)
        {
            defendDice[i].SetActive(true);
            defendDice[i].GetComponent<DiceValue>().setDiceValue(1);
            defendDice[i].GetComponent<Image>().sprite = wuerfel[0];
        }

        distance = Distance(attField, defField);

        kampfbutton.SetActive(true);
        stoppButton.SetActive(false);
        beendenButton.SetActive(false);

    }

    int Distance(GameObject unitfield, GameObject gegnerfield)
    {
        int maxxy = Math.Max(Math.Abs(unitfield.GetComponent<FieldHelper>().x - gegnerfield.GetComponent<FieldHelper>().x), Math.Abs(unitfield.GetComponent<FieldHelper>().y - gegnerfield.GetComponent<FieldHelper>().y));
        return Math.Max(maxxy, Math.Abs(unitfield.GetComponent<FieldHelper>().z - gegnerfield.GetComponent<FieldHelper>().z));
    }

    public void ShowKampfMenu()
    {
        km.SetActive(true);
        Init();
        gm.Paused();
    }

    public void SetAngreifer(GameObject unit)
    {
        this.attacker = unit;
    }

    public void SetAttField(GameObject unitField)
    {
        this.attField = unitField;
    }

    public void SetVerteidiger(GameObject gegner)
    {
        this.defender = gegner;
    }

    public void SetDefField(GameObject gegnerField)
    {
        this.defField = gegnerField;
    }

    public void SetDistance(int distance)
    {
        this.distance = distance;
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
                else if(defencevalues[i] > attackvalues[i])
                {
                    if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                    {
                        heartsatk[i].SetActive(false);
                        lostlpatk++;
                    }
                    
                }
                else
                {
                    if(dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID)> dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsatk[i].SetActive(false);
                            lostlpatk++;
                        }
                    }
                    else if(dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                    }
                    else
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
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
                else if (defencevalues[i] > attackvalues[i+diff] )
                {
                    if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                    {
                        heartsatk[i].SetActive(false);
                        lostlpatk++;
                    }
                }
                else
                {

                    if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) > dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsatk[i].SetActive(false);
                            lostlpatk++;
                        }
                    }
                    else if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                    }
                    else
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
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
                else if (defencevalues[i + diff] > attackvalues[i])
                {
                    if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                    {
                        heartsatk[i].SetActive(false);
                        lostlpatk++;
                    }
                }
                else
                {
                    if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) > dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsatk[i].SetActive(false);
                            lostlpatk++;
                        }
                    }
                    else if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                    }
                    else
                    {
                        heartsdef[i].SetActive(false);
                        lostlpdef++;
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsatk[i].SetActive(false);
                            lostlpatk++;
                        }
                    }
                }
            }
        }
        if ((dbc.GetLP(attacker.GetComponent<UnitHelper>().unitID) - lostlpatk) <= 0)
        {
            dbc.WriteToDB("Delete From Einheit Where ID = "+ attacker.GetComponent<UnitHelper>().unitID);
            mu.FightWinner(defender);
        }
        else if ((dbc.GetLP(defender.GetComponent<UnitHelper>().unitID) - lostlpdef) <= 0)
        {
            dbc.WriteToDB("Delete From Einheit Where ID = " + defender.GetComponent<UnitHelper>().unitID);
            mu.FightWinner(attacker);
        }
        else
        {
            dbc.WriteToDB("Update Einheit SET Lebenspunkte = " + (dbc.GetLP(attacker.GetComponent<UnitHelper>().unitID) - lostlpatk) + " Where ID = " + attacker.GetComponent<UnitHelper>().unitID);
            dbc.WriteToDB("Update Einheit SET Lebenspunkte = " + (dbc.GetLP(defender.GetComponent<UnitHelper>().unitID) - lostlpdef) + " Where ID = " + defender.GetComponent<UnitHelper>().unitID);

            mu.Continue();
        }
    }
}
