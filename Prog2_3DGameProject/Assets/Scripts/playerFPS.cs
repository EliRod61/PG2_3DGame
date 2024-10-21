using UnityEngine;

public class playerFPS : MonoBehaviour
{
    public Vector3 Velocity;
    public Vector3 movementInput;
    public Vector2 mouseInput;
    private float xRotation;

    
    public Transform PlayerCamera;
    public CharacterController Controller;
    public Transform Player;

    [Header("Movement")]
    public float Speed;
    public float JumpForce;
    public float Sensitivity;
    public float Gravity = 9.81f;
    public float sprintSpeed;

    [Header("Sneaking")]
    private bool Sneaking = false;
    private bool Sneak = false;
    private bool isOnGround;
    public float sneakSpeed;


    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        MoveCamera();
        Jump();
        Crouch();

        //run a check for crouch or jump keys, then set a boolean equal to true if requested
    }

    private void FixedUpdate()
    {
        //inside fixedUpdate we put all the physics or simulated physics
        //updates - like player move

        //first call a direction to find current requested inputs
        //if the player has requested jump or crouch, update status and run jump or crouch functions
        //update player state and booleans for jump,crouch,etc
        //based on status, run movement functions here
    }

    private void LateUpdate()
    {
        //if Update produces jitter, which it usually does for cameras,
        //then run your camera code inside LateUpdate instead
    }

    private void playerMovement()
    {
        Controller.Move(Velocity * Time.deltaTime);

        //WASD movement
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //Aim movement (First Person)
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // If left shift is held down, use the runningSpeed as the speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //rb.AddForce(Speed * sprintMoveSpeed, ForceMode.Force);
        }
        // Else (left shift is not held down) use the normal speed
        else
        {
            // Move the player
            //rb.AddForce(movementDirection * speed, ForceMode.Force);
        }
    }

    private void Jump()
    {
        //make the player jump
        if (Controller.isGrounded)
        {
            Velocity.y = -1f;
            isOnGround = true;

            if (Input.GetKeyDown(KeyCode.Space) && Sneaking == false)
            {
                Velocity.y = JumpForce;
            }
        }
        else
        {
            Velocity.y += Gravity * -2f * Time.deltaTime;
            isOnGround = false;
        }
    }

    private void EnterCrouch()
    {
        //update booleans or enums in here
        //one thing we might check for is grounded status
        //if grounded fails, reject the attempt to enterCrouch  
    }
    private void ExitCrouch()
    {

    }

    void Crouch()
    {
        //run the actual movement code
        //when leftCTRL pressed and sneak is false: crouch and tell unity we are sneaking
        if (Input.GetKey(KeyCode.LeftControl) && Sneak)
        {
            Player.localScale = new Vector3(1f, 0.5f, 1f);
            Sneaking = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Player.localScale = new Vector3(1f, 1f, 1f);
            Sneaking = false;
        }

        Vector3 MoveVector = transform.TransformDirection(movementInput); 

        //when sneaking is true, decrease speed: have normal speed otherwise
        if (Sneaking)
        {
            Controller.Move(MoveVector * SneakSpeed * Time.deltaTime);
        }
        else
        {
            Controller.Move(MoveVector * Speed * Time.deltaTime);
        }
    }
    private void MoveCamera()
    {
        xRotation -= mouseInput.y * Sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(0f, mouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
