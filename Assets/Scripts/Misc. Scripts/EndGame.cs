using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {
    void Start() {
        Invoke("ExitToMainMenu", 7f);    
    }

    void ExitToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
