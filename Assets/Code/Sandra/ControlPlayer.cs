using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HighSchool
{
    public class ControlPlayer : MonoBehaviour
    {

        //Outlet
        Rigidbody2D _rigidbody2D;
        public Transform aimPivot;
        public GameObject projectilePrefab;
        public Image imageHealthBar;
        public static ControlPlayer instance;
        public int jumpsLeft;

        //configuration
        //public float moveSpeed;

        //State tracking

        public float healthMax = 100f;
        public float health = 100f;

        /*
        void Awake()
        {
            instance = this;
        }
        */

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health > 0)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //time.deltaTime normalizes for frame rate
                    //forcemode, apply force immediately
                    _rigidbody2D.AddForce(Vector2.left * 9f * Time.deltaTime, ForceMode2D.Impulse);
                    //so that character turns in appropriate direction
                    // _rigidbody2D.AddForce(Vector2.left * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
                }

                //move player right
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    //time.deltaTime normalizes for frame rate
                    //forcemode, apply force immediately
                    _rigidbody2D.AddForce(Vector2.right * 9f * Time.deltaTime, ForceMode2D.Impulse);
                    // _rigidbody2D.AddForce(Vector2.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse); 
                }

                //aim at the mouse--> get mouse coordinates
                Vector3 mousePosition = Input.mousePosition;
                Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
                //hypotensue of triangle for shooting
                Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

                //trig for angles
                /*
                float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
                float angleToMouse = radiansToMouse * Mathf.Rad2Deg;
                aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);
                */

                //SHOOT, right click is 0, left is 1
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //creates a game object when mouse is clicked
                    GameObject newProjectile = Instantiate(projectilePrefab);
                    //shoot where character is, in same position

                    newProjectile.transform.position = transform.position;
                    //position where the aimpivot is
                    newProjectile.transform.rotation = aimPivot.rotation;

                }


                //JUMP
                //get key happens every frame, getkeydown doesnt(happens once), passage of tiem doesnt matter
                //so no need to normalize using deltaTime
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {



                    if (jumpsLeft > 0)
                    {
                        jumpsLeft--;
                        _rigidbody2D.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);

                    }
                }
               
            }
        }

                void TakeDamage(float damageAmount)
                {
                    health -= damageAmount;
                    if (health <= 0)
                    {
                        Die();
                Time.timeScale = 0;
            }
                    imageHealthBar.fillAmount = health / healthMax;
                }

        void GiveHealth(float healthAmount)
        {
            health += healthAmount;
            /*
            if (health <= 0)
            {
                Die();
            }
            */
            imageHealthBar.fillAmount = health + healthMax;
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

            if (other.gameObject.GetComponent<HealthObject>())
            {
                GiveHealth(10f);
            }
            
            //check that we collided w ground (below our feet)
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                //check what is directly below character's feet, collection of hits
                //raycastall, full colleciton of things below feet, 'rays' should start from our positon
                //raycast downward, and dont wat to raycast forever, so limit distance
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 0.85f);
                Debug.DrawRay(transform.position, -transform.up * 0.85f);
                //may have multiple things below our feet, filter through hits
                //want to see if its ground, so loop through it

                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];
                    //check we collided w the ground right below our feet
                    //check if layer we hit is the gorund
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        //reset jump count
                        jumpsLeft = 2;
                    }
                }
            }
        }

    }

}