using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        public float timeElapsed;
        public int keyCount;
        public bool hasBow;
        public bool beatBoss;
        public bool askedForIt;
        public bool youLose;
        public bool youWin;

        public GameObject explosionPrefab;

        //Methods
        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //Increment passage of time for each frame of the game
            timeElapsed += Time.deltaTime;

            if (GameController.instance.youLose)
            {
                StartCoroutine(MenuController.instance.LoadLevelAfterDelay(5));
                //print("Resetting level...");
                MenuController.instance.NewLevel = "DungeonPerson";
            }
            if (GameController.instance.youWin)
            {
                StartCoroutine(MenuController.instance.LoadLevelAfterDelay(5));
                print("Resetting level...");
                MenuController.instance.NewLevel = "Main";
            }
        }
    }

}
