using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour {
    int score;
    TMP_Text scoreText;

    void Start() {
        scoreText = GetComponent<TMP_Text>();
        if (PlayerPrefs.GetInt("CheckpointReached") == 1) {
            score = PlayerPrefs.GetInt("SavedScore");
        }
        DisplayScore();
    }

    public void IncreaseScore(int increaseAmount) {
        score += increaseAmount;
        UpdateScore();
        DisplayScore();
    }

    void UpdateScore() {
        PlayerPrefs.SetInt("SavedScore", score);
    }

    void DisplayScore() {
        scoreText.text = "Score: " + score;
    }
}
