using UnityEngine;
using UnityEngine.Playables;

public class LoadFromCheckpoint : MonoBehaviour {
    PlayableDirector playerTimeline;
    float checkpointTime = 57.5f;

    void Start() {
        playerTimeline = GetComponent<PlayableDirector>();
        if (PlayerPrefs.GetInt("CheckpointReached") == 1) {
            playerTimeline.time = checkpointTime;
        }
    }

    void Update() {
        CheckIfCheckPointReached();
    }

    void CheckIfCheckPointReached() {
        if (playerTimeline.time > checkpointTime) {
            PlayerPrefs.SetInt("CheckpointReached", 1);
        }
    }
}
