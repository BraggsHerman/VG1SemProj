using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sarah
{
    public class PlayerController : MonoBehaviour
    {
        
        // Outlets - sibling components (Transform, SpriteRenderer)
        Rigidbody2D _rigidbody2D;
        private SpriteRenderer sprite;
        private Animator _animator;
    
        // Configuration - settings (max health, speed)
    

        // State Tracking - current state of things (health, current weapon)
    
        // Methods
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (Math.Abs(_rigidbody2D.velocity.magnitude) > 0)
            {
                if (_rigidbody2D.velocity.y > Math.Abs(_rigidbody2D.velocity.x))
                {
                    _animator.SetInteger("Status", 2);
                }
                else
                {
                    _animator.SetInteger("Status", 1);
                }
            }
            else
            {
                _animator.SetInteger("Status", 0);
            }
        }


        // Update is called once per frame
        void Update()
        {
            // Move Player Left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //happens over time, so we need delta time to account for lag
                _rigidbody2D.AddForce(Vector2.left * 12f * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = true;
            }
            
            // Move Player Right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _rigidbody2D.AddForce(Vector2.right * 12f * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = false;
            }
            
            // Move Player Up
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _rigidbody2D.AddForce(Vector2.up * 12f * Time.deltaTime, ForceMode2D.Impulse);
            }
            
            // Move Player Down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _rigidbody2D.AddForce(Vector2.down * 12f * Time.deltaTime, ForceMode2D.Impulse);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            // Check for collision with wall
            if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
            {
                // Check what is directly below our character's feet
                RaycastHit2D[] above = Physics2D.RaycastAll(transform.position, transform.up, 0.85f);
                RaycastHit2D[] below = Physics2D.RaycastAll(transform.position, -transform.up, 0.85f);
                RaycastHit2D[] left = Physics2D.RaycastAll(transform.position, -transform.right, 0.85f);
                RaycastHit2D[] right = Physics2D.RaycastAll(transform.position, transform.right, 0.85f);
                //Debug.DrawRay(transform.position, -transform.up * 0.7f);
                /*
                // We might have multiple things below our feet
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];
                    
                    // Check that we collided with ground right below our feet
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        // Reset jump count
                        jumpsLeft = 2;
                    }
                }*/
            }
        }
    }
}
    
    
   


