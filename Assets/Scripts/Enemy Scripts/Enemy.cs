using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Enemy Particle Objects")]
    [Tooltip("References the death effects game object.")]
    [SerializeField] GameObject deathFX;
    [Tooltip("References the hit effects game object.")]
    [SerializeField] GameObject hitFX;

    [Header("Enemy Attributes")]
    [Tooltip("Assigns enemy health points.")]
    [SerializeField] int enemyHP;
    [Tooltip("Assigns points earned on enemy takedown.")]
    [SerializeField] int enemyTakedownPoints;

    ScoreTracker scoreBoard;
    GameObject explosionsParent;
    float difficultyMultipler;
    BossHealthTracker bossHealthTracker;

    void Start() {
        scoreBoard = FindAnyObjectByType<ScoreTracker>();
        bossHealthTracker = FindAnyObjectByType<BossHealthTracker>();
        explosionsParent = GameObject.FindWithTag("RuntimeExplosions");
        AddRigidbody();
        ApplyHPModifier();
        if (gameObject.CompareTag("Boss")) {
            bossHealthTracker.SetHP(enemyHP);
        }
    }

    void OnParticleCollision(GameObject other) {
        DecreaseHealthPoints();
        PlayHitExplosion();
    }

    void AddRigidbody() {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }


    void ApplyHPModifier() {
        difficultyMultipler = PlayerPrefs.GetFloat("DifficultyMultiplier");
        // If no multiplier, set it to the default
        if (difficultyMultipler == 0f) {
            difficultyMultipler = 1f;
        }
        enemyHP = Mathf.RoundToInt(enemyHP * difficultyMultipler);
    }

    // Simulates destruction of minions since they need to "respawn" over time
    public void SetMinionStatus(bool isAlive, int newHP) {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders) {
            collider.enabled = isAlive;
        }
        GetComponent<MeshRenderer>().enabled = isAlive;
        if (isAlive) {
            enemyHP = newHP;
            ApplyHPModifier();
        }
    }

    void DecreaseHealthPoints() {
        enemyHP--;
        if (IsBoss()) {
            bossHealthTracker.SetHP(enemyHP);
        }
        if (enemyHP < 1 && !isDead()) {
            DestroyEnemy();
            AwardPoints();
        }
    }

    bool IsBoss() {
        return gameObject.CompareTag("Boss");
    }

    bool isDead() {
        bool meshRendererEnabled = GetComponent<MeshRenderer>().enabled;
        if (!gameObject || meshRendererEnabled == false) {
            return true;
        }
        return false;
    }

    void DestroyEnemy() {
        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = explosionsParent.transform;
        if (IsBossMinion()) {
            SetMinionStatus(false, 0);
        }
        else {
            Destroy(gameObject);
        }
    }

    bool IsBossMinion() {
        return gameObject.CompareTag("BossMinion");
    }

    void AwardPoints() {
        scoreBoard.IncreaseScore(enemyTakedownPoints);
    }

    void PlayHitExplosion() {
        GameObject hitVfx = Instantiate(hitFX, transform.position, Quaternion.identity);
        hitVfx.transform.parent = explosionsParent.transform;
    }

    public int GetEnemyHP() {
        return enemyHP;
    }
}
