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
        public Transform[] itemSpawners;
        public GameObject itemPrefab;

        // Configuration - settings (max health, speed)
        public float speed;

        // State Tracking - current state of things (health, current weapon)
        public bool itemAvailable;
    
        // Methods
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            SpawnItem();
            itemAvailable = true;
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
                _rigidbody2D.AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = true;
            }
            
            // Move Player Right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _rigidbody2D.AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = false;
            }
            
            // Move Player Up
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _rigidbody2D.AddForce(Vector2.up * speed * Time.deltaTime, ForceMode2D.Impulse);
            }
            
            // Move Player Down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _rigidbody2D.AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Impulse);
            }

            if (!itemAvailable)
            {
                SpawnItem();
            }
        }

        private void SpawnItem()
        {
            Transform randomSpawner = itemSpawners[UnityEngine.Random.Range(0, itemSpawners.Length)];

            // Spawn a new item
            Instantiate(itemPrefab, randomSpawner.position, Quaternion.identity);

            itemAvailable = true;
        }
    }
}
    
    
   


