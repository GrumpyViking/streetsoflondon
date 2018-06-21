using UnityEngine;

/*
 * Ermöglicht zugriff auf grundlegende Informationen für andere Skripte
 * 
 * Autor Martin Schuster
 */
 
public class PassthroughData : MonoBehaviour {

    static public int startPlayer; //Beginnender Spieler
    static public string player1; //Name Spieler1
    static public string player2; //Name Spieler2
    static public bool gameActiv; //Läuft das Spiel
    static public int currentPlayer; //Spieler der Aktuell am zug ist
}
