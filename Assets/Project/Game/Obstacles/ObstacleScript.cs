using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private float speed;
    private PlayerScript playerScript;
    private GameScript gameScript;
    private ObstacleSpawnerScript obstacleSpawner;
    private bool destroyed = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //
    }

    public void Initialize(ObstacleSpawnerScript spawner, PlayerScript player, GameScript game, float baseSpeed)
    {
        obstacleSpawner = spawner;
        playerScript = player;
        gameScript = game;
        speed = baseSpeed;
        destroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < -15f)
        {
            obstacleSpawner.obstacleCount--;
            gameScript.UpdateScore(obstacleSpawner.obstacleScore);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.Damage();
        }
        Explode();
    }

    // Play explode effect and destroy
    void Explode()
    {
        if (destroyed)
        {
            return;
        }
        
        destroyed = true;

        // Explosion

        obstacleSpawner.obstacleCount--;
        Destroy(gameObject);
    }
}
