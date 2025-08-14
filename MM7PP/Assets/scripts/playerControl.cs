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
    public int availableJumps;
    public float playerHeight;
    public LayerMask ground;
    bool grounded;
    public int rotationSpeed = 7;
    /*
    public Transform orientation;
    public Transform playerObj;
    public Transform player;
    */

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
        
        
        
        
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        if (grounded) 
        { 
            availableJumps = 2;
        }
        
        Move();

        Jump();
    }
    private void Jump() 
    {
        if (jumpAction.WasPressedThisFrame() && availableJumps > 0)
        {
            
            rb.linearVelocity = new Vector3 (rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce);
            availableJumps--;
        }
    }
    
    private void Move()
    {
        //Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        //orientation.forward = viewDir.normalized;


        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 velocityDirection = new Vector3(moveValue.x, 0, moveValue.y);
        /*float verticalInput = moveValue.y;
        float horizontalInput = moveValue.x;

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero) 
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        */

        rb.AddForce(moveValue.x * moveSpeed, 0, moveValue.y * moveSpeed);
        
        if (Vector3.Dot(rb.linearVelocity, velocityDirection) < 0.95) 
        {
            //rb.linearVelocity *= 0.9f;
            rb.AddForce(moveValue.x * moveSpeed * 3, 0, moveValue.y * moveSpeed * 3);
        }

         
        
        

    }
   

}
