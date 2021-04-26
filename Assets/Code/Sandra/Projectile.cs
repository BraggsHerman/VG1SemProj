using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HighSchool
{
    public class Projectile : MonoBehaviour
    {

        //outlets
        Rigidbody2D _rigidbody2D;


        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = transform.right * 10f;
        }

        // Update is called once per frame
        /*
        void Update()
        {

        }

        */

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Obstacle>())
            {

              if (other.gameObject.GetComponent<Obstacle>().health <= 0)
              {
                    
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                  //  GC.instance.EarnPoints(10);
               }
              //gameobject isthe projectile itself
                Destroy(gameObject);
                
                /*
                //Create explosion, destroy it soon after
                GameObject explosion = Instantiate(GC.instance.explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 0.25f);

               
                */
            }

            if (other.gameObject.GetComponent<End>())
            {
                Destroy(gameObject);
            }
        }
    }
}