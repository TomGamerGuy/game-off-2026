using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] GameObject gameManager;
    private GameScript gameScript;

    [Header("Collision")]
    [SerializeField] GameObject bottomBorder;

    [Header("Sprites")]
    [SerializeField] Sprite shieldSprite;
    [SerializeField] Sprite baseSprite;
    [SerializeField] Sprite brokenSprite;

    // Public
    [NonSerialized] public Rigidbody2D rb;
    [NonSerialized] public Vector2 spawnPosition;

    //Private
    private int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameScript = gameManager.GetComponent<GameScript>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        spawnPosition = rb.position;
    }

    void FixedUpdate()
    {
        float targetVerticalVelocity = -gameScript.gravityForce;

        if (Keyboard.current.shiftKey.isPressed)
        {
            targetVerticalVelocity = gameScript.playerSpeed;
        }

        rb.linearVelocity = new Vector2(rb.linearVelocityX, targetVerticalVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == bottomBorder)
        {
            gameScript.GameOver();
        }
    }

    // Load the correct sprites for health
    void LoadHealth()
    {
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        switch (health)
        {
            case 3:
                playerSprite.sprite = shieldSprite;
            break;
            case 2:
                playerSprite.sprite = baseSprite;
            break;
            case 1:
                playerSprite.sprite = brokenSprite;
            break;
            default:
                playerSprite.sprite = brokenSprite;
            break;
        }
    }

    // Set the players health
    public void SetHealth(int h = 3)
    {
        health = h;
        LoadHealth();
    }

    // Damage the player
    public void Damage()
    {
        health--;
        if (health <= 0)
        {
            gameScript.GameOver();
        }
        else
        {
            LoadHealth();
        }
    }
}
