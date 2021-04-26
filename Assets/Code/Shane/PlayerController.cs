using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace test
{
  public enum Direction
  {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
  }

  public class PlayerController : MonoBehaviour
  {

        public static PlayerController instance;

        // Outlet
        Rigidbody2D _rigidbody;
        public Transform aimPivot;
        public GameObject projectilePrefab;
        SpriteRenderer _spriteRenderer;
        Animator _animator;
        public KeyCode keyUp;
        public KeyCode keyDown;
        public KeyCode keyLeft;
        public KeyCode keyRight;
        public float moveSpeed;

        public Transform[] attackZones;

        public Image imageHealthBar;
        public Text scoreUI;

        //State Tracking
        public float health = 100f;
        public float healthMax = 100f;
        public int score;
        public bool isPaused;
        public Direction facingDirection;

        //Methods
        void Start(){
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            //GameObject.FindGameObjectWithTag("AimPivot").SetActive(false);

        }

        void Awake()
        {
            instance = this;
        }

        void LateUpdate()
        {
            if(String.Equals(_spriteRenderer.sprite.name, "FinalPlayer_0"))
            {
                facingDirection = Direction.Up;
            }
            else if (String.Equals(_spriteRenderer.sprite.name, "FinalPlayer_21"))
            {
                facingDirection = Direction.Right;
            }
            else if (String.Equals(_spriteRenderer.sprite.name, "FinalPlayer_7"))
            {
                facingDirection = Direction.Left;
            }
            else if (String.Equals(_spriteRenderer.sprite.name, "FinalPlayer_14"))
            {
                facingDirection = Direction.Down;
            }
        }

        void Update(){

            //Update UI
            scoreUI.text = score.ToString();

            if (isPaused)
            {
                return;
            }

            if (GameController.instance.hasBow)
            {
                GameObject.FindGameObjectWithTag("BowEquip").GetComponent<SpriteRenderer>().enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject newProjectile = Instantiate(projectilePrefab);
                    newProjectile.transform.position = transform.position;
                    newProjectile.transform.rotation = aimPivot.rotation;
                }
            }

            float movementSpeed = _rigidbody.velocity.magnitude;
            _animator.SetFloat("speed", movementSpeed);
            if (movementSpeed > 0.1f)
            {
                _animator.SetFloat("movementX", _rigidbody.velocity.x);
                _animator.SetFloat("movementY", _rigidbody.velocity.y);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetTrigger("attack");

                //Convert enum to index
                int facingDirectionIndex = (int)facingDirection;

                //Get attack zone from index
                Transform attackZone = attackZones[facingDirectionIndex];

                //What objects are within a circle at that attack zone
                Collider2D[] hits = Physics2D.OverlapCircleAll(attackZone.position, 0.1f);

                //Handle each hit target
                foreach(Collider2D hit in hits)
                {
                    //Breakable breakableObject = ;

                    if (hit.GetComponent<Breakable>())
                    {
                        hit.GetComponent<Breakable>().Break();
                    }
                    if (hit.GetComponent<Boss>())
                    {
                        hit.GetComponent<Boss>().TakeDamage(5f);
                    }
                    if (hit.GetComponent<Queen>())
                    {
                        hit.GetComponent<Queen>().TakeDamage(10f);
                    }

                    if (hit.GetComponent<EnemyController>())
                    {
                        hit.GetComponent<EnemyController>().TakeDamage(10f);
                    }
                                                           
                }
                
            }
            if (Input.GetKey(keyUp))
            {
                _rigidbody.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
            }
            if (Input.GetKey(keyDown))
            {
                _rigidbody.AddForce(Vector2.down * moveSpeed, ForceMode2D.Impulse);
            }
            if (Input.GetKey(keyLeft))
            {
                _rigidbody.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            }
            if (Input.GetKey(keyRight))
            {
                _rigidbody.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            }

            //Aim Toward Mouse
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0,0, angleToMouse);

           

            if (Input.GetKey(KeyCode.P))
            {
                print("p pressed");
                MenuController.instance.Show();
            }
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (collision.gameObject.GetComponent<EnemyController>())
            {
                TakeDamage(10f);
            }
            if (collision.gameObject.GetComponent<Queen>())
            {
                TakeDamage(15f);
            }
            if (collision.gameObject.GetComponent<Boss>())
            {
                TakeDamage(20f);
            }
           
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<EnemyController>())
            {
                if (collision.gameObject.tag == "Fire")
                {
                    TakeDamage(1);
                    GameObject.FindGameObjectWithTag("Fire").GetComponent<AudioSource>().enabled = true;
                }
                else
                {
                    TakeDamage(1);
                }
                    
            }
            if (collision.gameObject.GetComponent<Queen>())
            {
                TakeDamage(1);
            }
            if (collision.gameObject.GetComponent<Boss>())
            {
                TakeDamage(1);
            }

        }
      
        void OnTriggerExit2D(Collider2D other)
        {
           
            if (other.gameObject.tag == "Fire")
            {
                GameObject.FindGameObjectWithTag("Fire").GetComponent<AudioSource>().enabled = false;
            }

        }
        void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                Die();
            }
            imageHealthBar.fillAmount = health / healthMax;
        }

        void Die()
        {
            GameController.instance.youLose = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            Destroy(GetComponent<CapsuleCollider2D>());
        }

        public void EarnPoints(int pointAmount)
        {
            score += Mathf.RoundToInt(pointAmount);
        }

        void UpdateDisplay()
        {
            scoreUI.text = score.ToString();
        }

    }
}
