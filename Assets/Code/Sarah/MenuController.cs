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

        public void ResetScore()
        {
            PlayerPrefs.DeleteKey("Bottle Score");
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
    }
}

