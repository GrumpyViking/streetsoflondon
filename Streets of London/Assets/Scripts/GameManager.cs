using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {
    GameObject playerOneText;
    GameObject playerTwoText;

    private void Start()
    {

    }


    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);       
    }
}
