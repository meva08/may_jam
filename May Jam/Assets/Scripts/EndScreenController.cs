using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private string gameSceneName = "MainScene";

    void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        var playAgainButton = root.Q<Button>("play-again-button");
        var menuButton = root.Q<Button>("menu-button");

        playAgainButton.clicked += OnPlayAgainClicked;
        menuButton.clicked += OnMenuClicked;

        // Hover effects for play again button
        playAgainButton.RegisterCallback<MouseEnterEvent>(e =>
            playAgainButton.style.opacity = 0.8f);
        playAgainButton.RegisterCallback<MouseLeaveEvent>(e =>
            playAgainButton.style.opacity = 1f);

        // Hover effects for menu button
        menuButton.RegisterCallback<MouseEnterEvent>(e =>
            menuButton.style.color = new StyleColor(new Color(1f, 1f, 1f)));
        menuButton.RegisterCallback<MouseLeaveEvent>(e =>
            menuButton.style.color = new StyleColor(new Color(0.59f, 0.59f, 0.71f)));
    }

    void OnPlayAgainClicked()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    void OnMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}