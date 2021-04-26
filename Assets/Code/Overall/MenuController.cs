using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overall
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;
        
        // Outlets
        public GameObject welcomeMenu;
        public GameObject levelMenu;
        
        // State Tracking
        public string level1;
        public string level2;
        public string level3;
        public string level4;

        //Methods
        void Awake()
        {
            instance = this;
            Hide();
            Show();
        }

        public void Show()
        {
            ShowWelcomeMenu();
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        void SwitchMenu(GameObject someMenu)
        {
            //Turn off all menus
            welcomeMenu.SetActive(false);
            levelMenu.SetActive(false);

            //Turn on requested menu
            someMenu.SetActive(true);

        }

        public void ShowWelcomeMenu()
        {
            SwitchMenu(welcomeMenu);
        }
        public void ShowLevelMenu()
        {
            SwitchMenu(levelMenu);
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

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

