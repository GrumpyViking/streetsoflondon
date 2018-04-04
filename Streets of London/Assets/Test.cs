using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    float count = 0;
    int enat = 0;
    GameObject text;
    TextMesh t;

    // Use this for initialization
    void Start () {
        text = new GameObject();
        t = text.AddComponent<TextMesh>();
        t.transform.localEulerAngles += new Vector3(90, 0, 0);
        t.transform.localPosition += new Vector3(0, 0, 0);

    }
	
	// Update is called once per frame
	void Update () {
        if (enat == 60)
        {
            ChangeText();
            enat = 0;
        }
        else { enat++; }

    }

    void ChangeText() { 
  
        t.text = count.ToString();
        t.fontSize = 30;
        
        count++;

    }
}
