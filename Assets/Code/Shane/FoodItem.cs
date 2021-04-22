using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace test
{
    public class FoodItem : MonoBehaviour
    {
        
        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                PlayerController.instance.health = PlayerController.instance.healthMax;
                PlayerController.instance.imageHealthBar.fillAmount = PlayerController.instance.health / PlayerController.instance.healthMax;
                Destroy(gameObject);
            }
        }
    }
}

