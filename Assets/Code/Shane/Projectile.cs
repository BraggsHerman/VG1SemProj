using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{
    public class Projectile : MonoBehaviour
    {
        //Outlets
        Rigidbody2D rigidbody;

        //State Tracking
        Transform target;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = transform.right * 10f;
        }

        // Update is called once per frame
        void Update()
        {
            
        }


        void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
            // Only explode on Boss
            if (other.gameObject.GetComponent<Boss>())
            {

                Destroy(gameObject);
                // Create an explosion and destroy it soon after 
                GameObject explosion = Instantiate(GameController.instance.explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 0.25f);

                
            }
        }
    }
}