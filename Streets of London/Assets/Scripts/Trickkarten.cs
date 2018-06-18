using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="neue Trickkarte", menuName ="Trickkarte" )]
public class Trickkarten : MonoBehaviour {

    public string nameTK;
    public string wirkungTK;
    public string effekt; 
    public Sprite wirkungsbereich;
    public Sprite trickkartenbild;

    public string[] activeTKSpieler1 = { null, null, null, null, null, null, null, null };
    public string[] activeTKSpieler2 = { null, null, null, null, null, null, null, null };
    public int[] activeTKDauer1 = { 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] activeTKDauer2 = { 0, 0, 0, 0, 0, 0, 0, 0 };

    public Ressources rsc;

    public GameObject trickkartenMenu;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;
    public GameObject slot6;
    public GameObject slot7;
    public GameObject slot8;

    public GameObject name1;
    public GameObject name2;
    public GameObject name3;
    public GameObject name4;
    public GameObject name5;
    public GameObject name6;
    public GameObject name7;
    public GameObject name8;

    public GameObject desc1;
    public GameObject desc2;
    public GameObject desc3;
    public GameObject desc4;
    public GameObject desc5;
    public GameObject desc6;
    public GameObject desc7;
    public GameObject desc8;

    public void OeffneTrickkartenMenu()
    {
        
        trickkartenMenu.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                if (TKPicker.player1TK[i] == " ")
                {
                    showTKSlots(i);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                if (TKPicker.player2TK[i] == " ")
                {
                    showTKSlots(i);
                    break;
                }
            }
        }
    }

    public void SchliesseTrickkartenMenu()
    {
        trickkartenMenu.SetActive(false);
        slot1.SetActive(false);
        slot2.SetActive(false);
        slot3.SetActive(false);
        slot4.SetActive(false);
        slot5.SetActive(false);
        slot6.SetActive(false);
        slot7.SetActive(false);
        slot8.SetActive(false);
    }

    //-------------------------------------------------------------------------
    //Anzeige der Kartendetails
    public void initializeTKDetails(string name)
    {
        switch (name)
        {
            case "Fusel":
                nameTK = name;
                wirkungTK = "Erhöhe den AW einer EInheit um 1 für 2 Runden.";
                break;
            case "Infektion":
                nameTK = name;
                wirkungTK = "Alle gegnerischen Einheiten im Wirkungsbereich verlieren 2 LP.";
                break;
            case "Verstärkter Mantel":
                nameTK = name;
                wirkungTK = "Erhöhe den VW einer Einheit um 1 für 2 Runden";
                break;
            case "Ration":
                nameTK = name;
                wirkungTK = "Heile alle Verbündeten im Wirkungsbereich um 2 LP.";
                break;
            case "Sabotage":
                nameTK = name;
                wirkungTK = "Beende den Effekt einer beliebigen, aktiven Trickkarte (auch Sabotage).";
                break;
            case "Investition":
                nameTK = name;
                wirkungTK = "Erhalte 3 Gold pro Runde für 3 Runden. Nach jeder Runde wird ein Schadensmarker als Rundenzähler auf die Karte gelegt.";
                break;
            case "Verführung":
                nameTK = name;
                wirkungTK = "Übernimm für eine Runde die Kontrolle über eine beliebige gegnerische Einheit.";
                break;
            case "Verstärkung":
                nameTK = name;
                wirkungTK = "Platziere einen Schläger an einer beliebigen Stelle auf deiner Spielfeldseite.";
                break;
        }
    }

    public void showTKSlots(int i)
    {
        switch (i)
        {
            case 1:
                showSlot1();
                break;
            case 2:
                showSlot1();
                showSlot2();
                break;
            case 3:
                showSlot1();
                showSlot2();
                showSlot3();
                break;
            case 4:
                showSlot1();
                showSlot2();
                showSlot3();
                showSlot4();
                break;
            case 5:
                showSlot1();
                showSlot2();
                showSlot3();
                showSlot4();
                showSlot5();
                break;
            case 6:
                showSlot1();
                showSlot2();
                showSlot3();
                showSlot4();
                showSlot5();
                showSlot6();
                break;
            case 7:
                showSlot1();
                showSlot2();
                showSlot3();
                showSlot4();
                showSlot5();
                showSlot6();
                showSlot7();
                break;
            case 8:
                showSlot1();
                showSlot2();
                showSlot3();
                showSlot4();
                showSlot5();
                showSlot6();
                showSlot7();
                showSlot8();
                break;
        }
    }

    public string getEffect(string name)
    {
        switch (name)
        {
            case "Fusel":
                return "Erhöhe den AW einer Einheit um 1 für 2 Runden.";
            case "Infektion":
                return "Alle gegnerischen Einheiten im Wirkungsbereich verlieren 2 LP.";
            case "Verstärkter Mantel":
                return "Erhöhe den VW einer Einheit um 1 für 2 Runden";
            case "Ration":
                return "Heile alle Verbündeten im Wirkungsbereich um 2 LP.";
        }
        return null;
    }

    public void showSlot1()
    {
        slot1.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name1.GetComponent<Text>().text = TKPicker.player1TK[0];
            desc1.GetComponent<Text>().text = getEffect(TKPicker.player1TK[0]);
        } else
        {
            name1.GetComponent<Text>().text = TKPicker.player2TK[0];
            desc1.GetComponent<Text>().text = getEffect(TKPicker.player2TK[0]);
        }
    }

    public void showSlot2()
    {
        slot2.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name2.GetComponent<Text>().text = TKPicker.player1TK[1];
            desc2.GetComponent<Text>().text = getEffect(TKPicker.player1TK[1]);
        }
        else
        {
            name2.GetComponent<Text>().text = TKPicker.player2TK[1];
            desc2.GetComponent<Text>().text = getEffect(TKPicker.player2TK[1]);
        }
    }

    public void showSlot3()
    {
        slot3.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name3.GetComponent<Text>().text = TKPicker.player1TK[2];
            desc3.GetComponent<Text>().text = getEffect(TKPicker.player1TK[2]);
        }
        else
        {
            name3.GetComponent<Text>().text = TKPicker.player2TK[2];
            desc3.GetComponent<Text>().text = getEffect(TKPicker.player2TK[2]);
        }
    }

    public void showSlot4()
    {
        slot4.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name4.GetComponent<Text>().text = TKPicker.player1TK[3];
            desc4.GetComponent<Text>().text = getEffect(TKPicker.player1TK[3]);
        }
        else
        {
            name4.GetComponent<Text>().text = TKPicker.player2TK[3];
            desc4.GetComponent<Text>().text = getEffect(TKPicker.player2TK[3]);
        }
    }

    public void showSlot5()
    {
        slot5.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name5.GetComponent<Text>().text = TKPicker.player1TK[4];
            desc5.GetComponent<Text>().text = getEffect(TKPicker.player1TK[5]);
        }
        else
        {
            name5.GetComponent<Text>().text = TKPicker.player2TK[4];
            desc5.GetComponent<Text>().text = getEffect(TKPicker.player2TK[5]);
        }
    }

    public void showSlot6()
    {
        slot6.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name6.GetComponent<Text>().text = TKPicker.player1TK[5];
            desc6.GetComponent<Text>().text = getEffect(TKPicker.player1TK[5]);
        }
        else
        {
            name6.GetComponent<Text>().text = TKPicker.player2TK[5];
            desc6.GetComponent<Text>().text = getEffect(TKPicker.player2TK[5]);
        }
    }

    public void showSlot7()
    {
        slot7.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name7.GetComponent<Text>().text = TKPicker.player1TK[6];
            desc7.GetComponent<Text>().text = getEffect(TKPicker.player1TK[6]);
        }
        else
        {
            name7.GetComponent<Text>().text = TKPicker.player2TK[6];
            desc7.GetComponent<Text>().text = getEffect(TKPicker.player2TK[6]);
        }
    }

    public void showSlot8()
    {
        slot8.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name8.GetComponent<Text>().text = TKPicker.player1TK[7];
            desc8.GetComponent<Text>().text = getEffect(TKPicker.player1TK[7]);
        }
        else
        {
            name8.GetComponent<Text>().text = TKPicker.player2TK[7];
            desc8.GetComponent<Text>().text = getEffect(TKPicker.player2TK[7]);
        }
    }

    public void useSlot1()
    {
        string effekt;
        if (PassthroughData.currentPlayer == 1)
        {
            Debug.Log(TKPicker.player1TK[0]);
            effekt = TKPicker.player1TK[0];
            TKPicker.player1TK[0] = TKPicker.player1TK[1];
            TKPicker.player1TK[1] = TKPicker.player1TK[2];
            TKPicker.player1TK[2] = TKPicker.player1TK[3];
            TKPicker.player1TK[3] = TKPicker.player1TK[4];
            TKPicker.player1TK[4] = TKPicker.player1TK[5];
            TKPicker.player1TK[5] = TKPicker.player1TK[6];
            TKPicker.player1TK[6] = TKPicker.player1TK[7];
            TKPicker.player1TK[7] = null;
        }
        else
        {
            Debug.Log(TKPicker.player2TK[0]);
            effekt = TKPicker.player2TK[0];
            TKPicker.player2TK[0] = TKPicker.player2TK[1];
            TKPicker.player2TK[1] = TKPicker.player2TK[2];
            TKPicker.player2TK[2] = TKPicker.player2TK[3];
            TKPicker.player2TK[3] = TKPicker.player2TK[4];
            TKPicker.player2TK[4] = TKPicker.player2TK[5];
            TKPicker.player2TK[5] = TKPicker.player2TK[6];
            TKPicker.player2TK[6] = TKPicker.player2TK[7];
            TKPicker.player2TK[7] = null;
        }
        slot1.SetActive(false);
        GetEffect(effekt);
        SchliesseTrickkartenMenu();
    }

    public void useSlot2()
    {
        string effekt;
        if (PassthroughData.currentPlayer == 1)
        {
            effekt = TKPicker.player1TK[1];
            TKPicker.player1TK[1] = TKPicker.player1TK[2];
            TKPicker.player1TK[2] = TKPicker.player1TK[3];
            TKPicker.player1TK[3] = TKPicker.player1TK[4];
            TKPicker.player1TK[4] = TKPicker.player1TK[5];
            TKPicker.player1TK[5] = TKPicker.player1TK[6];
            TKPicker.player1TK[6] = TKPicker.player1TK[7];
            TKPicker.player1TK[7] = null;
        }
        else
        {
            effekt = TKPicker.player2TK[1];
            TKPicker.player2TK[1] = TKPicker.player2TK[2];
            TKPicker.player2TK[2] = TKPicker.player2TK[3];
            TKPicker.player2TK[3] = TKPicker.player2TK[4];
            TKPicker.player2TK[4] = TKPicker.player2TK[5];
            TKPicker.player2TK[5] = TKPicker.player2TK[6];
            TKPicker.player2TK[6] = TKPicker.player2TK[7];
            TKPicker.player2TK[7] = null;
        }
        slot2.SetActive(false);
        GetEffect(effekt);
        SchliesseTrickkartenMenu();
    }

    public void useSlot3()
    {
        string effekt;
        if (PassthroughData.currentPlayer == 1)
        {
            effekt = TKPicker.player1TK[2];
            TKPicker.player1TK[2] = TKPicker.player1TK[3];
            TKPicker.player1TK[3] = TKPicker.player1TK[4];
            TKPicker.player1TK[4] = TKPicker.player1TK[5];
            TKPicker.player1TK[5] = TKPicker.player1TK[6];
            TKPicker.player1TK[6] = TKPicker.player1TK[7];
            TKPicker.player1TK[7] = null;
        }
        else
        {
            effekt = TKPicker.player2TK[2];
            TKPicker.player2TK[2] = TKPicker.player2TK[3];
            TKPicker.player2TK[3] = TKPicker.player2TK[4];
            TKPicker.player2TK[4] = TKPicker.player2TK[5];
            TKPicker.player2TK[5] = TKPicker.player2TK[6];
            TKPicker.player2TK[6] = TKPicker.player2TK[7];
            TKPicker.player2TK[7] = null;
        }
        slot3.SetActive(false);
        GetEffect(effekt);
        SchliesseTrickkartenMenu();
    }

    public void useSlot4()
    {
        string effekt;
        if (PassthroughData.currentPlayer == 1)
        {
            effekt = TKPicker.player1TK[3];
            TKPicker.player1TK[3] = TKPicker.player1TK[4];
            TKPicker.player1TK[4] = TKPicker.player1TK[5];
            TKPicker.player1TK[5] = TKPicker.player1TK[6];
            TKPicker.player1TK[6] = TKPicker.player1TK[7];
            TKPicker.player1TK[7] = null;
        }
        else
        {
            effekt = TKPicker.player2TK[3];
            TKPicker.player2TK[3] = TKPicker.player2TK[4];
            TKPicker.player2TK[4] = TKPicker.player2TK[5];
            TKPicker.player2TK[5] = TKPicker.player2TK[6];
            TKPicker.player2TK[6] = TKPicker.player2TK[7];
            TKPicker.player2TK[7] = null;
        }
        slot4.SetActive(false);
        GetEffect(effekt);
        SchliesseTrickkartenMenu();
    }

    public void useSlot5()
    {
        string effekt;
        if (PassthroughData.currentPlayer == 1)
        {
            effekt = TKPicker.player1TK[4];
            TKPicker.player1TK[4] = TKPicker.player1TK[5];
            TKPicker.player1TK[5] = TKPicker.player1TK[6];
            TKPicker.player1TK[6] = TKPicker.player1TK[7];
            TKPicker.player1TK[7] = null;
        }
        else
        {
            effekt = TKPicker.player2TK[4];
            TKPicker.player2TK[4] = TKPicker.player2TK[5];
            TKPicker.player2TK[5] = TKPicker.player2TK[6];
            TKPicker.player2TK[6] = TKPicker.player2TK[7];
            TKPicker.player2TK[7] = null;
        }
        slot5.SetActive(false);
        GetEffect(effekt);
        SchliesseTrickkartenMenu();
    }

    public void useSlot6()
    {
        string effekt;
        if (PassthroughData.currentPlayer == 1)
        {
            effekt = TKPicker.player1TK[5];
            TKPicker.player1TK[5] = TKPicker.player1TK[6];
            TKPicker.player1TK[6] = TKPicker.player1TK[7];
            TKPicker.player1TK[7] = null;
        }
        else
        {
            effekt = TKPicker.player2TK[5];
            TKPicker.player2TK[5] = TKPicker.player2TK[6];
            TKPicker.player2TK[6] = TKPicker.player2TK[7];
            TKPicker.player2TK[7] = null;
        }
        slot6.SetActive(false);
        GetEffect(effekt);
        SchliesseTrickkartenMenu();
    }

    public void useSlot7()
    {
        string effekt;
        if (PassthroughData.currentPlayer == 1)
        {
            effekt = TKPicker.player1TK[6];
            TKPicker.player1TK[6] = TKPicker.player1TK[7];
            TKPicker.player1TK[7] = null;
        }
        else
        {
            effekt = TKPicker.player2TK[6];
            TKPicker.player2TK[6] = TKPicker.player2TK[7];
            TKPicker.player2TK[7] = null;
        }
        slot7.SetActive(false);
        GetEffect(effekt);
        SchliesseTrickkartenMenu();
    }

    public void useSlot8()
    {
        string effekt;
        if (PassthroughData.currentPlayer == 1)
        {
            effekt = TKPicker.player1TK[7];
            TKPicker.player1TK[7] = null;
        }
        else
        {
            effekt = TKPicker.player2TK[7];
            TKPicker.player2TK[7] = null;
        }
        slot8.SetActive(false);
        GetEffect(effekt);
        SchliesseTrickkartenMenu();
    }

    public void GetEffect(string effect)
    {
        Debug.Log(effect);
        switch (effect) {
            case "Investition":
                InvestitionEffect();
                break;

        }
    }

    public void InvestitionEffect()
    {
        SaveEffect("Investition", 2);
        rsc.IncreaseActiveInvestitionen();
        rsc.RefreshDisplay(PassthroughData.currentPlayer);
        if (PassthroughData.currentPlayer == 1)
        {
            
        }
        else
        {
            
        }

    }

    public void EndInvestitionEffect()
    {
        rsc.DecreaseActiveInvestitionen();
        rsc.RefreshDisplay(PassthroughData.currentPlayer);
    }

    public void SaveEffect(string effectname, int effectduration)
    {
        if (PassthroughData.currentPlayer == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                Debug.Log(activeTKSpieler1[i] + " " + i);
                if (activeTKSpieler1[i] == "")
                {
                    
                    activeTKSpieler1[i] = effectname;
                    activeTKDauer1[i] = effectduration;
                    Debug.Log(activeTKSpieler1[i] + " gespeichert bei " + i);
                    break;
                }
                else
                {


                    Debug.Log("Bei " + i + " ist alles belegt.");
                }

            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                Debug.Log(activeTKSpieler2[i] + " " + i);
                if (activeTKSpieler2[i] == "")
                {
                    activeTKSpieler2[i] = effectname;
                    activeTKDauer2[i] = effectduration;
                    Debug.Log(activeTKSpieler2[i] + " gespeichert bei " + i);
                    break;
                }
                else
                {


                    Debug.Log("Bei " + i + " ist alles belegt.");
                }
            }
        }
    }

    public void EndActiveEffect(string effectname)
    {
        switch(effectname)
        {
            case "Investition":
                EndInvestitionEffect();
                break;
        }
    }

    public void CheckActiveEffects()
    {
        if (PassthroughData.currentPlayer == 1)
        {
            for ( int i = 0; i < 8; i++ )
            {
                if (activeTKSpieler1[i] == null)
                {
                    Debug.Log(activeTKSpieler1[i]);
                    break;
                }
                else
                {
                    if (activeTKDauer1[i] != 0)
                    {
                        activeTKDauer1[i]--;
                        CallActiveEffect(activeTKSpieler1[i], i);
                    }
                    else
                    {
                        EndActiveEffect(activeTKSpieler1[i]);
                        activeTKSpieler1[i] = null;
                        for (int j = i; j < 7; j++)
                        {
                            activeTKSpieler1[i] = activeTKSpieler1[i + 1];
                            activeTKDauer1[i] = activeTKDauer1[i + 1];
                        }
                        activeTKSpieler1[7] = null;
                        activeTKDauer1[7] = 0;
                    }
                        
                }
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                if (activeTKSpieler2[i] == null)
                {
                    Debug.Log(activeTKSpieler2[i]);
                    break;
                }
                else
                {
                    if (activeTKDauer2[i] != 0)
                    {
                        activeTKDauer2[i]--;
                        CallActiveEffect(activeTKSpieler2[i], i);
                    }
                    else
                    {
                        EndActiveEffect(activeTKSpieler2[i]);
                        activeTKSpieler2[i] = null;
                        for (int j = i; j < 7; j++)
                        {
                            activeTKSpieler2[i] = activeTKSpieler2[i + 1];
                            activeTKDauer2[i] = activeTKDauer2[i + 1];
                        }
                        activeTKSpieler2[7] = null;
                        activeTKDauer2[7] = 0;
                    }

                }
            }
        }
    }

    public void CallActiveEffect(string effectname, int position)
    {
        switch (effectname)
        {
            case "Investition":
                Debug.Log(effectname + " für Spieler " + PassthroughData.currentPlayer);

                break;
        }
    }
}

