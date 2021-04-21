using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovementController controller = other.GetComponent<PlayerMovementController>();

        if (controller != null)
        {
                controller.ChangeHealth(-1);

        }
        Debug.Log("Object that is damaged from entering the trigger : " + other);
       
    }
}

