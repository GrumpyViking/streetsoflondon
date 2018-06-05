using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour {
    public GameObject winnerText;

    private void OnEnable()
    {
        SetWinnerText();
    }
    public void SetWinnerText()
    {
        if(PassthrougData.currentPlayer == 1)
        {
            winnerText.GetComponent<TextMeshProUGUI>().text = PassthrougData.player1;
        }
        else
        {
            winnerText.GetComponent<TextMeshProUGUI>().text = PassthrougData.player2;
        }
    }
}
