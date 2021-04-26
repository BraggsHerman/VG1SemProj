using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sarah
{
    public class GameController : MonoBehaviour
    {
        
        public static GameController instance;
        
        // Outlets
        public Text textTimer;
        public Chaser[] chasers;

        // Configuration
        private float lastTime;
        private int nextTime;
        private int minutes;
        private int seconds;
        private float pauseTime;
        public int endSceneDelay;
        
        // State Tracking
        private bool gameOver;
        public bool isPaused;

        void Awake()
        {
            instance = this;
        }
        
        void Start()
        {
            textTimer.text = "0:00";
            lastTime = 0;
            nextTime = 1;
            isPaused = false;
        }
        
        void Update()
        {
            if (isPaused)
            {
                return;
            }
            
            lastTime += Time.deltaTime;
            if (lastTime > nextTime)
            {
                ++seconds;
                ++nextTime;
                print(nextTime);
                UpdateTime();
            }
            
            if (gameOver)
            {
                StartCoroutine(MenuController.instance.LoadLevelAfterDelay(endSceneDelay));
                print("Resetting level...");
            }
        }

        void UpdateTime()
        {
            if (seconds >= 60)
            {
                seconds -= 60;
                minutes += 1;
            }

            string secStr = seconds.ToString();
            if (seconds < 10)
            {
                secStr = "0" + secStr;
            }

            textTimer.text = minutes + ":" + secStr;
        }

        public void PauseGame()
        {
            pauseTime = lastTime + Time.deltaTime;
            isPaused = true;
            PlayerController.instance.isPaused = true;
            foreach (Chaser chaser in chasers)
            {
                chaser.isPaused = true;
            }
        }

        public void ResumeGame()
        {
            lastTime = pauseTime;
            isPaused = false;
            PlayerController.instance.isPaused = false;
            foreach (Chaser chaser in chasers)
            {
                chaser.isPaused = false;
            }
        }
    }
}

