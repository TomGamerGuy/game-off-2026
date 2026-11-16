using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] GameObject gameManager;
    private GameScript gameScript;

    [Header("Sprites")]
    [SerializeField] Sprite shieldSprite;
    [SerializeField] Sprite baseSprite;
    [SerializeField] Sprite brokenSprite;
    [SerializeField] GameObject shieldFlame;
    [SerializeField] GameObject baseFlame;
    [SerializeField] GameObject brokenFlame;

    [Header("Audio")]
    [SerializeField] AudioClip collisionSound;
    private AudioSource audioSource;

    [Header("Components")]
    public Rigidbody2D rb;

    // Public
    public Vector2 spawnPosition;

    //Private
    private int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameScript = gameManager.GetComponent<GameScript>();
        rb.freezeRotation = true;
        audioSource = GetComponent<AudioSource>();
        shieldFlame.SetActive(true);
        baseFlame.SetActive(false);
        brokenFlame.SetActive(false);
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
        if (!collision.gameObject.CompareTag("Border"))
        {
            audioSource.clip = collisionSound;
            audioSource.Play();
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
                shieldFlame.SetActive(true);
                baseFlame.SetActive(false);
                brokenFlame.SetActive(false);
            break;
            case 2:
                playerSprite.sprite = baseSprite;
                shieldFlame.SetActive(false);
                baseFlame.SetActive(true);
                brokenFlame.SetActive(false);
            break;
            case 1:
                playerSprite.sprite = brokenSprite;
                shieldFlame.SetActive(false);
                baseFlame.SetActive(false);
                brokenFlame.SetActive(true);
            break;
            default:
                playerSprite.sprite = brokenSprite;
                shieldFlame.SetActive(false);
                baseFlame.SetActive(false);
                brokenFlame.SetActive(true);
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
