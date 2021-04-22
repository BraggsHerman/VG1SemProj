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

        public float health = 200f;
        public float healthMax = 200f;

        public Image imageHealthBar;

        //State Tracking
        Transform target;


        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        void moveCharacter(Vector2 direction)
        {
            rigidbody.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        }
        void FixedUpdate()
        {
            moveCharacter(movement);
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

            
        }
    }
}

