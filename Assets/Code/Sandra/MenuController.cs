using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HighSchool
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;
        
        // Outlets
        public GameObject mainMenu;
        public GameObject levelMenu;
        public ControlPlayer player;

        // Configuration
        public string level1;
        public string level2;
        public string level3;
        public string level4;
        
        //Methods
        void Awake()
        {
            instance = this;
            Hide();
        }

        public void Show()
        {
            ShowMainMenu();
            gameObject.SetActive(true);
            Time.timeScale = 0;
            player.isPaused = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            player.isPaused = false;
        }

        void SwitchMenu(GameObject someMenu)
        {
            //Turn off all menus
            mainMenu.SetActive(false);
            levelMenu.SetActive(false);

            //Turn on requested menu
            someMenu.SetActive(true);
        }

        public void ShowMainMenu()
        {
            SwitchMenu(mainMenu);
        }
        
        public void ShowLevelMenu()
        {
            SwitchMenu(levelMenu);
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadLevel1()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(level1);
        }

        public void LoadLevel2()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(level2);
        }
        
        public void LoadLevel3()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(level3);
        }
        
        public void LoadLevel4()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(level4);
        }
    }
}

