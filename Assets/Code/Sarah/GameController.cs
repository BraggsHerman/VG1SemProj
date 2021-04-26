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
        public PlayerController _PlayerController;

        // Configuration
        private float lastTime;
        private int nextTime;
        private int minutes;
        private int seconds;
        public int totalSecs;
        private float pauseTime;
        public int endSceneDelay;
        
        // State Tracking
        private bool gameOver;
        public bool isPaused;
        public string timeString;

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
            totalSecs = 0;
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
                ++totalSecs;
                ++nextTime;
                print(nextTime);
                UpdateTime();
            }
            
            if (gameOver)
            {
                GameOverScript.instance.Show();
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

            timeString = minutes + ":" + secStr;
            textTimer.text = timeString;
        }

        public void PauseGame()
        {
            pauseTime = lastTime + Time.deltaTime;
            isPaused = true;
            _PlayerController.isPaused = true;
            foreach (Chaser chaser in chasers)
            {
                chaser.isPaused = true;
            }
        }

        public void ResumeGame()
        {
            lastTime = pauseTime;
            isPaused = false;
            _PlayerController.isPaused = false;
            foreach (Chaser chaser in chasers)
            {
                chaser.isPaused = false;
            }
        }
    }
}

