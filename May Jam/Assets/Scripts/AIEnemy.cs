using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    [SerializeField] private float lightDrainPerSecond = 0.5f;
    private Animator animator;
    public Transform player;
    private PlayerLight playerLight;
    public float speed;
    private float distance;
    public float distanceBetween;
    private bool isTouchingPlayer = false;
    
    // sound playing variables
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip enemySound;
    [SerializeField] private AudioClip enemySpotSound;
    [SerializeField] private float fadeSpeed = 2f;
    private bool hasPlayedSound = false;
    private bool isFadingIn = false;
    private bool isFadingOut = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerLight = player.GetComponent<PlayerLight>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;

        bool inLight = distance < playerLight.GetCurrentRadius() + distanceBetween;

        if (inLight && !hasPlayedSound)
        {
            hasPlayedSound = true;

            if (enemySpotSound != null)
            {
                audioSource.PlayOneShot(enemySpotSound);
            }
        }
        else if (!inLight && hasPlayedSound)
        {
            hasPlayedSound = false;
            isFadingOut = true;
            isFadingIn = false;
        }

        if (isFadingIn)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 1f, fadeSpeed * Time.deltaTime);
            if (audioSource.volume >= 1f)
            {
                isFadingIn = false;
            }
        }

        if (isFadingOut)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0f, fadeSpeed * Time.deltaTime);
            if (audioSource.volume <= 0f)
            {
                audioSource.Stop();
                isFadingOut = false;
            }
        }

        if (isTouchingPlayer)
        {
            playerLight.ReduceRadius(lightDrainPerSecond);
        }
        if (inLight)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            animator.SetFloat("Speed", speed);
            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", direction.y);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }
}
