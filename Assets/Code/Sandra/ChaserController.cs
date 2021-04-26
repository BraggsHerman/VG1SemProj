using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HighSchool
{
    public class ChaserController : MonoBehaviour
{



   
        //Outlet
        Rigidbody2D rigidbody;
        
        //public GameObject projectilePrefab;
       
        //public static ChaserController instance;
        float randomSpeed;


        //configuration
        //public float moveSpeed;

        //State tracking

        // public float healthMax = 100f;
        // public float health = 100f;

        /*
        void Awake()
        {
            instance = this;
        }
        */

        // Start is called before the first frame update
        void Start()
        {
            rigidbody= GetComponent<Rigidbody2D>();
            randomSpeed = Random.Range(4f, 5f);
        }

        // Update is called once per frame
        void Update()
        {



            //time.deltaTime normalizes for frame rate
            //forcemode, apply force immediately
            //_rigidbody2D.AddForce(Vector2.left * 9f * Time.deltaTime, ForceMode2D.Impulse);
            //so that character turns in appropriate direction
            // _rigidbody2D.AddForce(Vector2.left * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);


            //move player right


            //time.deltaTime normalizes for frame rate
            //forcemode, apply force immediately
            //_rigidbody2D.AddForce(Vector2.right * 5f * Time.deltaTime, ForceMode2D.Impulse);
            // transform.position = new Vector2(0, Mathf.Sin(GameController.instance.timeElapsed) * 3f);
            rigidbody.velocity = Vector2.right * randomSpeed;
            // _rigidbody2D.AddForce(Vector2.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse); 








            /*
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
                //stop timer
                StopCoroutine("FiringTimer");
                GetComponent<Rigidbody2D>().gravityScale = 0;
                //so if we get shoved by an asteroid, ship will float away
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            }
            

            void OnCollisionEnter2D(Collision2D other)
            {

                //if its an asteroid , take damage
                if (other.gameObject.GetComponent<Obstacle>())
                {
                    TakeDamage(10f);
                }




            }
            */
        }
        
            }
}
