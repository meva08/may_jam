using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Rigidbody2D _rigidbody2D;
    Vector2 _moveVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _moveVector * _moveSpeed;
    }

    public void Move(Vector2 moveVector)
    {
        _moveVector = moveVector;
    }
}
