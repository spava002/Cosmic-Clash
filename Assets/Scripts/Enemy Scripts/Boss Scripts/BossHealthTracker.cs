using UnityEngine;
using TMPro;

public class BossHealthTracker : MonoBehaviour {
    int bossHP;
    string bossName = "Star Destroyer";
    TMP_Text bossHPText;

    void Start() {
        bossHPText = GetComponent<TMP_Text>();
    }

    void Update() {
        DisplayHP();    
    }

    void DisplayHP() {
        bossHPText.text = bossName + "\nHealth: " + bossHP;
    }

    public void SetHP(int bossHP) {
        if (bossHP < 0) {
            bossHP = 0;
        }
        this.bossHP = bossHP;
    }
}
