using UnityEngine;

/*
 * Das FieldHelper Skript stellt den einzelnen feldern die informationen über die ID und die Koordinaten zur verfügung.
 * 
 * Des weiteren ob sich eine Einheit auf dem Feld befindet und wenn ja welche ID die hat.
 * 
 * Ist das feld eine Fabrik hat es zudem ein zweites koordinaten Set. 
 */ 

public class FieldHelper : MonoBehaviour {

    public bool isSet;
    public int unitID;
    public bool hasUnit;
    public int id;
    public int x;
    public int y;
    public int z;

    public bool isfabrik;
    public int x2;
    public int y2;
    public int z2;
}
