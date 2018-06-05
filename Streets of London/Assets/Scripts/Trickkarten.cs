using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="neue Trickkarte", menuName ="Trickkarte" )]
public class Trickkarten : MonoBehaviour {

    public string nameTK;
    public string wirkungTK;
 
    public Sprite wirkungsbereich;
    public Sprite trickkartenbild;

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
        if (PassthrougData.currentPlayer == 1)
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
        if (PassthrougData.currentPlayer == 1)
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
        if (PassthrougData.currentPlayer == 1)
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
        if (PassthrougData.currentPlayer == 1)
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
        if (PassthrougData.currentPlayer == 1)
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
        if (PassthrougData.currentPlayer == 1)
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
        if (PassthrougData.currentPlayer == 1)
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
        if (PassthrougData.currentPlayer == 1)
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
        if (PassthrougData.currentPlayer == 1)
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
        slot1.SetActive(false);
    }
    public void useSlot2()
    {
        slot2.SetActive(false);
    }
    public void useSlot3()
    {
        slot3.SetActive(false);
    }
    public void useSlot4()
    {
        slot4.SetActive(false);
    }
    public void useSlot5()
    {
        slot5.SetActive(false);
    }
    public void useSlot6()
    {
        slot6.SetActive(false);
    }
    public void useSlot7()
    {
        slot7.SetActive(false);
    }
    public void useSlot8()
    {
        slot8.SetActive(false);
    }
}

