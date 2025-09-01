using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] List<Rigidbody2D> rigidbodies;
    [SerializeField] float mSpeed;
    InputSystem_Actions inputActions;

    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.UI.Disable();
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += Move;
        inputActions.Player.Move.canceled += Move;

        inputActions.Player.Jump.performed += Jump;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Move(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.action.ReadValue<Vector2>();
        Debug.Log(dir);
        foreach (var rb in rigidbodies)
        {
            rb.linearVelocityX = dir.x * mSpeed;
        }

    }

    void Jump(InputAction.CallbackContext ctx)
    {

    }

    void OnDisable()
    {
        inputActions.Disable();
    }

}
