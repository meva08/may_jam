using UnityEngine;
using UnityEngine.UI;

public class IntroPopUp : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    void Start()
    {
        // Pause game while popup is open
        Time.timeScale = 0f;
        closeButton.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
