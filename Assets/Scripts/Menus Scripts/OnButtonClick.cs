using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour {
    [Tooltip("References the scene name that will be loaded.")]
    [SerializeField] string targetScene;

    Button button;

    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleButtonClick);
    }

    void HandleButtonClick() {
        if (targetScene.Equals("Quit")) {
            Application.Quit();
        }
        else {
            SceneManager.LoadScene(targetScene);
        }
    }
}
