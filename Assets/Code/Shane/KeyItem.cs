using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{
    public class KeyItem : MonoBehaviour
    {
        
        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                GameController.instance.keyCount += 1;
                Destroy(gameObject);
            }
        }
    }
}

