using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        public Image healthBarImage;

        // Configuration - settings (max health, speed)
        public float speed;
        public float maxHealth;

        // State Tracking - current state of things (health, current weapon)
        public bool itemAvailable;
        private int status;
        public float health;
    
        // Methods
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            SpawnItem();
            itemAvailable = true;
            status = 0;
            health = maxHealth;
        }

        private void FixedUpdate()
        {
            _animator.SetInteger("PrevStatus", status);
            CheckVelocity();
            _animator.SetInteger("Status", status);
        }


        // Update is called once per frame
        void Update()
        {
            // In future, add something to indicate loss
            if (health <= 0)
            {
                Die();
            }
            
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
            
            _animator.SetInteger("PrevStatus", status);
            CheckVelocity();
            _animator.SetInteger("Status", status);
        }

        private void SpawnItem()
        {
            Transform randomSpawner = itemSpawners[UnityEngine.Random.Range(0, itemSpawners.Length)];

            // Spawn a new item
            Instantiate(itemPrefab, randomSpawner.position, Quaternion.identity);

            itemAvailable = true;
        }

        void CheckVelocity()
        {
            Vector2 velocity = _rigidbody2D.velocity;
            float x = velocity.x;
            float y = velocity.y;
            if (velocity.sqrMagnitude > 0.1f)
            {
                if (y > 0 && y > Math.Abs(x))
                {
                    status = 1;
                }
                else if (y < 0 && Math.Abs(y) > Math.Abs(x))
                {
                    status = 3;
                }
                else
                {
                    status = 2;
                }
            }
            else
            {
                status = 0;
            }
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            UpdateHealthBar();
            if (health <= 0)
            {
                Die();
            }
           
        }

        public void RegenHealth(float amount)
        {
            health = Math.Min(health + amount, maxHealth);
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthBarImage.fillAmount = health / maxHealth;
        }

        void Die()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
    
    
   


