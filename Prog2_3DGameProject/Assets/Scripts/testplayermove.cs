using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetsplayermove : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform PlayerCamera;

    private float horizontalMovement;
    private float verticalMovement;

    public float speed = 5.0f;
    public float runningSpeed = 15f;
    public float jumpForce = 5f;

    // How much will the player slide on the ground
    // The lower the value, the greater distance the user will slide
    public float drag;

    // A field for the player's speed multiplier while jumping
    public float airMultiplier;

    private Rigidbody rb;
    private bool isOnGround;


    //CAMERA Variables 
    public Vector2 PlayerMouseInput;
    [SerializeField] private float Sensitivity;
    private float xRotation;

    //Crouch
    private bool Sneaking = false;
    private bool Sneak = false;
    public float sneakSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Shout a short raycast downward to check if the player is standing on something
        float gameObjectHeight = gameObject.transform.localScale.y;
        isOnGround = Physics.Raycast(transform.position, Vector3.down, gameObjectHeight + 0.1f);

        //Aim movement (First Person)
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Walk();
        Jump();
        Crouch();
        sprint();
        MoveCamera();
    }
    public void Walk()
    {
        // This will detect forward and backward movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // This will detect sideways movement
        verticalMovement = Input.GetAxisRaw("Vertical");
    }
    public void Jump()
    {
        // If space is pressed, add force in an upward direction (jump)
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            // Add force in an upward direction (jump)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Sneaking = false;
        }
        if (Input.GetKey(KeyCode.LeftControl) && isOnGround && Sneaking == true)
        {
            Player.localScale = new Vector3(1f, 0.5f, 1f);
            Sneaking = true;
        }
    }
    public void Crouch()
    {

        // Calculate the direction to move the player
        Vector3 movementDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

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


        //when sneaking is true, decrease speed: have normal speed otherwise
        if (Sneaking)
        {
            rb.AddForce(movementDirection * sneakSpeed, ForceMode.Force);
        }
        else
        {
            Walk();
        }
    }

    public void sprint()
    {
        // Calculate the direction to move the player
        Vector3 movementDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

        // If left shift is held down, use the runningSpeed as the speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(movementDirection * runningSpeed, ForceMode.Force);
        }
        // Else (left shift is not held down) use the normal speed
        else
        {
            // Move the player
            rb.AddForce(movementDirection * speed, ForceMode.Force);
        }

        // Apply drag only if on the ground
        if (isOnGround)
        {
            rb.drag = drag;
        }
        else
        {
            movementDirection *= airMultiplier;
            rb.drag = 0;
        }
    }
    private void MoveCamera()
    {
        xRotation -= PlayerMouseInput.y * Sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
