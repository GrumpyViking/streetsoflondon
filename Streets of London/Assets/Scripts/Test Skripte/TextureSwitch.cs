using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Altes Testskript um funktion von Texturen wechsel zu überprüfen
 * 
 */ 

public class TextureSwitch : MonoBehaviour {
    //Set these Textures in the Inspector
    public Texture m_MainTexture, m_AltTexTure;
    Renderer m_Renderer;

    void Start ()
     {
        //Fetch the Renderer from the GameObject
        m_Renderer = GetComponent<Renderer>();
        
        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        m_Renderer.material.SetTexture("_MainTex", m_MainTexture);
        

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            m_Renderer.material.SetTexture("_MainTex", m_MainTexture);


        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            m_Renderer.material.SetTexture("_MainTex", m_AltTexTure);
        }
    }

}
