using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Rigidbody2D _rigidbody2D;
    private Animator animator;
    Vector2 _moveVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _moveVector * _moveSpeed;

        float speed = _rigidbody2D.linearVelocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (speed > 0.1f)
        {
            float x = _rigidbody2D.linearVelocity.x;
            float y = _rigidbody2D.linearVelocity.y;

            // only allow movement in one direction at a time
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                animator.SetFloat("DirectionX", x);
                animator.SetFloat("DirectionY", 0f);
                animator.SetFloat("LastDirectionX", x);
                animator.SetFloat("LastDirectionY", 0f);
            }
            else
            {
                animator.SetFloat("DirectionX", 0f);
                animator.SetFloat("DirectionY", y);
                animator.SetFloat("LastDirectionX", 0f);
                animator.SetFloat("LastDirectionY", y);
            }
        }
        else
        {
            animator.SetFloat("DirectionX", animator.GetFloat("LastDirectionX"));
            animator.SetFloat("DirectionY", animator.GetFloat("LastDirectionY"));
        }
    }

    public void Move(Vector2 moveVector)
    {
        if (Mathf.Abs(moveVector.x) > Mathf.Abs(moveVector.y))
        {
            _moveVector = new Vector2(moveVector.x, 0f);
        }
        else
        {
            _moveVector = new Vector2(0f, moveVector.y);
        }
    }
}
