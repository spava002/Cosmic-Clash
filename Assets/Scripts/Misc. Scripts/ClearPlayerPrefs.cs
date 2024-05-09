using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour {
    void Start() {
        Invoke("ResetPlayerData", 5f);
    }

    void ResetPlayerData() {
        PlayerPrefs.DeleteAll();
    }
}
