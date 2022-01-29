using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] private Vector3 moveVal;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody rb;
    private PlayerInput playerInput;
    private CharacterController controller;
    private Controls playerControls;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();

        playerControls = new Controls();
        playerControls.Player.Enable();
        playerControls.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Need to add character controller gravity. 
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = playerControls.Player.Move.ReadValue<Vector2>();
        Vector3 move = transform.right * inputVector.x + transform.forward * inputVector.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

}
