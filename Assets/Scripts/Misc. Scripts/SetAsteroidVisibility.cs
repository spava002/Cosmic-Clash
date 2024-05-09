using UnityEngine;

public class SetAsteroidVisibility : MonoBehaviour {
    void Start() {
        if (PlayerPrefs.GetInt("DisableAsteroids") == 1) {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
        }
    }
}
