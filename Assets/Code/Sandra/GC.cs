using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HighSchool { 
public class GC : MonoBehaviour
{
        //game controller class- empty
        public static  GC instance;
        //OUTLETS--> need thm to talk to a few things
        //need an array of transform
        public Transform[] spawnPoints;
        public Transform[] healthPoints;
        //spawnign random asteroids at the spawnpoints
        public GameObject[] obstaclePrefabs;
        public GameObject[] healthPrefabs;
        public Text textScore;
        /*
        public GameObject explosionPrefab;
        
        public Text textMoney;
        public Text missleSpeedUpgradeText;
        public Text bonusUpgradeText;

        */
        //configuration of asteroid timing, go faster as game progreses
        //over course of game, go from 2 seconds to 0.2 seconds 
        public float maxObstacleDelay = 0.6f;
        public float minObstacleDelay = 0.2f;
        public float maxHealthDelay = 2f;
        public float minHealthDelay = 0.2f;

        //State trackign variable
        public float timeElapsed;
        //how long till we see another asteroid
        public float obstacleDelay;
        public float healthDelay;
        public int score;
        /*
 
        public int money;
        public float missleSpeed = 2f;
        public float bonusMultiplier = 1f;
        */

        
        void Awake()
        {
            instance = this;
        }
        
        void Start()
        {
           StartCoroutine("ObstacleSpawnTimer");
           score = 0;
            StartCoroutine("HealthSpawnTimer");
        }


        
        void Update()
        {
            //increment the passage of time for each frame of the game
          timeElapsed += Time.deltaTime;

            //how much harder does game get 
            //computer asteroid delay
            float decreaseDelayOverTime = maxObstacleDelay - ((maxObstacleDelay - minObstacleDelay) / 30f * timeElapsed);
            float decreaseHealthDelayOverTime = maxHealthDelay - ((maxHealthDelay - minHealthDelay) / 30f * timeElapsed);
            //never goes above max or below min
            obstacleDelay = Mathf.Clamp(decreaseDelayOverTime, minObstacleDelay, maxObstacleDelay);
            healthDelay = Mathf.Clamp(decreaseDelayOverTime, minHealthDelay, maxHealthDelay);
            UpdateDisplay();
        }

        
        void UpdateDisplay()
        {
            textScore.text = score.ToString();
           
        }
        
        public void EarnPoints(int pointAmount)
        {
            score += pointAmount;
           
        }
        

        void SpawnObstacle()
        {
            //pick random spawn points and prefabs
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
          
            GameObject randomObstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
           

            //Spawn, asteroid from spawn poitn
            Instantiate(randomObstaclePrefab, randomSpawnPoint.position, Quaternion.identity);
        }

        void SpawnHealth()
        {
            //pick random spawn points and prefabs
           
            Transform randomHealthPoint = healthPoints[Random.Range(0, healthPoints.Length)];
           
            GameObject randomHealthPrefab = healthPrefabs[Random.Range(0, healthPrefabs.Length)];

            //Spawn, asteroid from spawn poitn
            Instantiate(randomHealthPrefab, randomHealthPoint.position, Quaternion.identity);
        }


        IEnumerator ObstacleSpawnTimer()
        {
            //wait
            yield return new WaitForSeconds(obstacleDelay);
            //after this time, can spawn an asteroid
            SpawnObstacle();
            //restart the timer
            StartCoroutine("ObstacleSpawnTimer");
        }

        IEnumerator HealthSpawnTimer()
        {
            //wait
            yield return new WaitForSeconds(healthDelay);
            //after this time, can spawn an asteroid
            SpawnHealth();
            //restart the timer
            StartCoroutine("HealthSpawnTimer");
        }
        /*
        public void UpgradeMissleSpeed()
        {
            int cost = Mathf.RoundToInt(25 * missleSpeed);
            if (cost <= money)
            {
                money -= cost;
                missleSpeed += 1f;
                missleSpeedUpgradeText.text = "Missile Speed $" + Mathf.RoundToInt(25 * missleSpeed);
            }
        }

        public void UpgradeBonus()
        {
            int cost = Mathf.RoundToInt(100 * bonusMultiplier);

            if (cost <= money)
            {
                money -= cost;
                bonusMultiplier += 1f;
                bonusUpgradeText.text = "Multiplier $" + Mathf.RoundToInt(100 * bonusMultiplier);
            }

        }
 
        */
    }
}