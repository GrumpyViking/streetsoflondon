using UnityEngine;

/*
 * BankSkript
 * 
 * fügt dem Bankfeld nach jeder runde wenn keine Einheit auf dem Bankfeld steht eine GoldMünze hinzu
 * Prüft ob am runden Ende eine Einheit auf der Bank sich befindet und gibt dem Spieler dem die Einheit gehört den
 * vorhandenen Goldbetrag.
 * 
 * Autor: Martin Schuster
 */ 

public class Bank : MonoBehaviour {

    //Gold variable
    public int gold;
    //Das Bankfeld
    public GameObject bank;
    //Texturen für die Einzelnen Münzen
    public Texture[] bankGold;

    //Datenbank Skript
    public DataBaseController dbc;

    //Zu Beginn gold wird auf 0 gesetzt
    private void Start()
    {
        gold = 0;
    }

    // Wird vom GameManager aufgerufen am ende einer Runde wenn Einheit auf der Bank ist wird dem Entsprechenden Spieler
    // das Gold gutgeschrieben
    // und der Goldvorrat auf 0 gesetzt
    public void HasUnit()
    {
        if (bank.GetComponent<FieldHelper>().hasUnit)
        {
            int playerID = dbc.GetUnitPlayerID(bank.GetComponent<FieldHelper>().unitID);
            int goldNew = dbc.GoldPlayer(playerID) + gold;
            dbc.WriteToDB("Update Spieler Set Gold = " + goldNew + " Where ID=" + playerID + "");
            gold = 0;
            bank.GetComponent<Renderer>().material.mainTexture = bankGold[gold];
        }
        else
        {
            IncreaseGold();
        }
    }

    // Wird vom GameManager aufgerufen am ende einer Runde 
    // Jede Runde wo keine Einheit auf der Bank ist wird das Gold erhöht
    // und die Textur geändert
    public void IncreaseGold()
    {
        if(!bank.GetComponent<FieldHelper>().hasUnit && gold < 8)
        {
            gold++;
            bank.GetComponent<Renderer>().material.mainTexture = bankGold[gold];
        }
    }
}
