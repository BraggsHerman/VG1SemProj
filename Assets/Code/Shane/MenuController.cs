using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace test
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;
        public float delay = 5;
        public string NewLevel = "DungeonPerson";

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
            PlayerController.instance.isPaused = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            if (PlayerController.instance != null)
            {
                PlayerController.instance.isPaused = false;
            }
        }

        void SwitchMenu(GameObject someMenu)
        {
            //Turn off all menus
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            levelMenu.SetActive(false);

            //Turn on requested menu
            someMenu.SetActive(true);

        }

        public void ShowMainMenu()
        {
            SwitchMenu(mainMenu);
        }
        public void ShowOptionsMenu()
        {
            SwitchMenu(optionsMenu);
        }
        public void ShowLevelMenu()
        {
            SwitchMenu(levelMenu);
        }

        public void ResetScore()
        {
            PlayerPrefs.DeleteKey("Score");
            PlayerController.instance.score = 0;
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene("DungeonPerson");
        }
        //Outlets
        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject levelMenu;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(LoadLevelAfterDelay(delay));
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public IEnumerator LoadLevelAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            SceneManager.LoadScene(NewLevel);
        }
    }
}

