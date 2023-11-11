using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5; // Player's move speed

    Rigidbody2D rb; // Player's rigidbody2D

    Vector2 movement; // Player's movement vector

    Vector2 mousePos; // Mouse's position vector

    public GameObject gun; // Gun child object

    Animator anim; // Animator component

    bool facingRight = true; // Checks if player is facing right

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize(); // Sets all vectors = 1

        // Get Mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Animator
        anim.SetFloat("mouseX", mousePos.x);
        anim.SetFloat("mouseY", mousePos.y);

        if(mousePos.x > transform.position.x && facingRight == false) // If the player is facing left
        {
            Flip();
        } 
        else if(mousePos.x < transform.position.x && facingRight == true)
        {
            Flip();
        }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Flip() // Flips the sprite
    {
        facingRight = !facingRight; // If facingRight = true, then set facingRight = false. If facingRight = false, then set facingRight = true.
        transform.Rotate(0f, 180f, 0f);
    }
}
