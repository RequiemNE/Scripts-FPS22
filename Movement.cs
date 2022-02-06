using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private Camera cam;
    [Tooltip("Higher value = lower sensitivity.")][SerializeField] private float mouseSensitivity;
    [Tooltip("Mouse clamp for looking up and down.")][SerializeField] private float xClamp;

    private Rigidbody rb;
    private PlayerInput playerInput;
    private CharacterController controller;
    private Controls playerControls;
    private Vector2 inputVector;


    private bool isGrounded;
    public float dist;
    public LayerMask mask;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();

        playerControls = new Controls();
        playerControls.Player.Enable();
        playerControls.Player.Jump.performed += Jump_performed;
        playerControls.Player.Look.performed += Look_performed;
        playerControls.Player.Fire.performed += Fire;
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        // pass to gun script. 

        // figure out how to actually manage weapons. 
    }

    private void Look_performed(InputAction.CallbackContext obj)
    {
        // Horizontal input.
        Vector2 inputValue = playerControls.Player.Look.ReadValue<Vector2>();
        gameObject.transform.Rotate(Vector3.up * inputValue.x / mouseSensitivity);

        // Vertical input.
        cam.transform.Rotate(Vector3.left * inputValue.y / mouseSensitivity);

        Mathf.Clamp(inputValue.y, xClamp, xClamp); // NEEDS WORK
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            Jump();
            
            // maybe just set a bool canJump here.
            // then use another fucition in update to jump.
            // function would be if canJump = true, then jump.
            // Once force is applied, canJump = false.
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
    }

    void FixedUpdate()
    {
        Move();
        Check();
        
    }

    private void Move()
    {
        inputVector = playerControls.Player.Move.ReadValue<Vector2>();
        Vector3 move = transform.right * inputVector.x + transform.forward * inputVector.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    private void Check()
    {
        isGrounded = Physics.CheckSphere(transform.position, dist, mask);

        if (!isGrounded)
        {
            velocity.y = -2f;
        }
    }

}
