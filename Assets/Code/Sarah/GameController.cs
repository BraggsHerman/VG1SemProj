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
        
        // Configuration
        private float startTime;
        private int nextTime;
        private int minutes;
        private int seconds;

        void Awake()
        {
            instance = this;
        }
        
        void Start()
        {
            textTimer.text = "0:00";
            startTime = Time.deltaTime;
            nextTime = 1;
        }
        
        void Update()
        {

            if (Time.deltaTime - startTime > nextTime)
            {
                ++seconds;
                ++nextTime;
                print(nextTime);
                UpdateTime();
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
    }
}

