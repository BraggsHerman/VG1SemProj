using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sarah
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;
        
        // Outlets
        public GameObject mainMenu;
        public GameObject levelMenu;

        // Configuration
        public float delay = 6;
        public string nextLevel = "DungeonPerson";
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
            GameController.instance.PauseGame();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            GameController.instance.ResumeGame();
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
            SceneManager.LoadScene(level1);
        }

        public void ResetScore()
        {
            PlayerPrefs.DeleteKey("Level 1 High Score");
            PlayerController.instance.bottleScore = 0;
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(nextLevel);
        }

        public IEnumerator LoadLevelAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            SceneManager.LoadScene(nextLevel);
        }
        
        public void LoadLevel2()
        {
            SceneManager.LoadScene(level2);
        }
        
        public void LoadLevel3()
        {
            SceneManager.LoadScene(level3);
        }
        
        public void LoadLevel4()
        {
            SceneManager.LoadScene(level4);
        }
    }
}

