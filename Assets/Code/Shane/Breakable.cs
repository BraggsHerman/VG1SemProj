using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{
    public class Breakable : MonoBehaviour
    {
        public void Break()
        {
            Destroy(gameObject);
            if (gameObject.tag == "SpecialCrate")
            {
                GameObject.FindGameObjectWithTag("Bow").GetComponent<SpriteRenderer>().enabled = true;
            }

        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Projectile>())
            {
                PlayerController.instance.EarnPoints(10);
                Destroy(gameObject);
            }
        }


    }
}

