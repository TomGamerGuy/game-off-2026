using System;
using System.Collections;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    [Header("Asteroids")]
    [SerializeField] GameObject asteroidOne;
    [SerializeField] GameObject asteroidTwo;
    [SerializeField] GameObject asteroidThree;

    [Header("Debris")]
    [SerializeField] GameObject debrisOne;
    [SerializeField] GameObject debrisTwo;
    [SerializeField] GameObject debrisThree;

    [Header("Player")]
    public GameObject player;
    private PlayerScript playerScript;

    [Header("Game Manager")]
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject gameCanvas;
    private GameManagerScript gameManagerScript;
    private GameScript gameScript;

    [Header("Obstacles")]
    [SerializeField] int maxObstacles = 10;
    [NonSerialized] public int obstacleCount = 0;
    public float baseSpeed = 10f;
    [SerializeField] float minWaitTime = 5f;
    [SerializeField] float maxWaitTime = 5f;
    public int obstacleScore = 10;
    private bool isSpawning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
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
                int type = UnityEngine.Random.Range(0, 7);
                GameObject obstacle;
                float x = UnityEngine.Random.Range(10.0f, 15.0f);
                float y = UnityEngine.Random.Range(-5.0f, 5.0f);
                Vector3 position = new Vector3(x, y);

                switch (type)
                {
                    case 1:
                        obstacle = Instantiate(asteroidOne, position, Quaternion.identity);
                    break;
                    case 2:
                        obstacle = Instantiate(asteroidTwo, position, Quaternion.identity);
                    break;
                    case 3:
                        obstacle = Instantiate(asteroidThree, position, Quaternion.identity);
                    break;
                    case 4:
                        obstacle = Instantiate(debrisOne, position, Quaternion.identity);
                    break;
                    case 5:
                        obstacle = Instantiate(debrisTwo, position, Quaternion.identity);
                    break;
                    case 6:
                        obstacle = Instantiate(debrisThree, position, Quaternion.identity);
                    break;
                    default:
                        obstacle = Instantiate(asteroidOne, position, Quaternion.identity);
                    break;
                }
                ObstacleScript obstacleScript = obstacle.GetComponent<ObstacleScript>();
                obstacleScript.Initialize(this, playerScript, gameScript, baseSpeed);

                obstacleCount++;
            }
        }

        isSpawning = false;
    }
}
