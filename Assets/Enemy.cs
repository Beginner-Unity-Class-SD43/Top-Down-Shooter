using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float moveSpeed = 2; // Movement speed

    Rigidbody2D rb; // Rigidbody2D

    Vector2 directionToPlayer;

    PlayerMovement player; // The player

    Animator anim; // The animator

    bool facingRight = false; // Check if the slime is facing right

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        directionToPlayer = (player.transform.position - transform.position);
        directionToPlayer.Normalize(); // Set vectors to 1

        anim.SetFloat("dirX", directionToPlayer.x);
        anim.SetFloat("dirY", directionToPlayer.y);

        // Flip the sprite
        if(directionToPlayer.x >= 0 && facingRight == false)
        {
            Flip();
        }
        else if(directionToPlayer.x < 0 && facingRight == true)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + directionToPlayer * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
