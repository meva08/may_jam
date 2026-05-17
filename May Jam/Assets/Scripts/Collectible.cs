using UnityEngine;
using UnityEngine.InputSystem;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float holdTime = 2f;  // seconds to hold for collection
    [SerializeField] private float lightRestoreAmount = 2f; // how much radius to restore
    [SerializeField] private SpriteRenderer progressRenderer; // optional visual feedback

    private bool playerInRange = false;
    private float holdProgress = 0f;
    private PlayerLight playerLight;

    void Update()
    {
        if (!playerInRange) 
        {
            holdProgress = 0f;
            return;
        }

        bool holdingButton = Keyboard.current.eKey.isPressed;

        if (holdingButton)
        {
            holdProgress += Time.deltaTime;

            // shrink the collectible as progress increases
            float scale = Mathf.Lerp(1f, 0f, holdProgress / holdTime);
            transform.localScale = new Vector3(scale, scale, scale);

            if (holdProgress >= holdTime)
            {
                Collect();
            }
        }
        else
        {
            // Reset progress if they let go
            holdProgress = 0f;
            transform.localScale = Vector3.one;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerLight = other.GetComponent<PlayerLight>();
        }
    }

    private void Collect()
    {
        if (playerLight != null)
        {
            playerLight.RestoreRadius(lightRestoreAmount);
        }
        Destroy(gameObject);
    }
}