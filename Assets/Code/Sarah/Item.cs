using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sarah
{
    public class Item : MonoBehaviour
    {
        public float regenAmount;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Destroy(gameObject);
                PlayerController player;
                if (player = other.gameObject.GetComponent<PlayerController>())
                {
                    player.itemAvailable = false;
                    player.RegenHealth(regenAmount);
                }
            }
        }
    }
}

