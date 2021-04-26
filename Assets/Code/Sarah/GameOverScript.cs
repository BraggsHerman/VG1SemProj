using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sarah
{
    public class GameOverScript : MonoBehaviour
    {
        public static GameOverScript instance;

        // Outlets
        public Text scoreText;
        public Text timeText;
        public Text highScoreText;
        public Text bestTimeText;
        private AudioSource _audioSource;
        public AudioClip babyCrySound;
        
        // Configuration
        public string nextLevel;
        public string welcomeScene;

        //Methods
        void Awake()
        {
            instance = this;
            _audioSource = GetComponent<AudioSource>();
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _audioSource.PlayOneShot(babyCrySound);
            Time.timeScale = 0;
            GameController.instance.PauseGame();
            UpdateStats();
            ShowScore();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            GameController.instance.ResumeGame();
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadNextLevel()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(nextLevel);
        }

        public void LoadWelcomeScene()
        {
            SceneManager.LoadScene(welcomeScene);
        }

        void UpdateStats()
        {
            int bestTime = PlayerPrefs.GetInt("Level 1 Best Time Secs");
            int myTime = GameController.instance.totalSecs;
            if (bestTime == 0)
            {
                PlayerPrefs.SetInt("Level 1 Best Time Secs", myTime);
                PlayerPrefs.SetString("Level 1 Best Time", GameController.instance.timeString);
            }
            if (bestTime < myTime)
            {
                PlayerPrefs.SetInt("Level 1 Best Time Secs", myTime);
                PlayerPrefs.SetString("Level 1 Best Time", GameController.instance.timeString);
            }
            int highScore = PlayerPrefs.GetInt("Level 1 High Score");
            int score = PlayerController.instance.bottleScore;
            if (score > highScore)
            {
                PlayerPrefs.SetInt("Level 1 High Score", score);
            }
        }
        
        void ShowScore()
        {
            scoreText.text = PlayerController.instance.bottleScore.ToString();
            timeText.text = GameController.instance.timeString;
            highScoreText.text = PlayerPrefs.GetInt("Level 1 High Score").ToString();
            bestTimeText.text = PlayerPrefs.GetString("Level 1 Best Time");
        }
    }
}

