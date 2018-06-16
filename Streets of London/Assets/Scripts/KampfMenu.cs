using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * KampfMenu Skript
 * 
 * Zuständig für die Anzeige des Kampfmenüs
 * Das setzen der Würfel für die Angriffs und Verteidigungswerte
 * Die Herzen für die Lebenspunkte
 * 
 * Das setzten der Reichweite und des Geländebonus
 */
 
public class KampfMenu : MonoBehaviour {

    //Skripte
    public MoveUnit mu;
    public GameManager gm;
    public DataBaseController dbc;

    //Beeinflussbare Spielobjekte
    public GameObject km;
    public GameObject kampfButton;
    public GameObject stoppButton;
    public GameObject beendenButton;
    public GameObject[] attackDice;
    public Sprite[] wuerfel;
    public GameObject[] defendDice;
    public GameObject[] heartsAtk;
    public GameObject[] heartsDef;
    public GameObject rwAtt;
    public GameObject rwDef;
    public GameObject gwAtt;
    public GameObject gwDef;

    //Hilfsvariablen
    int[] attackValues;
    int[] defenceValues;
    GameObject attacker;
    GameObject attField;
    GameObject defField;
    GameObject defender;
    int gewinner=2;
    int attDice; 
    int defDice;
    int distance; //distanz zwischen Angreifer und Verteidiger

    public void KampfStart()
    {
        kampfButton.SetActive(false);
        stoppButton.SetActive(true);
        InvokeRepeating("ChangeDiceTexture", 0f, 0.1f);
    }

    //Zurücksetzten der Werte
    public void Reset()
    {
        for(int i = 0; i < 6; i++)
        {
            attackDice[i].SetActive(false);
            defendDice[i].SetActive(false);
            heartsAtk[i].SetActive(false);
            heartsDef[i].SetActive(false);
        }
        attacker = null;
        defender = null;
    }

    void Init()
    {
        //Angreifer
        
        for(int i = 0; i < dbc.GetLP(attacker.GetComponent<UnitHelper>().unitID); i++)
        {
            heartsAtk[i].SetActive(true);
        }
        rwAtt.GetComponent<Text>().text = "RW: " + Convert.ToString(dbc.GetRW(attacker.GetComponent<UnitHelper>().unitID));
        gwAtt.GetComponent<Text>().text = "GW: " + Convert.ToString(dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID));
        attDice = dbc.GetAtt(attacker.GetComponent<UnitHelper>().unitID);
        for (int i = 0; i < attDice; i++)
        {
            attackDice[i].SetActive(true);
            attackDice[i].GetComponent<DiceValue>().setDiceValue(1);
            attackDice[i].GetComponent<Image>().sprite = wuerfel[0];
        }

        //Verteidiger

        for (int i = 0; i < dbc.GetLP(defender.GetComponent<UnitHelper>().unitID); i++)
        {
            heartsDef[i].SetActive(true);
        }
        rwDef.GetComponent<Text>().text = "RW " + Convert.ToString(dbc.GetRW(defender.GetComponent<UnitHelper>().unitID));
        gwDef.GetComponent<Text>().text = "GW: " + Convert.ToString(dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID));
        defDice = dbc.GetDef(defender.GetComponent<UnitHelper>().unitID);
        for (int i = 0; i < defDice; i++)
        {
            defendDice[i].SetActive(true);
            defendDice[i].GetComponent<DiceValue>().setDiceValue(1);
            defendDice[i].GetComponent<Image>().sprite = wuerfel[0];
        }

        distance = Distance(attField, defField);

        kampfButton.SetActive(true);
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
        attackValues = new int[attDice];
        defenceValues = new int[defDice];

        int rnddice;
       
        for (int i = 0; i < attDice; i++)
        {
            rnddice = UnityEngine.Random.Range(1, 6);
            attackDice[i].SetActive(true);
            attackDice[i].GetComponent<DiceValue>().setDiceValue(rnddice);
            attackValues[i] = rnddice;
            attackDice[i].GetComponent<Image>().sprite = wuerfel[rnddice];
        }

        for (int i = 0; i < defDice; i++)
        {
            rnddice = UnityEngine.Random.Range(1, 6);
            defendDice[i].SetActive(true);
            defendDice[i].GetComponent<DiceValue>().setDiceValue(rnddice);
            defenceValues[i] = rnddice;
            defendDice[i].GetComponent<Image>().sprite = wuerfel[rnddice];
        }
        
    }

    public void KampfStopp()
    {
        CancelInvoke();
        kampfButton.SetActive(false);
        stoppButton.SetActive(false);
        ergebnis();
        beendenButton.SetActive(true);
    }

    void ergebnis()
    {
        Array.Sort(attackValues);
        Array.Sort(defenceValues);

        int lostlpatk = 0;
        int lostlpdef = 0;

        if(attDice == defDice)
        {
            for(int i = attDice-1; i >= 0; i--)
            {
                if (defenceValues[i] < attackValues[i])
                {
                    heartsDef[i].SetActive(false);
                    lostlpdef++;
                }
                else if(defenceValues[i] > attackValues[i])
                {
                    if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                    {
                        heartsAtk[i].SetActive(false);
                        lostlpatk++;
                    }
                    
                }
                else
                {
                    if(dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID)> dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostlpatk++;
                        }
                    }
                    else if(dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsDef[i].SetActive(false);
                        lostlpdef++;
                    }
                    else
                    {
                        heartsDef[i].SetActive(false);
                        lostlpdef++;
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostlpatk++;
                        }
                    }
                    
                }
            }
        }
        else if(attDice > defDice)
        {
            int diff = attDice - defDice;
            for (int i = defDice-1; i >= 0; i--)
            {
                if (defenceValues[i] < attackValues[i+diff])
                {
                    heartsDef[i].SetActive(false);
                    lostlpdef++;
                }
                else if (defenceValues[i] > attackValues[i+diff] )
                {
                    if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                    {
                        heartsAtk[i].SetActive(false);
                        lostlpatk++;
                    }
                }
                else
                {

                    if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) > dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostlpatk++;
                        }
                    }
                    else if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsDef[i].SetActive(false);
                        lostlpdef++;
                    }
                    else
                    {
                        heartsDef[i].SetActive(false);
                        lostlpdef++;
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostlpatk++;
                        }


                    }
                }
            }
        }
        else if(attDice < defDice)
        {
            int diff = defDice - attDice;
            for (int i = attDice-1; i >= 0; i--)
            {
                if (defenceValues[i + diff] < attackValues[i])
                {
                    heartsDef[i].SetActive(false);
                    lostlpdef++;
                }
                else if (defenceValues[i + diff] > attackValues[i])
                {
                    if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                    {
                        heartsAtk[i].SetActive(false);
                        lostlpatk++;
                    }
                }
                else
                {
                    if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) > dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostlpatk++;
                        }
                    }
                    else if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsDef[i].SetActive(false);
                        lostlpdef++;
                    }
                    else
                    {
                        heartsDef[i].SetActive(false);
                        lostlpdef++;
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
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
