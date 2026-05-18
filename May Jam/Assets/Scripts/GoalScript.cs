using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    [SerializeField] private float powerThreshold = 3f;
    [SerializeField] private float transferAmount = 1f;
    [SerializeField] private float transferRange = 2f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float currentPower = 0f;
    private Transform player;
    private PlayerLight playerLight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerLight = player.GetComponent<PlayerLight>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= transferRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            TransferLight();
        }

        float fillAmount = currentPower / powerThreshold;
        spriteRenderer.color = Color.Lerp(Color.grey, Color.yellow, fillAmount);
    }

    void TransferLight()
    {
        if (playerLight.GetCurrentRadius() <= playerLight.GetMinRadius())
        {
            Debug.Log("Player light is too low to transfer.");
            return;
        }

        currentPower += transferAmount;
        playerLight.ReduceRadius(transferAmount);
        Debug.Log($"Goal power: {currentPower}/{powerThreshold}");

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
