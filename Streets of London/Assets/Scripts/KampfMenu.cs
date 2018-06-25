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
 * 
 * Autor: Martin Schuster
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
    public GameObject pictureAtt;
    public GameObject pictureDef;

    //Hilfsvariablen
    int[] attackValues;
    int[] defenceValues;
    GameObject attacker;
    GameObject attField;
    GameObject defField;
    GameObject defender;
    int attDice; 
    int defDice;
    int distance; //distanz zwischen Angreifer und Verteidiger

    //Funktion die gestartet wird wenn der Kampfstartet
    public void KampfStart()
    {
        kampfButton.SetActive(false); // Deaktiviert den Kampfstart button
        stoppButton.SetActive(true); // Aktiviert den kampfstopp button
        InvokeRepeating("ChangeDiceTexture", 0f, 0.1f); // für alle 0.1s die ChangeDiceTexture Funktion aus
    }

    //Zurücksetzten der Werte beim beenden
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

    //Beim aufruf des Kampfmenüs wird diese Methode ausgeführt
    void Init()
    {
        //Angreifer
        //Setzt entsprechend der Werte die Hertzen, Angriffswürfel, Reichweitenwert, Geländebonuswert und das Bild des Angreifers 
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

        pictureAtt.GetComponent<RawImage>().texture = attacker.GetComponent<Renderer>().material.mainTexture;

        //Verteidiger
        //Setzt entsprechend der Werte die Hertzen, Verteidigungsswürfel, Reichweitenwert, Geländebonuswert und das Bild des Verteidigers 
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
        pictureDef.GetComponent<RawImage>().texture = defender.GetComponent<Renderer>().material.mainTexture;

        distance = Distance(attField, defField);//hilfsvariable ob verteidiger in reichweite ist um den angreifer schaden zuzufügen

        kampfButton.SetActive(true);
        stoppButton.SetActive(false);
        beendenButton.SetActive(false);
    }

    //Distance Funktion bestimmt die Distanz zwsichen Angreifer und verteidiger
    int Distance(GameObject unitfield, GameObject gegnerfield)
    {
        int maxxy = Math.Max(Math.Abs(unitfield.GetComponent<FieldHelper>().x - gegnerfield.GetComponent<FieldHelper>().x), Math.Abs(unitfield.GetComponent<FieldHelper>().y - gegnerfield.GetComponent<FieldHelper>().y));
        return Math.Max(maxxy, Math.Abs(unitfield.GetComponent<FieldHelper>().z - gegnerfield.GetComponent<FieldHelper>().z));
    }

    //Funktion um Kampfmenu anzuzeigen
    public void ShowKampfMenu()
    {
        km.SetActive(true);
        Init();
        gm.Paused();
    }

    //Setter für Angreifer
    public void SetAngreifer(GameObject unit)
    {
        this.attacker = unit;
    }

    //Setter für Angreiferfeld
    public void SetAttField(GameObject unitField)
    {
        this.attField = unitField;
    }

    //Setter für Verteidiger
    public void SetVerteidiger(GameObject gegner)
    {
        this.defender = gegner;
    }

    //Setter für Verteidigerfeld
    public void SetDefField(GameObject gegnerField)
    {
        this.defField = gegnerField;
    }

    //Setter für Distance
    public void SetDistance(int distance)
    {
        this.distance = distance;
    }

    //Funktion Blendet das Kampfmenu aus
    public void HideKampfMenu()
    {
        km.SetActive(false);
        gm.Continue();
    }
    
    //Wird alle 0.1s ausgeführt und ändert zufällig zwischen dem wert 1-6 den Wert und die Textur der Würfel
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

    //Beendet das ausführen der Zufälligen berechnung der Würfelwerte
    public void KampfStopp()
    {
        CancelInvoke();
        kampfButton.SetActive(false);
        stoppButton.SetActive(false);
        Ergebnis();
        beendenButton.SetActive(true);
    }

    //Wertet das Ergebnis des "Würfeln" aus
    void Ergebnis()
    {
        //Sortiert die würfel ergebnise von klein nach groß
        Array.Sort(attackValues);
        Array.Sort(defenceValues);
        //Hilfsvariablen wieviel Leben der Angreifer bzw. Verteidiger in dem Kampf verloren haben
        int lostLPAtk = 0;
        int lostLPDef = 0;
        //Funktion wenn die Angriffswürfel gleich der Verteidigungswürfel ist
        if (attDice == defDice)
        {
            for(int i = attDice-1; i >= 0; i--)
            {
                if (defenceValues[i] < attackValues[i])
                {
                    heartsDef[i].SetActive(false);
                    lostLPDef++;
                }
                else if(defenceValues[i] > attackValues[i])
                {
                    if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                    {
                        heartsAtk[i].SetActive(false);
                        lostLPAtk++;
                    }
                }
                else
                {
                    if(dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID)> dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostLPAtk++;
                        }
                    }
                    else if(dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsDef[i].SetActive(false);
                        lostLPDef++;
                    }
                    else
                    {
                        heartsDef[i].SetActive(false);
                        lostLPDef++;
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostLPAtk++;
                        }
                    }
                }
            }
        }
        //Funktion wenn die Angriffswürfel größer der Verteidigungswürfel ist
        else if (attDice > defDice)
        {
            int diff = attDice - defDice;
            for (int i = defDice-1; i >= 0; i--)
            {
                if (defenceValues[i] < attackValues[i+diff])
                {
                    heartsDef[i].SetActive(false);
                    lostLPDef++;
                }
                else if (defenceValues[i] > attackValues[i+diff] )
                {
                    if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                    {
                        heartsAtk[i].SetActive(false);
                        lostLPAtk++;
                    }
                }
                else
                {

                    if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) > dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostLPAtk++;
                        }
                    }
                    else if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsDef[i].SetActive(false);
                        lostLPDef++;
                    }
                    else
                    {
                        heartsDef[i].SetActive(false);
                        lostLPDef++;
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostLPAtk++;
                        }
                    }
                }
            }
        }
        //Funktion wenn die Angriffswürfel kleiner der Verteidigungswürfel ist
        else if(attDice < defDice)
        {
            int diff = defDice - attDice;
            for (int i = attDice-1; i >= 0; i--)
            {
                if (defenceValues[i + diff] < attackValues[i])
                {
                    heartsDef[i].SetActive(false);
                    lostLPDef++;
                }
                else if (defenceValues[i + diff] > attackValues[i])
                {
                    if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                    {
                        heartsAtk[i].SetActive(false);
                        lostLPAtk++;
                    }
                }
                else
                {
                    if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) > dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostLPAtk++;
                        }
                    }
                    else if (dbc.GetFieldBonus(defender.GetComponent<UnitHelper>().fieldID) < dbc.GetFieldBonus(attacker.GetComponent<UnitHelper>().fieldID))
                    {
                        heartsDef[i].SetActive(false);
                        lostLPDef++;
                    }
                    else
                    {
                        heartsDef[i].SetActive(false);
                        lostLPDef++;
                        if (distance <= dbc.GetRW(defender.GetComponent<UnitHelper>().unitID))
                        {
                            heartsAtk[i].SetActive(false);
                            lostLPAtk++;
                        }
                    }
                }
            }
        }
        //Wird ausgeführt wenn der Verteidiger den Angreifer Besiegt hatt Lp=0
        if ((dbc.GetLP(attacker.GetComponent<UnitHelper>().unitID) - lostLPAtk) <= 0)
        {
            //dbc.WriteToDB("Delete From Einheit Where ID = "+ attacker.GetComponent<UnitHelper>().unitID);
            mu.FightWinner(defender);
        }
        //Wird ausgeführt wenn der Angreifer den Verteidiger Besiegt hatt Lp=0
        else if ((dbc.GetLP(defender.GetComponent<UnitHelper>().unitID) - lostLPDef) <= 0)
        {
            //dbc.WriteToDB("Delete From Einheit Where ID = " + defender.GetComponent<UnitHelper>().unitID);
            mu.FightWinner(attacker);
        }
        //Ausführen wenn keiner der beiden Besiegt ist
        else
        {
            dbc.WriteToDB("Update Einheit SET Lebenspunkte = " + (dbc.GetLP(attacker.GetComponent<UnitHelper>().unitID) - lostLPAtk) + " Where ID = " + attacker.GetComponent<UnitHelper>().unitID);
            dbc.WriteToDB("Update Einheit SET Lebenspunkte = " + (dbc.GetLP(defender.GetComponent<UnitHelper>().unitID) - lostLPDef) + " Where ID = " + defender.GetComponent<UnitHelper>().unitID);
            mu.ResetKampfAnzeige();
            mu.Continue();
        }
    }
}
