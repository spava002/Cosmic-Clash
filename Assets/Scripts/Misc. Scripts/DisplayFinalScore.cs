using UnityEngine;
using TMPro;
public class DisplayFinalScore : MonoBehaviour {
    int finalScore;
    TMP_Text finalScoreText;

    void Start() {
        finalScore = PlayerPrefs.GetInt("SavedScore");
        finalScoreText = GetComponent<TMP_Text>();
        DisplayScore();
    }

    void DisplayScore() {
        finalScoreText.text = "Final Score: " + finalScore;
    }
}
