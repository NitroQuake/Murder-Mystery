using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool playerMovement = true;
    private float speed = 3.0f;
    private float verticalInput;
    private float horizontalInput;
    private Rigidbody2D rigidbodyP;
    private float maxSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyP = GetComponent<Rigidbody2D>();
        transform.position = new Vector2 (0, 11.5f);
    }
    
    // Player Movement
    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerMovement)
        {
            // Makes sure player doesn't go super fast
            if (rigidbodyP.velocity.magnitude > maxSpeed)
            {
                rigidbodyP.velocity = Vector2.ClampMagnitude(rigidbodyP.velocity, maxSpeed);
            }

            // get the user's input
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            // Physics instead of translate to avoid player going outside the walls
            rigidbodyP.AddForce(Vector3.up * speed * verticalInput);
            rigidbodyP.AddForce(Vector3.right * speed * horizontalInput);
        } else
        {
            rigidbodyP.velocity = Vector2.zero;
        }
    }

    // allows player to move or not to move
    public void canPlayerMove(bool move)
    {
        playerMovement = move;
    }
}
