using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class GoalScript : MonoBehaviour
{
    [SerializeField] private float powerThreshold = 3f;
    [SerializeField] private float transferRate = 5f;
    [SerializeField] private float transferRange = 2f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Light2D goalLight;

    // Color progression from dark purple → blue → yellow as it fills
    [SerializeField] private Color emptyColor = new Color(0.1f, 0.05f, 0.2f);
    [SerializeField] private Color fullColor = new Color(1f, 0.9f, 0.3f);

    private float currentPower = 0f;
    private Transform player;
    private PlayerLight playerLight;
    private bool playerInRange = false;
    [SerializeField] private GameObject hintText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerLight = player.GetComponent<PlayerLight>();
        spriteRenderer.color = emptyColor;
        goalLight.intensity = 0f;
        goalLight.color = emptyColor;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        playerInRange = distance <= transferRange;

        if (playerInRange && Keyboard.current.eKey.isPressed)
        {
            TransferLight();
        }

        float fillAmount = currentPower / powerThreshold;
        spriteRenderer.color = Color.Lerp(emptyColor, fullColor, fillAmount);
        goalLight.intensity = Mathf.Lerp(0f, 2f, fillAmount);
        goalLight.color = Color.Lerp(emptyColor, fullColor, fillAmount);
        goalLight.pointLightOuterRadius = Mathf.Lerp(1f, 5f, fillAmount);

        // hide hint once player starts filling
        if (hintText != null)
            hintText.SetActive(currentPower <= 0f);
    }

    void TransferLight()
    {
        if (playerLight.GetCurrentRadius() <= playerLight.GetMinRadius())
        {
            Debug.Log("Player light is too low to transfer.");
            return;
        }

        float amount = transferRate * Time.deltaTime;
        currentPower += amount;
        playerLight.ReduceRadius(amount);

        if (currentPower >= powerThreshold)
        {
            Win();
        }
    }

    void Win()
    {
        SceneManager.LoadScene("WinScene");
    }
}