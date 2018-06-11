using System;
using UnityEngine;
using UnityEngine.UI;

public class Ressources : MonoBehaviour {

    public GameObject gold;
    public GameObject zusatzgold;
    public DataBaseController dbc;
    public GameObject[] fields;

    int goldIncome = 0;
    int baseIncome = 1;

    public void AktualisiereGold(int playerID)
    {
        int goldNextRound=0;
        goldNextRound = dbc.GoldPlayer(playerID) + CalcIncome(playerID);
        dbc.WriteToDB("Update Spieler Set Gold = " + goldNextRound + " Where ID="+playerID+"");
    }

    public void RefreshDisplay(int playerID)
    {
        gold.GetComponent<Text>().text = "Gold: " + Convert.ToString(dbc.GoldPlayer(playerID));
        zusatzgold.GetComponent<Text>().text = "+ " + CalcIncome(playerID);
    }

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

        return (goldIncome+baseIncome);
    }
	
}
