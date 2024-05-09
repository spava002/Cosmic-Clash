using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAsteroids : MonoBehaviour {
    Toggle toggle;

    void Start() {
        toggle = GetComponent<Toggle>();
        PlayerPrefs.SetInt("DisableAsteroids", 0);

        toggle.onValueChanged.AddListener(delegate {
            ChangeValue(toggle);
        });
    }

    void ChangeValue(Toggle toggle) {
        if (toggle.isOn) {
            PlayerPrefs.SetInt("DisableAsteroids", 1);
        }
        else {
            PlayerPrefs.SetInt("DisableAsteroids", 0);
        }
    }
}
