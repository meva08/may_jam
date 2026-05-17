using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    [SerializeField] private float lightDrainPerSecond = 0.5f;
    public Transform player;
    private PlayerLight playerLight;
    public float speed;
    private float distance;
    public float distanceBetween;
    private Animator animator;

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

        if (distance < playerLight.GetCurrentRadius() + distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            
            // Update animator
            Vector2 velocity = (Vector2)player.position - (Vector2)transform.position;
            animator.SetFloat("Speed", velocity.magnitude);
            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", direction.y);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerLight.ReduceRadius(lightDrainPerSecond * Time.deltaTime);
        }
    }
}
