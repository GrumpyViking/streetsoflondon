using UnityEngine;
using TMPro;

/*
 * GameOver Skript zeigt bei erreichter Siegbedingung den Gewinner an.
 */ 

public class GameOver : MonoBehaviour {

    //Stellt das TextObject auf der Gewinner anzeige zur verfügung
    public GameObject winnerText;

    //Wird das GameOver fenster aktiviert wird der Gewinner ermittelt
    private void OnEnable()
    {
        SetWinnerText();
    }

    //Ermittelt den Gewinner durch das Abfragen welcher Spieler zurzeit an der Reihe ist
    public void SetWinnerText()
    {
        if(PassthroughData.currentPlayer == 1)
        {
            winnerText.GetComponent<TextMeshProUGUI>().text = PassthroughData.player1;
        }
        else
        {
            winnerText.GetComponent<TextMeshProUGUI>().text = PassthroughData.player2;
        }
    }
}
