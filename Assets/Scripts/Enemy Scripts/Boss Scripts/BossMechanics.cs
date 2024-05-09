using System;
using Unity.VisualScripting;
using UnityEngine;

public class BossMechanics : MonoBehaviour {
    GameMusicPlayer gameMusicPlayer;
    BossTimer bossTimer;
    ParticleSystem forcefield;
    GameObject[] minions;
    bool startSequence = true;

    void Start() {
        gameMusicPlayer = FindAnyObjectByType<GameMusicPlayer>();
        bossTimer = FindAnyObjectByType<BossTimer>();
        forcefield = GetComponentInChildren<ParticleSystem>();
        minions = GameObject.FindGameObjectsWithTag("BossMinion");
        SetMinionsVisibility(false);
        PlayerPrefs.DeleteKey("EscapeSequence");
    }

    void Update() {
        PlayerPrefs.SetInt("StartSequence", startSequence ? 1 : 0);
        bool escapeSequence = PlayerPrefs.GetInt("EscapeSequence") == 1;
        if (!escapeSequence) {
            if (AllMinionsDead()) {
                SetDamageState(true);
                SetForcefieldVisibility(true);
            }
            else {
                SetDamageState(false);
                SetForcefieldVisibility(false);
            }
        }
    }

    void SetMinionsVisibility(bool isVisible) {
        foreach(GameObject minion in minions) {
            minion.GetComponent<MeshRenderer>().enabled = isVisible;
        }
    }

    bool AllMinionsDead() {
        int totalMinions = minions.Length;
        int totalMinionsDead = 0;
        foreach (GameObject minion in minions) {
            if (!minion.GetComponent<MeshRenderer>().enabled) {
                totalMinionsDead++;
            }
        }
        return totalMinions == totalMinionsDead;
    }

    // If all minions alive, boss can't take damage
    // If all minions dead, boss can take damage
    void SetDamageState(bool canBeDamaged) {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders) {
            collider.enabled = canBeDamaged;
        }
    }

    public void SetForcefieldVisibility(bool invisible) {
        if (!startSequence) {
            if (invisible) {
                forcefield.Stop();
            }
            else {
                forcefield.Play();
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("MinionsVisibilityTrigger")) {
            SetMinionsVisibility(true);
            Invoke("ToggleBossUI", 8f);
            Invoke("StartBossTimer", 8f);
            Invoke("StartBossMusic", 8f);
            startSequence = false;
        }
    }

    public void ToggleBossUI() {
        GameObject bossUI = GameObject.FindGameObjectWithTag("BossUI");
        bool visibilityState = bossUI.GetComponent<Canvas>().enabled;
        bossUI.GetComponent<Canvas>().enabled = !visibilityState;
    }

    void StartBossTimer() {
        bossTimer.StartTimer();
    }
    
    void StartBossMusic() {
        // If player dies, we aren't accessing the same music player initialized at start
        // Check if its still the same. If not, define it again.
        if (!gameMusicPlayer) {
            gameMusicPlayer = FindAnyObjectByType<GameMusicPlayer>();
        }
        gameMusicPlayer.PlayBossMusic();
    }
}
