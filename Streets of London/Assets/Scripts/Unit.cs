using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    private int id;
    private string name;
    private int actionValue;
    private int attValue;
    private int lifeValue;
    private int defValue;
    private int reach;
    private int cost;

    public Unit(int id, string name, int actionValue, int attValue, int lifeValue, int defValue, int reach, int cost)
    {
        this.id = id;
        this.name = name;
        this.actionValue = actionValue;
        this.attValue = attValue;
        this.lifeValue = lifeValue;
        this.defValue = defValue;
        this.reach = reach;
        this.cost = cost;
    }

    


}
