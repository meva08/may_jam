using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        var playButton = root.Q<Button>("play-button");
        var exitButton = root.Q<Button>("exit-button");

        playButton.clicked += OnPlayClicked;
        exitButton.clicked += OnExitClicked;

        // Hover effects
        playButton.RegisterCallback<MouseEnterEvent>(e =>
            playButton.style.backgroundColor = new StyleColor(new Color(1f, 0.9f, 0.3f)));
        playButton.RegisterCallback<MouseLeaveEvent>(e =>
            playButton.style.backgroundColor = new StyleColor(new Color(1f, 0.78f, 0.2f)));

        exitButton.RegisterCallback<MouseEnterEvent>(e =>
            exitButton.style.color = new StyleColor(new Color(1f, 1f, 1f)));
        exitButton.RegisterCallback<MouseLeaveEvent>(e =>
            exitButton.style.color = new StyleColor(new Color(0.59f, 0.59f, 0.71f)));
    }

    void OnPlayClicked()
    {
        SceneManager.LoadScene("MainScene"); // replace with your game scene name
    }

    void OnExitClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}