using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shane
{
    public class MazeObstacle2 : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            // Reload scene only when colliding with player
            if (other.gameObject.GetComponent<PlayerController2>())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}

