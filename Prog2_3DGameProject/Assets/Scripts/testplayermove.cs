using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetsplayermove : MonoBehaviour
{
    private float horizontalMovement;
    private float verticalMovement;

    // A field editable from inside Unity with a default value of 5
    public float speed = 5.0f;

    // A field for the player's running speed
    public float runningSpeed = 15f;

    // A field for the player's jump force
    public float jumpForce = 5f;

    // How much will the player slide on the ground
    // The lower the value, the greater distance the user will slide
    public float drag;

    private Rigidbody rb;
    private bool isOnGround;

    // A field for the player's speed multiplier while jumping
    public float airMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Shout a short raycast downward to check if the player is standing on something
        float gameObjectHeight = gameObject.transform.localScale.y;
        isOnGround = Physics.Raycast(transform.position, Vector3.down, gameObjectHeight + 0.1f);

        // This will detect forward and backward movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // This will detect sideways movement
        verticalMovement = Input.GetAxisRaw("Vertical");

        // Calculate the direction to move the player
        Vector3 movementDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

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

        // If space is pressed, add force in an upward direction (jump)
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            // Add force in an upward direction (jump)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
