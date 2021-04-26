using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace Sarah
{
    public class PlayerController : MonoBehaviour
    {
        
        public static PlayerController instance;
        
        // Outlets - sibling components (Transform, SpriteRenderer)
        Rigidbody2D _rigidbody2D;
        private SpriteRenderer sprite;
        private Animator _animator;
        public Transform[] itemSpawners;
        public GameObject itemPrefab;
        public Image healthBarImage;
        public Text bottleScoreText;

        // Configuration - settings (max health, speed)
        public float speed;
        public float maxHealth;

        // State Tracking - current state of things (health, current weapon)
        public bool itemAvailable;
        private int status;
        public float health;
        public int bottleScore;
        public bool isPaused;
    
        // Methods
        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            SpawnItem();
            itemAvailable = true;
            status = 0;
            health = maxHealth;
            bottleScore = 0;
            isPaused = false;
        }

        private void FixedUpdate()
        {
            if (isPaused)
            {
                return;
            }
            
            _animator.SetInteger("PrevStatus", status);
            CheckVelocity();
            _animator.SetInteger("Status", status);
        }
        
        void Update()
        {
            if (isPaused)
            {
                return;
            }
            
            // In future, add something to indicate loss
            if (health <= 0)
            {
                Die();
            }
            
            // Move Player Left
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                //happens over time, so we need delta time to account for lag
                _rigidbody2D.AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = true;
            }
            
            // Move Player Right
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                _rigidbody2D.AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = false;
            }
            
            // Move Player Up
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                _rigidbody2D.AddForce(Vector2.up * speed * Time.deltaTime, ForceMode2D.Impulse);
            }
            
            // Move Player Down
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                _rigidbody2D.AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Impulse);
            }
            
            // Show menu
            if (Input.GetKey(KeyCode.P))
            {
                MenuController.instance.Show();
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
            UpdateBottleScore();
        }

        private void UpdateHealthBar()
        {
            healthBarImage.fillAmount = health / maxHealth;
        }

        void UpdateBottleScore()
        {
            ++bottleScore;
            bottleScoreText.text = ": " + bottleScore;
        }

        void Die()
        {
            GameOverScript.instance.Show();
        }
    }
}
    
    
   


