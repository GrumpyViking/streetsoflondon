using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpielerMenu : MonoBehaviour {

    public GameObject spielerMenuScriptObject;
    public GameObject playerTextObject;
    public GameManager gm;

    int unitsSelected = 0;
    public UnitSelection us;

    bool init;
    private void Start()
    {
        init = false;
        if (PassthrougData.startPlayer == 0)
        {
            SetPlayer(PassthrougData.player1);
        }
        else
        {
            SetPlayer(PassthrougData.player2);
        }
        Debug.Log("test");
    }
    
    public void PanelState(bool state)
    {
        spielerMenuScriptObject.SetActive(state);

    }

    public void SetPlayer(string name)
    {
        playerTextObject.GetComponent<Text>().text = name;
    }

    public void StartGame()
    {
        if (unitsSelected < 1)
        {
            unitsSelected++;
            us.Auswahl();
        }
        else
        {           
                if (!init)
                {
                    gm.SetupScene();
                    init = true;
                }
                else
                {
                    gm.Continue();
                }
        }
    }

    public void ExitPorgram()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
