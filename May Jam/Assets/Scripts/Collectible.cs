using UnityEngine;
using UnityEngine.InputSystem;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float holdTime = 2f;
    [SerializeField] private float lightRestoreAmount = 2f;
    [SerializeField] private Color activeColor = new Color(0f, 1f, 1f, 1f);
    [SerializeField] private Color inactiveColor = new Color(0.2f, 0.2f, 0.2f, 1f);

    [HideInInspector] public int quadrantIndex; // set by spawner
    [HideInInspector] public QuadrantSpawner spawner; // set by spawner

    private bool playerInRange = false;
    private bool isActive = true;
    private bool isCollecting = false;
    private float holdProgress = 0f;
    private PlayerLight playerLight;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = activeColor;
    }

    void Update()
    {
        if (!isActive) return;
        if (!playerInRange && !isCollecting)
        {
            holdProgress = 0f;
            return;
        }

        bool holdingButton = Keyboard.current.eKey.isPressed;

        if (holdingButton)
        {
            holdProgress += Time.deltaTime;

            float pulse = Mathf.Lerp(1f, 0.5f, holdProgress / holdTime);
            spriteRenderer.color = new Color(activeColor.r * pulse, activeColor.g * pulse, activeColor.b * pulse, 1f);

            if (holdProgress >= holdTime)
            {
                isCollecting = true;
                Collect();
            }
        }
        else
        {
            holdProgress = 0f;
            spriteRenderer.color = activeColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            playerInRange = true;
            playerLight = other.GetComponent<PlayerLight>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollecting)
        {
            playerInRange = false;
            holdProgress = 0f;
            spriteRenderer.color = activeColor;
        }
    }

    private void Collect()
    {
        if (playerLight != null)
        {
            playerLight.RestoreRadius(lightRestoreAmount);
        }

        // Tell spawner to spawn a new one in this quadrant after delay
        if (spawner != null)
        {
            spawner.RespawnCollectibleInQuadrant(quadrantIndex);
        }

        Destroy(gameObject);
    }
}