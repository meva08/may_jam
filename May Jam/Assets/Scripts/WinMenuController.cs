using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WinMenuController : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button menuButton;

    void Start()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        menuButton.onClick.AddListener(OnMenuClicked);

        // Play Again button - gets brighter blue on hover
        AddHoverEffect(playAgainButton,
            normalColor: new Color(0f, 0.71f, 0.86f, 1f),
            hoverColor: new Color(0f, 0.86f, 1f, 1f));

       var menuText = menuButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        var trigger = menuButton.gameObject.AddComponent<EventTrigger>();

        var enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener(_ => menuText.color = Color.white);

        var exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener(_ => menuText.color = new Color(5f/255f, 5f/255f, 15f/255f));

        trigger.triggers.Add(enterEntry);
        trigger.triggers.Add(exitEntry);
    }

    void AddHoverEffect(Button button, Color normalColor, Color hoverColor)
    {
        var trigger = button.gameObject.AddComponent<EventTrigger>();

        var enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener(_ =>
            button.GetComponent<Image>().color = hoverColor);

        var exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener(_ =>
            button.GetComponent<Image>().color = normalColor);

        trigger.triggers.Add(enterEntry);
        trigger.triggers.Add(exitEntry);
    }

    void OnPlayAgainClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    void OnMenuClicked()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}