using System;
using UnityEngine;

public class MinionMechanics : MonoBehaviour {
    [Tooltip("Assigns time in seconds before a minion respawns again.")]
    [SerializeField] float minionRespawnTime;

    Enemy enemy;
    float startTime;
    bool isRespawning = false;
    int totalHP;
    bool startSequence;

    void Start() {
        enemy = GetComponent<Enemy>();
        totalHP = enemy.GetEnemyHP();
        ApplyRespawnModifier();
    }

    void ApplyRespawnModifier() {
        float difficultyMultipler = PlayerPrefs.GetFloat("DifficultyMultiplier");
        // If no multiplier, set it to the default
        if (difficultyMultipler == 0f) {
            difficultyMultipler = 1f;
        }
        minionRespawnTime = Mathf.RoundToInt(minionRespawnTime / difficultyMultipler);
    }

    void Update() {
        startSequence = PlayerPrefs.GetInt("StartSequence") == 1;
        if (!startSequence) {
            CheckIfDead();
            if (isRespawning) {
                float elapsedTime = Time.time - startTime;
                // Respawn the minion if enough time has passed and the boss is still alive
                if (elapsedTime > minionRespawnTime && BossIsAlive()) {
                    enemy.SetMinionStatus(true, totalHP);
                    isRespawning = false;
                }
            } 
        }
   
    }

    GameObject BossIsAlive() {
        return GameObject.FindWithTag("Boss");
    }

    void CheckIfDead() {
        if (!GetComponent<MeshRenderer>().enabled) {
            if (!isRespawning) {
                startTime = Time.time;
                isRespawning = true;
            }
        }
    }
}
