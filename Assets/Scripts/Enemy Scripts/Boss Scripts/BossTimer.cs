using TMPro;
using UnityEngine;

public class BossTimer : MonoBehaviour {
    float totalTime = 120f;
    float remainingTime;
    float startTime;
    float elapsedTime;
    bool runTimer = false;
    TMP_Text timer;
    
    void Start() {
        timer = GetComponent<TMP_Text>();
        remainingTime = totalTime;
    }

    void Update() {
        if (runTimer) {
            CalculateRemainingTime();
            DisplayTime();
        }
    }

    void CalculateRemainingTime() {
        if (runTimer) {
            elapsedTime = Time.time - startTime;
            remainingTime = totalTime - elapsedTime;
            if (elapsedTime > totalTime) {
                remainingTime = 0f;
            }
        }
    }

    void DisplayTime() {
        if (remainingTime < 30f) {
            timer.color = Color.red;
        }
        timer.text = "Time Left: " + remainingTime.ToString("F2");
    }

    public void StartTimer() {
        startTime = Time.time;
        runTimer = true;
    }

    public void StopTimer() {
        runTimer = false;
    }

    public float GetRemainingTime() {
        return remainingTime;
    }
}
