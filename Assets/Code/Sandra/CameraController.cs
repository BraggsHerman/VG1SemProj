using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //OUTLET
    //tell camera who to follow
    public Transform target;

    //Configuration
    //where should camera be relative to player
    public Vector3 offset;
    public float smoothness;

    //State tracking 
    //to keetp track of velocity, for smoothness
    Vector3 _velocity;


    // Methods
    //maintain a certain distance for the rest of the game
    void Start()
    {
        if (target)
        {
            offset = transform.position - target.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position, target.position + offset, ref _velocity, smoothness);
        }
    }
}

