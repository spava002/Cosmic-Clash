using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    [Header("Player Explosion Effects")]
    [Tooltip("Particle system that is played upon collision.")]
    [SerializeField] ParticleSystem explosionFX;
    [Tooltip("Audio clip that is played upon collision.")]
    [SerializeField] AudioClip explosionSFX;

    [Tooltip("References player timeline that is paused upon collision.")]
    [SerializeField] PlayableDirector playerTimeline;

    GameObject enemyWaves;
    GameMusicPlayer gameMusicPlayer;
    AudioSource audioSource;
    float reloadLevelDelay = 1f;

    void Start() {
        enemyWaves = GameObject.FindGameObjectWithTag("EnemyWaves");
        gameMusicPlayer = FindAnyObjectByType<GameMusicPlayer>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) {
        InitiateCrashSequence();
    }

    void InitiateCrashSequence() {
        PauseTimeline();
        HideEnemyWaves();
        DisablePlayerMovement();
        DisablePlayerFiring();
        PlayExplosionFX();
        PlayExplosionSFX();
        DisableCollisions();
        Invoke("ReloadLevel", reloadLevelDelay);
    }

    void PauseTimeline() {
        playerTimeline.Pause();
    }

    void HideEnemyWaves() {
        enemyWaves.SetActive(false);
    }

    void DisablePlayerMovement() {
        GetComponent<PlayerControls>().enabled = false;
    }

    void DisablePlayerFiring() {
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particleSystems) {
            particle.Stop();
        }
    }

    void PlayExplosionFX() {
        explosionFX.Play();
    }

    void PlayExplosionSFX() {
        audioSource.PlayOneShot(explosionSFX);
    }

    void DisableCollisions() {
        // Disables so no more crashes can occur
        GetComponent<MeshRenderer>().enabled = false;
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders) {
            collider.enabled = false;
        }
    }

    void ReloadLevel() {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
        // Returns back to normal game music since checkpoint begins before boss fight starts
        if (PlayerPrefs.GetInt("CheckpointReached") == 1) {
            gameMusicPlayer.PlayGameMusic();
        }
    }
}
