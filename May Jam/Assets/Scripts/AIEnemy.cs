using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public Transform player;
    private PlayerLight playerLight;
    public float speed;
    private float distance;
    public float distanceBetween;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerLight = player.GetComponent<PlayerLight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < playerLight.GetCurrentRadius() + distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
