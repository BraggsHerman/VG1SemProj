using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sarah
{
    public class Chaser : MonoBehaviour
    {
        // Outlets
        private Rigidbody2D rigidbody;
        
        
        // Configuration
        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        };

        // State Tracking
        private Direction direction;
        private bool playerInSightRange;
        private bool player;
        
        //Methods
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            direction = Direction.Right;
        }
        
        void Update()
        {
            
        }
    }
}

