using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SetDifficulty : MonoBehaviour {
    Button difficultyButton;
    float difficultyMultipler = 1f;
    string assignedDifficulty;

    void Start() {
        difficultyButton = GetComponent<Button>();
        difficultyButton.onClick.AddListener(AssignDifficulty);
        assignedDifficulty = GetComponentInChildren<TMP_Text>().text;
    }

    void AssignDifficulty() {
        switch (assignedDifficulty) {
            case "Easy":
                difficultyMultipler = 0.5f;
                break;
            case "Normal":
                difficultyMultipler = 1f;
                break;
            case "Hard":
                difficultyMultipler = 1.5f;
                break;
            default:
                break;
        }
        PlayerPrefs.SetFloat("DifficultyMultiplier", difficultyMultipler);
        PlayerPrefs.SetString("ChosenDifficulty", assignedDifficulty);
    }
}
