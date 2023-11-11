using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    public float moveSpeed = 2; // Movement speed

    Rigidbody2D rb; // Rigidbody2D

    Vector2 directionToPlayer;

    PlayerMovement player; // The player

    Animator anim; // The animator

    bool facingRight = false; // Check if the slime is facing right

    public float damage = 5f; // Enemy damage

    public Image healthBar;

    public float maxHealth = 20;
    public float health;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
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

        if(health <= 0)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.33f);
            Destroy(gameObject);
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
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            TakeDamage(bullet.damage);
        }
    }
    void Flip() // Flips the sprite
    {
        facingRight = !facingRight; // If facingRight = true, then set facingRight = false. If facingRight = false, then set facingRight = true.
        transform.Rotate(0f, 180f, 0f);
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
    }
}
