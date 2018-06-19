using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="neue Trickkarte", menuName ="Trickkarte" )]
public class Trickkarten : MonoBehaviour {

    public string nameTK;
    public string wirkungTK;
    public string effekt;

    public string[] activeTKSpieler1 = { null, null, null, null, null, null, null, null };
    public string[] activeTKSpieler2 = { null, null, null, null, null, null, null, null };
    public int[] activeTKDauer1 = { 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] activeTKDauer2 = { 0, 0, 0, 0, 0, 0, 0, 0 };

    public Ressources rsc;
    public DataBaseController dbc;

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

    public Image bild1;
    public Image bild2;
    public Image bild3;
    public Image bild4;
    public Image bild5;
    public Image bild6;
    public Image bild7;
    public Image bild8;

    public Sprite bildTemp;
    public Sprite bildInvestition;
    public Sprite bildDoppelbock;
    public Sprite bildMantel;
    public Sprite bildInfektion;
    public Sprite bildRation;

    public int[] anzahlEinheitenFusel;
    public int[] anzahlEinheitenMantel;
    public int[] anzahlEinheitenRation;
    public int[] anzahlEinheitenInfektion;
    public int[][] einheitenIdFusel;
    public int[][] einheitenIdMantel;
    public int[][] einheitenIdRation;
    public int[][] einheitenIdInfektion;

    public void Start()
    {
        anzahlEinheitenFusel = new int[2];
        anzahlEinheitenMantel = new int[2];
        anzahlEinheitenRation = new int[2];
        anzahlEinheitenInfektion = new int[2];
        einheitenIdFusel = new int[2][];
        einheitenIdFusel[0] = new int[30];
        einheitenIdFusel[1] = new int[30];
        einheitenIdMantel = new int[2][];
        einheitenIdMantel[0] = new int[30];
        einheitenIdMantel[1] = new int[30];
        einheitenIdRation = new int[2][];
        einheitenIdRation[0] = new int[30];
        einheitenIdRation[1] = new int[30];
        einheitenIdInfektion = new int[2][];
        einheitenIdInfektion[0] = new int[30];
        einheitenIdInfektion[1] = new int[30];
    }

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
                wirkungTK = "Erhöhe den AW deiner Einheiten um 1 für die nächste Runde.";
                bildTemp = bildDoppelbock;
                break;
            case "Infektion":
                nameTK = name;
                wirkungTK = "Alle gegnerischen Einheiten verlieren 1 LP. Einheiten können nicht durch diesen Effekt sterben.";
                bildTemp = bildInfektion;
                break;
            case "Verstärkter Mantel":
                nameTK = name;
                wirkungTK = "Erhöhe den VW deiner Einheiten um 1 für die nächste Runde.";
                bildTemp = bildMantel;
                break;
            case "Ration":
                nameTK = name;
                wirkungTK = "Heile alle deine Einheiten um 1 LP.";
                bildTemp = bildRation;
                break;
            case "Investition":
                nameTK = name;
                wirkungTK = "Erhalte 3 Gold pro Runde für 3 Runden.";
                bildTemp = bildInvestition;
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
            case "Investition":
                return "Erhalte 3 Gold pro Runde für 3 Runden.";
        }
        return null;
    }

    public Sprite getImage(string name)
    {
        switch (name)
        {
            case "Fusel":
                return bildDoppelbock;
            case "Infektion":
                return bildInfektion;
            case "Verstärkter Mantel":
                return bildMantel;
            case "Ration":
                return bildRation;
            case "Investition":
                return bildInvestition;
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
            bild1.sprite = getImage(TKPicker.player1TK[0]);
        } else
        {
            name1.GetComponent<Text>().text = TKPicker.player2TK[0];
            desc1.GetComponent<Text>().text = getEffect(TKPicker.player2TK[0]);
            bild1.sprite = getImage(TKPicker.player2TK[0]);
        }
    }

    public void showSlot2()
    {
        slot2.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name2.GetComponent<Text>().text = TKPicker.player1TK[1];
            desc2.GetComponent<Text>().text = getEffect(TKPicker.player1TK[1]);
            bild2.sprite = getImage(TKPicker.player1TK[1]);
        }
        else
        {
            name2.GetComponent<Text>().text = TKPicker.player2TK[1];
            desc2.GetComponent<Text>().text = getEffect(TKPicker.player2TK[1]);
            bild2.sprite = getImage(TKPicker.player2TK[1]);
        }
    }

    public void showSlot3()
    {
        slot3.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name3.GetComponent<Text>().text = TKPicker.player1TK[2];
            desc3.GetComponent<Text>().text = getEffect(TKPicker.player1TK[2]);
            bild3.sprite = getImage(TKPicker.player1TK[2]);
        }
        else
        {
            name3.GetComponent<Text>().text = TKPicker.player2TK[2];
            desc3.GetComponent<Text>().text = getEffect(TKPicker.player2TK[2]);
            bild3.sprite = getImage(TKPicker.player2TK[2]);
        }
    }

    public void showSlot4()
    {
        slot4.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name4.GetComponent<Text>().text = TKPicker.player1TK[3];
            desc4.GetComponent<Text>().text = getEffect(TKPicker.player1TK[3]);
            bild4.sprite = getImage(TKPicker.player1TK[3]);
        }
        else
        {
            name4.GetComponent<Text>().text = TKPicker.player2TK[3];
            desc4.GetComponent<Text>().text = getEffect(TKPicker.player2TK[3]);
            bild4.sprite = getImage(TKPicker.player2TK[3]);
        }
    }

    public void showSlot5()
    {
        slot5.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name5.GetComponent<Text>().text = TKPicker.player1TK[4];
            desc5.GetComponent<Text>().text = getEffect(TKPicker.player1TK[4]);
            bild5.sprite = getImage(TKPicker.player1TK[4]);
        }
        else
        {
            name5.GetComponent<Text>().text = TKPicker.player2TK[4];
            desc5.GetComponent<Text>().text = getEffect(TKPicker.player2TK[4]);
            bild5.sprite = getImage(TKPicker.player2TK[4]);
        }
    }

    public void showSlot6()
    {
        slot6.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name6.GetComponent<Text>().text = TKPicker.player1TK[5];
            desc6.GetComponent<Text>().text = getEffect(TKPicker.player1TK[5]);
            bild6.sprite = getImage(TKPicker.player1TK[5]);
        }
        else
        {
            name6.GetComponent<Text>().text = TKPicker.player2TK[5];
            desc6.GetComponent<Text>().text = getEffect(TKPicker.player2TK[5]);
            bild6.sprite = getImage(TKPicker.player2TK[5]);
        }
    }

    public void showSlot7()
    {
        slot7.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name7.GetComponent<Text>().text = TKPicker.player1TK[6];
            desc7.GetComponent<Text>().text = getEffect(TKPicker.player1TK[6]);
            bild7.sprite = getImage(TKPicker.player1TK[6]);
        }
        else
        {
            name7.GetComponent<Text>().text = TKPicker.player2TK[6];
            desc7.GetComponent<Text>().text = getEffect(TKPicker.player2TK[6]);
            bild7.sprite = getImage(TKPicker.player2TK[6]);
        }
    }

    public void showSlot8()
    {
        slot8.SetActive(true);
        if (PassthroughData.currentPlayer == 1)
        {
            name8.GetComponent<Text>().text = TKPicker.player1TK[7];
            desc8.GetComponent<Text>().text = getEffect(TKPicker.player1TK[7]);
            bild8.sprite = getImage(TKPicker.player1TK[7]);
        }
        else
        {
            name8.GetComponent<Text>().text = TKPicker.player2TK[7];
            desc8.GetComponent<Text>().text = getEffect(TKPicker.player2TK[7]);
            bild8.sprite = getImage(TKPicker.player2TK[7]);
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
            case "Ration":
                RationEffect();
                break;
            case "Fusel":
                FuselEffect();
                break;
            case "Verstärkter Mantel":
                MantelEffect();
                break;
            case "Infektion":
                InfektionEffect();
                break;

        }
    }

    public void InvestitionEffect()
    {
        SaveEffect("Investition", 2);
        rsc.IncreaseActiveInvestitionen();
        rsc.RefreshDisplay(PassthroughData.currentPlayer);
    }

    public void FuselEffect()
    {
        SaveEffect("Fusel", 1);
    }

    public void ActivateFusel()
    {
        anzahlEinheitenFusel[PassthroughData.currentPlayer] = dbc.GetNumUnitsofPlayer(PassthroughData.currentPlayer);
        einheitenIdFusel[PassthroughData.currentPlayer] = dbc.GetUnitIds(PassthroughData.currentPlayer);
        for (int i = 0; i < anzahlEinheitenFusel[PassthroughData.currentPlayer]; i++)
        {
            dbc.WriteToDB("Update Einheit SET Angriffspunkte = " + (dbc.GetAtt(einheitenIdFusel[PassthroughData.currentPlayer][i]) + 1) + " Where ID = " + einheitenIdFusel[PassthroughData.currentPlayer][i]);
        }
    }

    public void MantelEffect()
    {
        SaveEffect("Verstärkter Mantel", 1);
    }

    public void ActivateMantel()
    {
        anzahlEinheitenMantel[PassthroughData.currentPlayer] = dbc.GetNumUnitsofPlayer(PassthroughData.currentPlayer);
        einheitenIdMantel[PassthroughData.currentPlayer] = dbc.GetUnitIds(PassthroughData.currentPlayer);
        for (int i = 1; i <= anzahlEinheitenMantel[PassthroughData.currentPlayer]; i++)
        {
            dbc.WriteToDB("Update Einheit SET Verteidigungspunkte = " + (dbc.GetDef(einheitenIdMantel[PassthroughData.currentPlayer][i]) + 1) + " Where ID = " + einheitenIdMantel[PassthroughData.currentPlayer][i]);
        }
    }

    public void RationEffect()
    {
        Debug.Log("Ration Effekt für Spieler " + PassthroughData.currentPlayer);
        anzahlEinheitenRation[PassthroughData.currentPlayer] = dbc.GetNumUnitsofPlayer(PassthroughData.currentPlayer);
        einheitenIdRation[PassthroughData.currentPlayer] = dbc.GetUnitIds(PassthroughData.currentPlayer);
        for (int i = 1; i <= anzahlEinheitenMantel[PassthroughData.currentPlayer]; i++)
        {
            //if (dbc.GetLP(einheitenIdRation[PassthroughData.currentPlayer][i]) < dbc.GetMaxLP())
        }
    }

    public void InfektionEffect()
    {
        Debug.Log("Infektion Effekt für Spieler " + PassthroughData.currentPlayer);

        if (PassthroughData.currentPlayer == 1)
        {
            anzahlEinheitenInfektion[1] = dbc.GetNumUnitsofPlayer(2);
            einheitenIdInfektion[1] = dbc.GetUnitIds(2);
            for (int i = 1; i <= anzahlEinheitenInfektion[2]; i++)
            {
                if (dbc.GetLP(einheitenIdRation[1][i]) != 1)
                {
                    dbc.WriteToDB("Update Einheit SET Lebenspunkte = " + (dbc.GetLP(einheitenIdInfektion[1][i]) - 1) + " Where ID = " + einheitenIdInfektion[1][i]);
                }
            }
        }
        else
        {
            anzahlEinheitenInfektion[0] = dbc.GetNumUnitsofPlayer(1);
            einheitenIdInfektion[0] = dbc.GetUnitIds(1);
            for (int i = 1; i <= anzahlEinheitenInfektion[0]; i++)
            {
                if (dbc.GetLP(einheitenIdRation[1][i]) != 1)
                {
                    dbc.WriteToDB("Update Einheit SET Lebenspunkte = " + (dbc.GetLP(einheitenIdInfektion[0][i]) - 1) + " Where ID = " + einheitenIdInfektion[0][i]);
                }
            }
        }
    }

    public void EndInvestitionEffect()
    {
        rsc.DecreaseActiveInvestitionen();
        rsc.RefreshDisplay(PassthroughData.currentPlayer);
    }

    public void EndFuselEffect()
    {
        for (int i = 1; i <= anzahlEinheitenFusel[PassthroughData.currentPlayer]; i++)
        {
            dbc.WriteToDB("Update Einheit SET Angriffspunkte = " + (dbc.GetAtt(einheitenIdFusel[PassthroughData.currentPlayer][i]) + 1) + " Where ID = " + einheitenIdFusel[PassthroughData.currentPlayer][i]);
        }
    }

    public void EndMantelEffect()
    {
        for (int i = 1; i <= anzahlEinheitenMantel[PassthroughData.currentPlayer]; i++)
        {
            dbc.WriteToDB("Update Einheit SET Verteidigungspunkte = " + (dbc.GetDef(einheitenIdMantel[PassthroughData.currentPlayer][i]) + 1) + " Where ID = " + einheitenIdMantel[PassthroughData.currentPlayer][i]);
        }
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
            case "Fusel":
                EndFuselEffect();
                break;
            case "Verstärkter Mantel":
                EndMantelEffect();
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
            case "Fusel":
                Debug.Log(effectname + " für Spieler " + PassthroughData.currentPlayer);
                ActivateFusel();
                break;
            case "Verstärkter Mantel":
                Debug.Log(effectname + " für Spieler " + PassthroughData.currentPlayer);
                ActivateMantel();
                break;
        }
    }
}

