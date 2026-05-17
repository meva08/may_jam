using UnityEngine;
using UnityEngine.UIElements;

public class UIScaler : MonoBehaviour
{
    private UIDocument uiDocument;
    private Camera mainCamera;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        mainCamera = Camera.main;
        ScaleToScreen();
    }

    void ScaleToScreen()
    {
        float height = mainCamera.orthographicSize * 2f;
        float width = height * mainCamera.aspect;

        var panelSettings = uiDocument.panelSettings;
        panelSettings.referenceResolution = new Vector2Int(Screen.width, Screen.height);

        transform.localScale = Vector3.one;
        GetComponent<RectTransform>()?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        GetComponent<RectTransform>()?.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}