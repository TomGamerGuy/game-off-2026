using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private float speed;
    private PlayerScript playerScript;
    private GameScript gameScript;
    private ObstacleSpawnerScript obstacleSpawner;
    private bool destroyed = false;
    private GameObject explosion;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //
    }

    public void Initialize(ObstacleSpawnerScript spawner, PlayerScript player, GameScript game, float baseSpeed, GameObject explosionEffect)
    {
        obstacleSpawner = spawner;
        playerScript = player;
        gameScript = game;
        speed = baseSpeed;
        destroyed = false;
        explosion = explosionEffect;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -15f)
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
            Explode(collision);
        }
        if (!collision.gameObject.CompareTag("Border"))
        {
            obstacleSpawner.obstacleCount--;
            Destroy(gameObject);
        }
    }

    // Play explode effect and destroy
    void Explode(Collision2D collision)
    {
        if (destroyed)
        {
            return;
        }
        
        destroyed = true;

        Vector2 contactPoint = collision.GetContact(0).point;
        Instantiate(explosion, contactPoint, Quaternion.identity);
    }
}
