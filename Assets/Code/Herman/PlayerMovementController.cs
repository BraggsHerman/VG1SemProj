using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovementController : MonoBehaviour
{
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;


    public int maxHealth = 5;
    public SpriteRenderer spriteRenderer;

    public int health { get { return currentHealth; } }
    int currentHealth;

    public float speed = 3.0f;
    Rigidbody2D rb;
    float horizontal;
    float vertical;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public Animator anim;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        anim = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = rb.velocity.magnitude;
        anim.SetFloat("Speed", movementSpeed);
        Debug.Log(movementSpeed);

        if(movementSpeed > 0.1f) 
        {
            anim.SetFloat("movementX", rb.velocity.x);
            anim.SetFloat("movementY", rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("Attack");
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        // Debug.Log(horizontal);

        if (isInvincible) 
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        //flipping for direction
        if(vertical < 0 || horizontal < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }


    if(currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }


    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        transform.position = position;

        rb.MovePosition(position);

    }

 

    public void ChangeHealth(int amount)
    {
       
        if(amount > 0)
        {
            if (isInvincible) {
                  
                isInvincible = true;
                invincibleTimer = timeInvincible;
            }   
            
        } 
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);


    }




}
