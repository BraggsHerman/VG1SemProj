using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace HighSchool { 
    public class End : MonoBehaviour
{
   

        //outlets
        Rigidbody2D _rigidbody2D;


        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
        }

        // Update is called once per frame
        /*
        void Update()
        {

        }

        */

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<ControlPlayer>())
            {
                //stop game stuffs


                PlayerPrefs.DeleteKey("Score");
                //GC.instance.score = 0;
               
                Time.timeScale = 0;
                SceneManager.LoadScene("DungeonPerson");
            }
               

             
               
            }
        }
    }


