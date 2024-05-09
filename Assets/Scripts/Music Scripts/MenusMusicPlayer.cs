using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusMusicPlayer : MonoBehaviour {
    void Awake() {
        int numOfMusicPlayers = FindObjectsByType<MenusMusicPlayer>(FindObjectsSortMode.None).Length;
        if (numOfMusicPlayers > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }    
    }

    void Update() {
        string currentLevelName = SceneManager.GetActiveScene().name;
        if (currentLevelName == "Celestria") {
            Destroy(gameObject);
        } 
    }
}
