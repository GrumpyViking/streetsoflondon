using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KaufMenuScript : MonoBehaviour
{
    public GameObject kaufMenuScriptObject;
    public GameObject playerTextTop;
    public GameObject playerTextKaufMenu;
    public Sprite[] unitpictures;
    public GameObject[] units;

    public GameObject goldvor;
    public GameObject goldnach;

    public GameObject anzeige1;
    public GameObject anzeige2;
    public GameObject anzeige3;
    public GameObject anzeige4;
    public GameObject anzeige5;
    public GameObject anzeigeTK;

    public GameObject preis1;
    public GameObject preis2;
    public GameObject preis3;
    public GameObject preis4;
    public GameObject preis5;
    public GameObject preisTK;

    public DataBaseController dbc;

    public void OeffneKaufmenue()
    {
        kaufMenuScriptObject.SetActive(true);

        playerTextKaufMenu.GetComponent<Text>().text = playerTextTop.GetComponent<Text>().text;
        goldvor.GetComponent<Text>().text = dbc.RequestFromDB("Select Gold from Spieler where ID = " + PassthrougData.currentPlayer);
        EinheitentypAktualisierung(PassthrougData.currentPlayer);


    }

    public void SchliesseKaufmenu()
    {
        kaufMenuScriptObject.SetActive(false);
        anzeige1.GetComponent<Text>().text = "0";
        Preisaktualisierung1();
        anzeige2.GetComponent<Text>().text = "0";
        Preisaktualisierung2();
        anzeige3.GetComponent<Text>().text = "0";
        Preisaktualisierung3();
        anzeige4.GetComponent<Text>().text = "0";
        Preisaktualisierung4();
        anzeige5.GetComponent<Text>().text = "0";
        Preisaktualisierung5();
        anzeigeTK.GetComponent<Text>().text = "0";
        PreisaktualisierungTK();
    }
    //---------------------------------------------------------------------------------------------------------------
    //OnClick-Methoden der Buttons zum Erhöhen und Verringern der Kaufmenge der Einheitentypen 1-5 und Trickkarten
    public void ErhoeheAnzeige1()
    {
        int x1 = Convert.ToInt32(anzeige1.GetComponent<Text>().text);
        String text1 = Convert.ToString(x1 + 1);
        anzeige1.GetComponent<Text>().text = text1;
        Preisaktualisierung1();
    }

    public void VerringereAnzeige1()
    {
        if (Convert.ToInt32(anzeige1.GetComponent<Text>().text) > 0)
        {
            int x1 = Convert.ToInt32(anzeige1.GetComponent<Text>().text);
            String text1 = Convert.ToString(x1 - 1);
            anzeige1.GetComponent<Text>().text = text1;
            Preisaktualisierung1();
        }
    }

    public void ErhoeheAnzeige2()
    {
        int x2 = Convert.ToInt32(anzeige2.GetComponent<Text>().text);
        String text2 = Convert.ToString(x2 + 1);
        anzeige2.GetComponent<Text>().text = text2;
        Preisaktualisierung2();
    }

    public void VerringereAnzeige2()
    {
        if (Convert.ToInt32(anzeige2.GetComponent<Text>().text) > 0)
        {
            int x2 = Convert.ToInt32(anzeige2.GetComponent<Text>().text);
            String text2 = Convert.ToString(x2 - 1);
            anzeige2.GetComponent<Text>().text = text2;
            Preisaktualisierung2();
        }
    }

    public void ErhoeheAnzeige3()
    {
        int x3 = Convert.ToInt32(anzeige3.GetComponent<Text>().text);
        String text3 = Convert.ToString(x3 + 1);
        anzeige3.GetComponent<Text>().text = text3;
        Preisaktualisierung3();
    }

    public void VerringereAnzeige3()
    {
        if (Convert.ToInt32(anzeige3.GetComponent<Text>().text) > 0)
        {
            int x3 = Convert.ToInt32(anzeige3.GetComponent<Text>().text);
            String text3 = Convert.ToString(x3 - 1);
            anzeige3.GetComponent<Text>().text = text3;
            Preisaktualisierung3();
        }
    }

    public void ErhoeheAnzeige4()
    {
        int x4 = Convert.ToInt32(anzeige4.GetComponent<Text>().text);
        String text4 = Convert.ToString(x4 + 1);
        anzeige4.GetComponent<Text>().text = text4;
        Preisaktualisierung4();
    }

    public void VerringereAnzeige4()
    {
        if (Convert.ToInt32(anzeige4.GetComponent<Text>().text) > 0)
        {
            int x4 = Convert.ToInt32(anzeige4.GetComponent<Text>().text);
            String text4 = Convert.ToString(x4 - 1);
            anzeige4.GetComponent<Text>().text = text4;
            Preisaktualisierung4();
        }
    }

    public void ErhoeheAnzeige5()
    {
        int x5 = Convert.ToInt32(anzeige5.GetComponent<Text>().text);
        String text5 = Convert.ToString(x5 + 1);
        anzeige5.GetComponent<Text>().text = text5;
        Preisaktualisierung5();
    }

    public void VerringereAnzeige5()
    {
        if (Convert.ToInt32(anzeige5.GetComponent<Text>().text) > 0)
        {
            int x5 = Convert.ToInt32(anzeige5.GetComponent<Text>().text);
            String text5 = Convert.ToString(x5 - 1);
            anzeige5.GetComponent<Text>().text = text5;
            Preisaktualisierung5();
        }
    }

    public void ErhoeheAnzeigeTK()
    {
        int xTK = Convert.ToInt32(anzeigeTK.GetComponent<Text>().text);
        String textTK = Convert.ToString(xTK + 1);
        anzeigeTK.GetComponent<Text>().text = textTK;
        PreisaktualisierungTK();
    }

    public void VerringereAnzeigeTK()
    {
        if (Convert.ToInt32(anzeigeTK.GetComponent<Text>().text) > 0)
        {
            int xTK = Convert.ToInt32(anzeigeTK.GetComponent<Text>().text);
            String textTK = Convert.ToString(xTK - 1);
            anzeigeTK.GetComponent<Text>().text = textTK;
            PreisaktualisierungTK();
        }
    }
    //---------------------------------------------------------------------------------------------------------------
    //Aktualisierung der Gesamtpreisanzeigen für die Einheitentypen 1-5 und die Trickkarten
    public void Preisaktualisierung1()
    {
        preis1.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige1.GetComponent<Text>().text) * 2);
    }

    public void Preisaktualisierung2()
    {
        preis2.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige2.GetComponent<Text>().text) * 2);
    }

    public void Preisaktualisierung3()
    {
        preis3.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige3.GetComponent<Text>().text) * 2);
    }

    public void Preisaktualisierung4()
    {
        preis4.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige4.GetComponent<Text>().text) * 2);
    }

    public void Preisaktualisierung5()
    {
        preis5.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeige5.GetComponent<Text>().text) * 2);
    }

    public void PreisaktualisierungTK()
    {
        preisTK.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(anzeigeTK.GetComponent<Text>().text) * 4);
    }
    //---------------------------------------------------------------------------------------------------------------

    public void KaufBestaetigen()
    {
        //Schreibe Einheiten in DB
        //dbc.WriteToDB("Insert into Einheit(ID,Name,Aktionspunkte,Angriffspunkte,Lebenspunkte, Verteidigungspunkte,Reichweite,Kosten, SpielerID) VALues()");
       

        //Spawne einheiten auf Spielfeld



        SchliesseKaufmenu();
    }

    public void EinheitentypAktualisierung(int playerID)
    {
        int[] ids = new int[5];
        ids = dbc.GetUnitIds(playerID);
        for(int i = 0; i < units.Length; i++)
        {
            if (ids[i] == 1 || ids[i] == 2)
            {
                //material vom Boss zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[0];
            }
            if (ids[i] == 3 || ids[i] == 4)
            {
                //material vom Diebin zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[1];
            }
            if (ids[i] == 5 || ids[i] == 6)
            {
                //material vom Meuchelmörder zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[2];
            }
            if (ids[i] == 7 || ids[i] == 8)
            {
                //material vom pestarzt zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[3];
            }
            if (ids[i] == 9 || ids[i] == 10)
            {
                //material vom polizist zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[4];
            }
            if (ids[i] == 11 || ids[i] == 12)
            {
                //material vom raufbold zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[5];
            }
            if (ids[i] == 13 || ids[i] == 14)
            {
                //material vom scharfschuetze zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[6];
            }
            if (ids[i] == 15 || ids[i] == 16)
            {
                //material vom schlaeger zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[7];
            }
            if (ids[i] == 17 || ids[i] == 18)
            {
                //material vom taschendieb zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[8];
            }
            if (ids[i] == 19 || ids[i] == 20)
            {
                //material vom tueftler zuweisen
                units[i].GetComponent<Image>().sprite = unitpictures[9];
            }
        }
    }

}

