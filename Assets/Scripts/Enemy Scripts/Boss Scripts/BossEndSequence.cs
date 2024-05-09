using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEndSequence : MonoBehaviour {
    BossTimer bossTimer;
    BossMechanics bossMechanics;
    HyperdriveVFX hyperdriveVFX;
    bool sequencePlayed = false;

    void Start() {
        bossTimer = FindAnyObjectByType<BossTimer>();
        bossMechanics = FindAnyObjectByType<BossMechanics>();
        HyperdriveVFX[] hyperdriveVFXs = FindObjectsByType<HyperdriveVFX>(FindObjectsSortMode.None);
        foreach (HyperdriveVFX hyperdriveVFX in hyperdriveVFXs) {
            if (hyperdriveVFX.CompareTag("Boss")) {
                this.hyperdriveVFX = hyperdriveVFX;
            }
        }
    }

    void Update() {
        if (!sequencePlayed) {
            if (bossTimer.GetRemainingTime() == 0f) {
                PlayerPrefs.SetInt("EscapeSequence", 1);
                BossEscapeSequence();
            }
            else if (!GameObject.FindGameObjectWithTag("Boss")) {
                BossDeathSequence();
            }
        }
    }

    void BossEscapeSequence() {
        hyperdriveVFX.PlayHyperdriveFX();
        bossMechanics.SetForcefieldVisibility(true);
        bossMechanics.ToggleBossUI();
        bossTimer.StopTimer();
        Invoke("LoadLossLevel", 3f);
        sequencePlayed = true;
    }

    void BossDeathSequence() {
        Invoke("LoadVictoryLevel", 3f);
        bossMechanics.ToggleBossUI();
        bossTimer.StopTimer();
        sequencePlayed = true;
    }

    void LoadLossLevel() {
        SceneManager.LoadScene("Celestria Destroyed");
    }

    void LoadVictoryLevel() {
        SceneManager.LoadScene("Celestria Saved");
    }
}
