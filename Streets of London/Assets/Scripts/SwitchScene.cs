using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {
    // Wechselt zur übergebenen Szene
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);       
    }
}
