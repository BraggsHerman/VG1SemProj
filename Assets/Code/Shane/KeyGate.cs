using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{
    public class KeyGate : MonoBehaviour
    {


        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>() && GameController.instance.keyCount > 0)
            {

                PopUpSystem pop = GameObject.FindGameObjectWithTag("GameController").GetComponent<PopUpSystem>();
                pop.popUpText.text = "It opened!";

                GameObject.FindGameObjectWithTag("FirstDoor").GetComponent<BoxCollider2D>().enabled = false;
                GameObject.FindGameObjectWithTag("FirstDoor").GetComponent<SpriteRenderer>().enabled = false;

            }
        }

    }
}

