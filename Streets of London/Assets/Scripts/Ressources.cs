using System;
using UnityEngine;
using UnityEngine.UI;

public class Ressources : MonoBehaviour {

    public GameObject gold;
    public GameObject zusatzgold;
    public DataBaseController dbc;

    int goldIncome = 0;
    int baseIncome = 1;

    public void AktualisiereGold(int playerID)
    {
        int goldNextRound=0;
        RefreshDisplay(playerID);
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
        //prüfen ob geschäftsfelder von einheiten besetzt sind


        return goldIncome+baseIncome;
    }
	
}
