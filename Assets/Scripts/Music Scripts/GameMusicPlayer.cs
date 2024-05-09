using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicPlayer : MonoBehaviour {
    [Header("Music Audio Clips")]
    [Tooltip("References game music.")]
    [SerializeField] AudioClip gameMusic;
    [Tooltip("References boss music.")]
    [SerializeField] AudioClip bossMusic;

    AudioSource audioSource;

    void Awake() {
        int numOfMusicPlayers = FindObjectsByType<GameMusicPlayer>(FindObjectsSortMode.None).Length;
        if (numOfMusicPlayers > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        PlayGameMusic();
    }

    void Update() {
        string currentLevelName = SceneManager.GetActiveScene().name;
        if (currentLevelName != "Celestria") {
            Destroy(gameObject);
        }
    }

    public void PlayGameMusic() {
        audioSource.Stop();
        audioSource.clip = gameMusic;
        audioSource.volume = 0.05f;
        audioSource.Play();
    }

    public void PlayBossMusic() {
        audioSource.Stop();
        audioSource.clip = bossMusic;
        audioSource.volume = 0.15f;
        audioSource.Play();
    }
}
