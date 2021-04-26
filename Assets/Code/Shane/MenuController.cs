using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace test
{
    public class MenuController : MonoBehaviour
    {
        //Outlets
        public static MenuController instance;
        public float delay = 5;
        public string NewLevel = "DungeonPerson";

        public GameObject levelMenu;
        public GameObject mainMenu;

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

        public void Exit()
        {
            SceneManager.LoadScene("Main");

        }


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

