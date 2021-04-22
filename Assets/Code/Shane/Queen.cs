using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace test
{
    public class Queen : MonoBehaviour
    {

        public static Queen instance;

        //Outlets
        Rigidbody2D rigidbody;
        public Transform player;
        public float speed = 10f;
        private Vector2 movement;

        public float health = 100f;
        public float healthMax = 100f;

        

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
                PlayerController.instance.EarnPoints(200);
            }
        }

        void Die()
        {
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Projectile>())
            {
                print("projectile");
                TakeDamage(10f);
            }

            
        }
    }
}

