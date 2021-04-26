using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HighSchool
{
    public class HealthObject : MonoBehaviour
{



   
        //state tracking
       
        public static HealthObject instance;
    

        //asteroid uses normal physics so ue rigidbody
        Rigidbody2D rigidbody;
        //asteroid moves at random speed 
        //state tracking
        float randomSpeed;
        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            randomSpeed = Random.Range(0.5f, 3f);
        }

        // Update is called once per frame
        void Update()
        {
            rigidbody.velocity = Vector2.left * randomSpeed;
        }
        //when an object disappears
        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        /*
        void GiveHealth(float healthAmount)
        {
            health += healthAmount;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            //imageHealthBar.fillAmount = health / healthMax;
        }
        */

        void OnCollisionEnter2D(Collision2D other)
        {
            //if its an asteroid , take damage
            if (other.gameObject.GetComponent<ControlPlayer>())
            {
                //if obstacles collide w character , it should disappear
                Destroy(gameObject);
            }

            if (other.gameObject.GetComponent<Obstacle>())
            {
                //if obstacles collide w character , it should disappear
                Destroy(gameObject);
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Destroy(gameObject);
            }

           
        }
    }


}
