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
        
       
        float randomSpeed;


       
        void Start()
        {
            rigidbody= GetComponent<Rigidbody2D>();
            randomSpeed = Random.Range(3f, 4f);
        }

        // Update is called once per frame
        void Update()
        {



         
            rigidbody.velocity = Vector2.right * randomSpeed;
            // _rigidbody2D.AddForce(Vector2.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse); 








         
        }
        
            }
}
