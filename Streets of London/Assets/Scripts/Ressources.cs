using System;
using UnityEngine;
using UnityEngine.UI;

public class Ressources : MonoBehaviour {

    public GameObject gold;             //GameObject der Gold-Anzeige
    public GameObject zusatzgold;       //GameObject der Zusatzgold-Anzeige

    public DataBaseController dbc;      //DataBaseController-Object zum Schreiben in die Datenbank

    public GameObject[] fields;         //Array für die einzelnen Geländefelder (Berechnung des Einkommens)

    int goldIncome = 0;                     //Attribut für das Einkommen aus Geschäftsfeldern und dem Investitions-Effekt
    int baseIncome = 1;                     //Attribut für das Grundeinkommen
    int activeInvestitionenSpieler1 = 0;    //Attribut für die Anzahl der aktiven Investitions-Effekte für Spieler 1
    int activeInvestitionenSpieler2 = 0;    //Attribut für die Anzahl der aktiven Investitions-Effekte für Spieler 2

    //Methode zur Aktualisierung des Goldwertes und Speicherung des neuen Goldwertes in der Datenbank
    public void AktualisiereGold(int playerID)
    {
        int goldNextRound=0;
        goldNextRound = dbc.GoldPlayer(playerID) + CalcIncome(playerID);
        dbc.WriteToDB("Update Spieler Set Gold = " + goldNextRound + " Where ID="+playerID+"");
    }

    //Methode zur Aktualisierung der Anzeige des Goldwertes und des Zusatzgoldes
    public void RefreshDisplay(int playerID)
    {
        gold.GetComponent<Text>().text = "Gold: " + Convert.ToString(dbc.GoldPlayer(playerID));
        zusatzgold.GetComponent<Text>().text = "+ " + CalcIncome(playerID); 
    }

    //Methode zur Berechnung des Einkommens in der nächsten Runde (Beachtung der Positionierung von Einheiten auf Geschäftsfeldern und Trickkarteneffekte)
    public int CalcIncome(int playerID)
    {
        goldIncome = 0;
        for(int i = 0; i < fields.Length; i++)
        {
            if (fields[i].GetComponent<FieldHelper>().hasUnit)
            {
                if (dbc.GetFieldName(fields[i].GetComponent<FieldHelper>().id).Equals("Geschäft 1"))
                {
                    if (dbc.GetUnitPlayerID(fields[i].GetComponent<FieldHelper>().unitID) == playerID)
                    {
                        goldIncome++;
                    }
                }

                if (dbc.GetFieldName(fields[i].GetComponent<FieldHelper>().id).Equals("Geschäft 2"))
                {
                    if (dbc.GetUnitPlayerID(fields[i].GetComponent<FieldHelper>().unitID) == playerID)
                    {
                        goldIncome += 2; 
                    }
                    
                }
            }
        }
        if (playerID == 1)
        {
            for (int k = 0; k < activeInvestitionenSpieler1; k++)
            {
                goldIncome += 3;
            }
        }
        else
        {
            for (int k = 0; k < activeInvestitionenSpieler2; k++)
            {
                goldIncome += 3;
            }
        }
        


        return (goldIncome+baseIncome);
    }
	
    //Methode zur Erhöhung der aktiven Investitions-Effekte
    public void IncreaseActiveInvestitionen()
    {
        if (PassthroughData.currentPlayer == 1)
        {
            activeInvestitionenSpieler1++;
        }
        else
        {
            activeInvestitionenSpieler2++;
        }
    }

    //Methode zur Verringerung der aktiven Investions-Effekte
    public void DecreaseActiveInvestitionen()
    {
        if (PassthroughData.currentPlayer == 1)
        {
            activeInvestitionenSpieler1--;
        }
        else
        {
            activeInvestitionenSpieler2--;
        }
    }
}
