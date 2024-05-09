using UnityEngine;

public class SelfDestruct : MonoBehaviour {
    [Tooltip("Assigns the time in seconds before object is destroyed.")]
    [SerializeField] float selfDestructTime = 2f;

    void Start() {
        Destroy(gameObject, selfDestructTime);
    }
}
