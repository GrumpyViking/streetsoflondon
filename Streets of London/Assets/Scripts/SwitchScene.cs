using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * SwitchScene Script
 * 
 * Funktion zuw Szenen wechsel mittels Asynchronenladen
 *  - Szene von der gewechselt wird bleibt Aktiv bis Ladevorgang abgeschlossen ist
 *  
 * Autor: Martin Schuster
 */ 

public class SwitchScene : MonoBehaviour {

    //UI Objekte die Ladevortschritt anzeigen
    public GameObject loadingScreen;
    public Slider slider;
    
    //Funktion wird aufgerufen mit der ID der Szene die gestartet werden soll 
    public void ChangeScene(int sceneID)
    {
        StartCoroutine(LoadAsynchronously(sceneID));//Koroutinen ermöglichen es während der Ausführung Rückmeldungen zum gesamten Programm zu geben
    }

    //IEnumerator ist der Rückgabetyp für Koroutinen 
    IEnumerator LoadAsynchronously(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID); //Variable operation beinhaltet vielzahl der werte die während der AsyncOperation anfallen
        loadingScreen.SetActive(true); //Aktiviert die Anzeige der Ladeanzeige

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f); // Standard ist in unity der Wert operation.progress von 0-0.9 dadurch umwandlung zu 0-1
            slider.value = progress; // anzeige Ladestand
            yield return null; // das fortsetzen der while-schleife wird bis zum nächsten Frame ausgesetzt
        }
    }
}
