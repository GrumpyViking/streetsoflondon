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

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public int ActionValue
    {
        get
        {
            return actionValue;
        }

        set
        {
            actionValue = value;
        }
    }

    public int AttValue
    {
        get
        {
            return attValue;
        }

        set
        {
            attValue = value;
        }
    }

    public int LifeValue
    {
        get
        {
            return lifeValue;
        }

        set
        {
            lifeValue = value;
        }
    }

    public int DefValue
    {
        get
        {
            return defValue;
        }

        set
        {
            defValue = value;
        }
    }

    public int Reach
    {
        get
        {
            return reach;
        }

        set
        {
            reach = value;
        }
    }

    public int Cost
    {
        get
        {
            return cost;
        }

        set
        {
            cost = value;
        }
    }
}
