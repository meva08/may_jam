using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private InputAction _moveAction;
    private CharacterController _characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = _moveAction.ReadValue<Vector2>();
        _characterController.Move(moveVector);
    }
}
