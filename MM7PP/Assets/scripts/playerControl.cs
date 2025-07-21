using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class playerControl : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;
    InputAction sprint;
    Rigidbody rb;
    public int jumpForce = 400;
    public int moveSpeed = 10;
    public int sprintSpeed = 2;
    public int maxSpeed = 30;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprint = InputSystem.actions.FindAction("Sprint");
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Jump();
    }
    private void Jump() 
    {
        if (jumpAction.WasPressedThisFrame())
        {
            rb.linearVelocity = new Vector3 (rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
    
    private void Move()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rb.AddForce(moveValue.x * moveSpeed, 0, moveValue.y * moveSpeed);
    }
   

}
