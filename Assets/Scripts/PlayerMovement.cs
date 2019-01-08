using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D playerRB;
    private Transform playerTR;
    public float maxSpeed;          // the max speed the object will travel at
    public float acceleration;      // how quickly the object reaches max speed
    public float rotationSpeed;     // how quickly the object rotates
    public float turnForce;         // how quickly the object's force rotates
    private int pressed;            // keeps track if a key is already pressed
    public float dashSpeed;         // how strongly the objbect dashes


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "RedWall")
        {
            Destroy(gameObject);
            return;
        }
    }

    // Use this for initialization
    void Start () {
        // Gets the RigidBody2D of the gameObject that this script is attached to
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        // Gets the Transform of the gameObject that this script is attached to
        playerTR = gameObject.GetComponent<Transform>();

        maxSpeed = 2.5f;
        acceleration = 50f;
        rotationSpeed = 40f;
        turnForce = -0.6f;
        pressed = 0;
        dashSpeed = 28f;
    }

// FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
void FixedUpdate()
    {

        // Use 'pressed' to ensure one key press cant overide another
        float h = 0f;
        if (Input.GetKey("left") && (pressed >= 0))
        {
            h = 1f;
            pressed = 1;
        }
        else if (Input.GetKey("right") && (pressed <= 0))
        {
            h = -1f;
            pressed = -1;
        }
        else
        {
            h = 0f;
            pressed = 0;
        }

        //Vector2 movementDir = new Vector2(0, 1);     //(horizontal, vertical)
        Vector2 movementDir = transform.up + (transform.right * h * turnForce);

        // Only add force if the player is below the max speed
        if (playerRB.velocity.magnitude < maxSpeed)
        {
            playerRB.AddForce(movementDir * acceleration);
        }

        // Adding torque for turning
        if (playerRB.velocity.magnitude < maxSpeed)
        {
            playerRB.AddTorque(rotationSpeed * h);
        }
        else
        {
            playerRB.AddTorque(rotationSpeed/3 * h);
        }


        // Dash
        // Dash when one direction is pressed while the other is held
        if (pressed == 1 && Input.GetKeyDown("right"))
        {
            playerRB.AddForce(( (transform.up * -1f) + (transform.right * 2f)) * dashSpeed, ForceMode2D.Impulse);
        }
        else if (pressed == -1 && Input.GetKeyDown("left"))
        {
            playerRB.AddForce(( (transform.up * -1f) + (-transform.right * 2f)) * dashSpeed, ForceMode2D.Impulse);
        }

        Debug.Log(playerRB.velocity.magnitude);

        //Debug.Log("H= " + playerRB.angularVelocity);

        // maintain speed and velocity and max speed
        //playerRB.velocity = (Vector3.Lerp(playerRB.velocity, transform.forward, Time.deltaTime)).normalized * maxSpeed;
    }

}
