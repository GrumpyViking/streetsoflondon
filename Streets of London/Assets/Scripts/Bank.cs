using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour {

    public int gold;
    public GameObject bank;

    public DataBaseController dbc;


    private void Start()
    {
        gold = 0;
    }

    public void HasUnit()
    {
        if (bank.GetComponent<FieldHelper>().hasUnit)
        {
            int playerID = dbc.GetUnitPlayerID(bank.GetComponent<FieldHelper>().unitID);
            int goldNew = dbc.GoldPlayer(playerID) + gold;
            dbc.WriteToDB("Update Spieler Set Gold = " + goldNew + " Where ID=" + playerID + "");
            gold = 0;
        }
    }

    public void IncreaseGold()
    {
        if(!bank.GetComponent<FieldHelper>().hasUnit && gold < 8)
        {
            gold++;
        }
    }
}
