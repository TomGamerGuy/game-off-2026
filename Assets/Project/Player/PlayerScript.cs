using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] GameObject gameManager;
    private GameScript gameScript;

    // Public
    [NonSerialized] public Rigidbody2D rb;
    [NonSerialized] public Vector2 spawnPosition;

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
}
