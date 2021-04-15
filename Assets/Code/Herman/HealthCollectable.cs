using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovementController controller = other.GetComponent<PlayerMovementController>();

        if(controller != null)
        {
            if(controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(this.gameObject);
            }

        }
        Debug.Log("Object that entered the trigger : " + other);
    }
}
