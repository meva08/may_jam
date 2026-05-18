using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private InputAction _moveAction;
    private CharacterController _characterController;

    void Awake()
    {
        // Explicitly enable the Player action map
        var playerMap = InputSystem.actions.FindActionMap("Player");
        if (playerMap != null)
        {
            playerMap.Enable();
            Debug.Log("Player action map enabled!");
        }
        else
        {
            Debug.Log("Player action map NOT found!");
        }

        _moveAction = InputSystem.actions.FindAction("Move");
        _characterController = GetComponent<CharacterController>();
        _moveAction.Enable();
    }

    void OnDestroy()
    {
        _moveAction.Disable();
    }

    void Update()
    {
        Vector2 moveVector = _moveAction.ReadValue<Vector2>();
        _characterController.Move(moveVector);
    }
}