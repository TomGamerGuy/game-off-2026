using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    [Header("Asteroids")]
    [SerializeField] GameObject asteroidOne;
    [SerializeField] GameObject asteroidTwo;
    [SerializeField] GameObject asteroidThree;
    [SerializeField] GameObject asteroidFour;

    [Header("Debris")]
    [SerializeField] GameObject debrisOne;
    [SerializeField] GameObject debrisTwo;
    [SerializeField] GameObject debrisThree;
    [SerializeField] GameObject debrisFour;

    [Header("Player")]
    public GameObject player;
    private PlayerScript playerScript;

    [Header("Game Manager")]
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject gameCanvas;
    private GameScript gameScript;

    [Header("Obstacles")]
    [NonSerialized] public int maxObstacles = 10;
    [NonSerialized] public int obstacleCount = 0;
    [NonSerialized] public float baseSpeed = 10f;
    [SerializeField] float minWaitTime = 5f;
    [SerializeField] float maxWaitTime = 5f;
    public int obstacleScore = 10;
    [SerializeField] GameObject explosionEffect;
    private bool isSpawning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameScript = gameManager.GetComponent<GameScript>();
        playerScript = player.GetComponent<PlayerScript>();

        obstacleCount = 0;
        isSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameCanvas.activeSelf && obstacleCount < maxObstacles && !isSpawning)
        {
            StartCoroutine(SpawnObstacle());
        }
    }

    private IEnumerator SpawnObstacle()
    {
        isSpawning = true;

        while (gameCanvas.activeSelf && obstacleCount < maxObstacles)
        {
            float waitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            if (gameCanvas.activeSelf && obstacleCount < maxObstacles)
            {
                // Spawn Obstacle
                int type = UnityEngine.Random.Range(1, 9);
                if (type == 6 && UnityEngine.Random.Range(1, 5) != 3)
                {
                    type = UnityEngine.Random.Range(1, 9);
                }
                GameObject obstacle;
                float x = UnityEngine.Random.Range(15.0f, 20.0f);
                float y = UnityEngine.Random.Range(-5.0f, 5.0f);
                Vector3 position = new Vector3(x, y, 0f);

                Quaternion rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));

                switch (type)
                {
                    case 1:
                        obstacle = Instantiate(asteroidOne, position, rotation);
                    break;
                    case 2:
                        obstacle = Instantiate(asteroidTwo, position, rotation);
                    break;
                    case 3:
                        obstacle = Instantiate(asteroidThree, position, rotation);
                    break;
                    case 4:
                        obstacle = Instantiate(debrisOne, position, rotation);
                    break;
                    case 5:
                        obstacle = Instantiate(debrisTwo, position, rotation);
                    break;
                    case 6:
                        obstacle = Instantiate(debrisThree, position, rotation);
                    break;
                    case 7:
                        obstacle = Instantiate(asteroidFour, position, rotation);
                    break;
                    case 8:
                        obstacle = Instantiate(debrisFour, position, rotation);
                    break;
                    default:
                        obstacle = Instantiate(asteroidOne, position, rotation);
                    break;
                }
                ObstacleScript obstacleScript = obstacle.GetComponent<ObstacleScript>();
                obstacleScript.Initialize(this, playerScript, gameScript, baseSpeed, explosionEffect);

                obstacleCount++;
                
                // TEMP
                Debug.Log("Obstacle Count: " + obstacleCount.ToString());
            }
        }

        isSpawning = false;
    }
}
