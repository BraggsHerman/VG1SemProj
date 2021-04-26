using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace test {
    public class EnemyController : MonoBehaviour
    {

        public static EnemyController instance;

        public bool horizontal;

        public float health = 20f;
        public float healthMax = 20f;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (horizontal)
            {
                transform.position = new Vector2(Mathf.Sin(GameController.instance.timeElapsed) * 6f, transform.position.y);
            }
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                Die();
                PlayerController.instance.EarnPoints(25);
            }
            
        }

        void Die()
        {
            Destroy(gameObject);
        }

        


    }
}

