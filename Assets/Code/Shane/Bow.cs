using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{
    public class Bow : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                GameController.instance.hasBow = true;
                GameController.instance.keyCount = 0;
                Destroy(GameObject.FindGameObjectWithTag("BossDoor"));
                Destroy(gameObject);
            }
        }

    }
}

