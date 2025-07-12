using UnityEngine;
using UnityEngine.InputSystem;
public class playerControl : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody rb;
    public int jumpForce = 400;
    public int moveSpeed = 10;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        rb.AddForce(moveValue.x * moveSpeed, 0, moveValue.y * moveSpeed);

        Jump();
    }
    private void Jump() 
    {
        if (jumpAction.WasPressedThisFrame())
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
    
}
