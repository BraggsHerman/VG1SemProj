using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public float speed;
    public bool vertical;
    

    Rigidbody2D rb;

    public float changeTime;
    float timer;
    int direction = 1;


    // Start is called before the first frame update
    void Start()
    {
         changeTime = Random.Range(3f, 15f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }
     void Update()
    {
        timer -= Time.deltaTime;
        
        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
            



        }

        if(direction < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

    }

    
    void FixedUpdate()
    {
        Vector2 position = rb.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMovementController player = other.gameObject.GetComponent<PlayerMovementController>();

        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
