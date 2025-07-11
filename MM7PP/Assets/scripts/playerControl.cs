using UnityEngine;
using UnityEngine.InputSystem;
public class playerControl : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody rb;
    public int jumpForce = 40;


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

        if (jumpAction.IsPressed()) 
        {
            rb.AddForce(Vector3.up * jumpForce);         
        }
    }
}
