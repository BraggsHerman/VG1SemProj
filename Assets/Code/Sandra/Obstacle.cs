using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HighSchool
{
    public class Obstacle : MonoBehaviour
    {
        //state tracking
        public float healthMax = 80f;
        public float health = 80f;
        public static Obstacle instance;
        /*   
               void OnCollisionEnter2D(Collision2D other)
               {
                   //check for a component, is the object a projectile if we hit it, destroy
                   //type game object, compeneent projectile
                   if (other.gameObject.GetComponent<Projectile>())
                   {
                       //target and projectile clean themselves up
                       Destroy(gameObject);
                   }

               }



           //will also incorporate the obstacles to spawn from a spawn point

           public static Obstacle instance;
           //OUTLETS--> need thm to talk to a few things
           //need an array of transform
           public Transform[] spawnPoints;
           //spawnign random asteroids at the spawnpoints
           public GameObject[] obstaclePrefabs;
           public Text textScore;
           public float maxAsteroidDelay = 2f;
           public float minAsteroidDelay = 0.2f;

           //State trackign variable
           public float timeElapsed;
           //how long till we see another asteroid
           public float asteroidDelay;
           public int score;

           void Awake()
           {
               instance = this;
           }
           void Start()
           {
               StartCoroutine("ObstacleSpawnTimer");
               score = 0;

           }

       }
   }

   */

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

       
        void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0)
            {
               Destroy(gameObject);
            }
            //imageHealthBar.fillAmount = health / healthMax;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            //if its an asteroid , take damage
            if (other.gameObject.GetComponent<ControlPlayer>())
            {
                //if obstacles collide w character , it should disappear
                Destroy(gameObject);
            }

            //if its an asteroid , take damage
            if (other.gameObject.GetComponent<Projectile>())
            {
                TakeDamage(10f);
                if (health <= 0)
            {
                GC.instance.EarnPoints(10);
            }
            }

            

        }

      
    }

    
}