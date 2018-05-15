using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ressources : MonoBehaviour {

    public GameObject gold;
    public GameObject zusatzgold;
    public DataBaseController dbc;

    int goldIncome = 0;

    public void AktualisiereGold(int playerID)
    {
        gold.GetComponent<Text>().text = "Gold: " + dbc.RequestFromDB("Select Gold from Spieler where ID = '" + playerID + "'");
        zusatzgold.GetComponent<Text>().text = "+ " + CalcIncome(playerID);
    }

    public int CalcIncome(int playerID)
    {
        return goldIncome;
    }
	
}
