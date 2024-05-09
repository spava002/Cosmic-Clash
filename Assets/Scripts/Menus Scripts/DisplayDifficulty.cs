using UnityEngine;
using TMPro;
using System;

public class DisplayDifficulty : MonoBehaviour {
    TMP_Text difficultyConfirmation;
    string chosenDifficulty = "Normal";

    void Start() {
        difficultyConfirmation = GetComponent<TMP_Text>();
    }

    void Update() {
        if (PlayerPrefs.GetString("ChosenDifficulty") != "") {
            chosenDifficulty = PlayerPrefs.GetString("ChosenDifficulty");
        } 
        DisplayConfirmation();
    }

    void DisplayConfirmation() {
        difficultyConfirmation.text = "Difficulty Set to: " + chosenDifficulty;
    }
}
