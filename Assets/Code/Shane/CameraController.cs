using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shane
{
    public class CameraController : MonoBehaviour
    {
        //Outlet
        public Transform target;

        //Config
        public Vector3 offset;
        public float smoothness;

        Vector3 _velocity;
        // Start is called before the first frame update
        void Start()
        {
            if (target)
            {
                offset = transform.position - target.position;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    target.position + offset,
                    ref _velocity,
                    smoothness
                );
            }
        }
    }
}
