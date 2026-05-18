using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        exitButton.onClick.AddListener(OnExitClicked);

        // Hover effects for play button
        AddHoverEffect(playButton,
            normalColor: new Color(98f/255f, 132f/255f, 137f/255f),
            hoverColor: new Color(0f, 0.86f, 1f));

        // Hover effects for exit button text
        AddHoverEffect(exitButton,
            normalColor: new Color(98f/255f, 132f/255f, 137f/255f),
            hoverColor: new Color(0f, 0.86f, 1f));
    }

    void AddHoverEffect(Button button, Color normalColor, Color hoverColor)
    {
        var trigger = button.gameObject.AddComponent<EventTrigger>();

        var enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener(_ =>
        {
            button.GetComponent<Image>().color = hoverColor;
        });

        var exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener(_ =>
        {
            button.GetComponent<Image>().color = normalColor;
        });

        trigger.triggers.Add(enterEntry);
        trigger.triggers.Add(exitEntry);
    }

    void OnPlayClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    void OnExitClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}