using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {
    [Header("Input Attributes")]
    [Tooltip("Assigns keys for up/down/left/right player movement.")] 
    [SerializeField] InputAction movement;
    [Tooltip("Assigns key for player firing.")]
    [SerializeField] InputAction fire;

    [Header("Lasers Control")]
    [Tooltip("Assigns lasers used on player ship.")]
    [SerializeField] GameObject[] lasers;

    [Header("Ship Movement Attributes")]
    [Tooltip("Assigns rate of speed which player can move on the screen.")]
    [SerializeField] float speedFactor = 20f;
    [Tooltip("Assigns maximum range on x-axis that player can move to.")]
    [SerializeField] float xRange = 20f;
    [Tooltip("Assigns maximum range on y-axis that player can move to.")]
    [SerializeField] float yRange = 12f;

    [Header("Screen Position/Rotation Attributes")]
    [Tooltip("Assigns rate of which pitch related to the ship position is changed.")]
    [SerializeField] float positionPitchFactor = -3f;
    [Tooltip("Assigns rate of which pitch related to the ship rotation is changed.")]
    [SerializeField] float controlPitchFactor = -15f;
    [Tooltip("Assigns rate of which yaw related to the ship position is changed.")]
    [SerializeField] float positionYawFactor = 2f;
    [Tooltip("Assigns rate of which roll related to the ship rotation is changed.")]
    [SerializeField] float controlRollFactor = -10f;

    HyperdriveVFX hyperdriveVFX;
    float xMovement;
    float yMovement;

    void Start() {
        HyperdriveVFX[] hyperdriveVFXs = FindObjectsByType<HyperdriveVFX>(FindObjectsSortMode.None);
        foreach (HyperdriveVFX hyperdriveVFX in hyperdriveVFXs) {
            if (hyperdriveVFX.CompareTag("Player")) {
                this.hyperdriveVFX = hyperdriveVFX;
            }
        }
    }

    // Good practice to have enable/disable methods
    void OnEnable() {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable() {
        movement.Disable();
        fire.Disable();
    }

    void Update() {
        PlayerMovement();
        PlayerRotation();
        PlayerFire();
    }

    void PlayerMovement() {
        // Lower player movement range when hyperdrive is on
        if (hyperdriveVFX.IsHyperdrivePlaying()) {
            xRange = 8f;
            yRange = 4f;
        }
        else {
            xRange = 20f;
            yRange = 12f;
        }
        xMovement = movement.ReadValue<Vector2>().x;
        yMovement = movement.ReadValue<Vector2>().y;

        float xOffset = xMovement * Time.deltaTime * speedFactor;
        float newXPosition = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXPosition, -xRange, xRange);

        float yOffset = yMovement * Time.deltaTime * speedFactor;
        float newYPosition = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPosition, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, 0f);
    }

    void PlayerRotation() {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToMovement = yMovement * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToMovement;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xMovement * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    // Could add overheating feature if space is held for 5 seconds
    void PlayerFire() {
        if (fire.ReadValue<float>() > 0.1) {
            ActivateLasers(true);
        }
        else {
            ActivateLasers(false);
        }
    }

    void ActivateLasers(bool isActive) {
        foreach (GameObject laser in lasers) {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
