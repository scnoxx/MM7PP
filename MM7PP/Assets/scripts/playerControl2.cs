using UnityEngine;
using UnityEngine.InputSystem;

public class playerControl2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    

    public float rotationSpeed;

    InputAction moveAction;
    InputAction jumpAction;

    public int moveSpeed = 10;
    public int maxSpeed = 30;
    public int jumpForce = 10;
    public int availableJumps;
    public float playerHeight;
    public LayerMask ground;
    bool grounded;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rb.maxLinearVelocity = maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        grounded = Physics.Raycast(rb.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        if (grounded)
        {
            availableJumps = 2;
            //Debug.Log(grounded);
        }

        Move();
        Jump();
    }

    private void Move()
    {
        Vector3 viewDir = player.position - transform.position;
        viewDir.y = 0f;
        orientation.forward = viewDir.normalized;
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        float verticalInput = moveValue.y;
        float horizontalInput = moveValue.x;
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        inputDir.Normalize();
        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir, Time.deltaTime * rotationSpeed);

            rb.MovePosition(rb.position + inputDir * moveSpeed * Time.deltaTime);
            //Debug.Log(moveValue);
        }
    }
    private void Jump()
    {
        if (jumpAction.WasPressedThisFrame() && availableJumps > 0)
        {

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce);
            availableJumps--;
        }
    }
}
