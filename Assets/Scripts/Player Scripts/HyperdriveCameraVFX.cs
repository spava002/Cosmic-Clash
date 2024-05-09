using UnityEngine;

public class HyperdriveCameraVFX : MonoBehaviour {
    [Header("Camera FOV Increase/Decrease Attributes")]
    [Tooltip("Assigns the max FOV that the camera will reach.")]
    [SerializeField] float maxFOV = 120f;
    [Tooltip("Assigns the speed at which the camera FOV will increase at.")]
    [SerializeField] float cameraFOVIncreaseFactor = 15f;
    [Tooltip("Assigns the min FOV that the camera will reach.")]
    [SerializeField] float minFOV = 80f;
    [Tooltip("Assigns the speed at which the camera FOV will decrease at.")]
    [SerializeField] float cameraFOVDecreaseFactor = -20f;

    Camera mainCamera;
    ParticleSystem hyperdrive;
    float startTime;
    float elapsedTime;
    
    void Start() {
        Transform parent = transform.parent;
        foreach (Transform child in parent) {
            ParticleSystem particleComponent = child.GetComponent<ParticleSystem>();
            Camera cameraComponent = child.GetComponent<Camera>();
            if (particleComponent) {
                hyperdrive = particleComponent;
            }
            else if (cameraComponent) {
                mainCamera = cameraComponent;
            }
        }
    }

     void Update() {
        if (hyperdrive.isPlaying && mainCamera) {
            AdjustCameraFOV();
        }
    }

    void AdjustCameraFOV() {
        SetStartTime();
        SetElapsedTime();
        float currentCameraFOV = mainCamera.fieldOfView;
        if (elapsedTime < 4f) {
            UpdateFOV(cameraFOVIncreaseFactor, maxFOV);
        }
        else {
            UpdateFOV(cameraFOVDecreaseFactor, currentCameraFOV);
        }
    }

    void SetStartTime() {
        if (startTime == 0) {
            startTime = Time.time;
        }
    }

    void SetElapsedTime() {
        elapsedTime = Time.time - startTime;
    }

    void UpdateFOV(float cameraFOVUpdateFactor, float maxFOV) {
        float currentCameraFOV = mainCamera.fieldOfView;
        float newFOV = currentCameraFOV + Time.deltaTime * cameraFOVUpdateFactor;
        mainCamera.fieldOfView = Mathf.Clamp(newFOV, minFOV, maxFOV);
    }
}
