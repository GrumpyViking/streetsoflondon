using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpielerMenu : MonoBehaviour {


    public GameObject spielerMenuScriptObject;
    public GameObject playerTextObject;
    public GameObject mainUI;


    public GameManager gm;
    public DataBaseController dbc;
    public UnitSelection us;
    public FieldBuilder fb;

    
    bool init;
    bool fieldBuild;
    int unitsSelected;

    //Initialisieren
    void Start()
    {
        init = false;
        fieldBuild = false;
        unitsSelected = 0;
        mainUI.SetActive(false);
        if (PassthroughData.currentPlayer == 1)
        {
            SetPlayer(PassthroughData.player1);
        }
        else
        {
            SetPlayer(PassthroughData.player2);
        }
    }
    
    //Anzeige des Panels
    public void PanelState(bool state)
    {
        spielerMenuScriptObject.SetActive(state);

    }

    //Name des Spielers setzen
    public void SetPlayer(string name)
    {
        playerTextObject.GetComponent<Text>().text = name;
    }

    public void CheckAction()
    {
        if (!fieldBuild)
        {
            FieldBuilder();
        }
        else
        {
            StartGame();
        }
    }

    public void SetFieldBuild(bool value)
    {
        fieldBuild = value;
    }

    void FieldBuilder()
    {
        fb.PanelState(true);
    }

    public void StartGame()
    {
        if (unitsSelected != 2)
        { 
            unitsSelected++;
            us.Auswahl();
            us.StartTimer();
        }
        else
        {
            if (!init)
            {
                gm.SetupScene();
                PassthroughData.gameActiv = true;
                init = true;
            }
            else
            {
                PassthroughData.gameActiv = true;
                gm.Continue();
            }
        }
    }

    //Beenden des Programms
    public void ExitPorgram()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        dbc.CleanDB();
    }
}
