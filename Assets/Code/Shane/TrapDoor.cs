using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace test
{
    public class TrapDoor : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObject.name == "TrapBoundary")
            {

                if (other.gameObject.GetComponent<PlayerController>())
                {
                    GameObject.FindGameObjectWithTag("TrapDoor").GetComponent<BoxCollider2D>().enabled = true;
                    GameObject.FindGameObjectWithTag("TrapDoor").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.FindGameObjectWithTag("BossCageTemp").GetComponent<BoxCollider2D>().enabled = false;
                    GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Image>().enabled = true;
                }
            }
            else
            {
                if (other.gameObject.GetComponent<PlayerController>())
                {
                   
                    GameObject.FindGameObjectWithTag("QueenCageTemp").GetComponent<BoxCollider2D>().enabled = false;
                    
                }
            }
        }
        
    }
}

