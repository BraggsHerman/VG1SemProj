using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace test
{
    public class Boss : MonoBehaviour
    {

        public static Boss instance;

        //Outlets
        Rigidbody2D rigidbody;
        public Transform player;
        public float speed = 10f;
        private Vector2 movement;
        Animator animator;
        SpriteRenderer sprite;

        public float health = 200f;
        public float healthMax = 200f;

        public Image imageHealthBar;

        //State Tracking
        Transform target;
        public float direction;


        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        void moveCharacter(Vector2 direction)
        {
            rigidbody.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        }
        void FixedUpdate()
        {
            //void FixedUpdate()
            //{
            //This update event is sync'd with the Physics Engine
            print("Speed:");
            print(speed);
            animator.SetFloat("Speed", speed);
            if (rigidbody.velocity.magnitude > 0)
            {
                animator.speed = rigidbody.velocity.magnitude / 3f;
            }
            else
            {
                animator.speed = 1f;
            }
            print(animator.speed);
            //}
            direction = movement.x;
            moveCharacter(movement);

            if (direction < 0) //moving left
            { 
                sprite.flipX = true;
            } else {
                sprite.flipX = false;
            }

            
        }
        // Update is called once per frame
        void Update()
        {
            Vector2 directionToTarget = player.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;


            rigidbody.MoveRotation(angle);

            directionToTarget.Normalize();
            movement = directionToTarget;

        }
        public void TakeDamage(float damageAmount)
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
            PlayerController.instance.EarnPoints(1000);
            GameController.instance.beatBoss = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            PolygonCollider2D[] polys = GetComponents<PolygonCollider2D>();
            foreach (PolygonCollider2D poly in polys)
                Destroy(poly);

            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Projectile>())
            {
                TakeDamage(20f);
            }
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                print("attacking");
                animator.SetBool("Attacking", true);
                
            }

            
        }
        
        void OnCollisionExit2D(Collision2D collision)
        {
            
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                print("not attacking");
                //Debug.Break();
                animator.SetBool("Attacking", false);
            }


        }
        


    }
}

