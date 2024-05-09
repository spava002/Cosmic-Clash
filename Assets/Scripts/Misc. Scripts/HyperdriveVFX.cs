using UnityEngine;

public class HyperdriveVFX : MonoBehaviour {
    [Tooltip("References the hyperdrive audio clip.")]
    [SerializeField] AudioClip hyperdriveAudio;

    ParticleSystem hyperdrive;
    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        Transform parent = transform.parent;
        foreach (Transform child in parent) {
            ParticleSystem particleComponent = child.GetComponent<ParticleSystem>();
            if (particleComponent) {
                hyperdrive = particleComponent;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("HyperdriveTrigger")) {
            PlayHyperdriveSFX();
            Invoke("PlayHyperdriveFX", 2f);
        }
    }

    void PlayHyperdriveSFX() {
        if (audioSource) {
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(hyperdriveAudio, 0.5f);
            }
        }
    }

    public void PlayHyperdriveFX() {
        if (!IsHyperdrivePlaying()) {
            hyperdrive.Play();
        }
    }

    public bool IsHyperdrivePlaying() {
        return hyperdrive.isPlaying;
    }
}
