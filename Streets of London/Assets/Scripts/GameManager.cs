using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {
    public PlayerMenu pm;
    private void Start()
    {
        pm.PanelState(true);
    } 
}
