using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.EventSystems;
using System.Collections;

public class GameScript : MonoBehaviour
{
    [Header("Playtesting")]
    [SerializeField] bool canDie = true;
    
    [Header("Scripts")]
    [SerializeField] GameManagerScript gameManagerScript;
    [SerializeField] LevelSelectScript levelSelectScript;

    [Header("Player")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject explosionEffect;

    [Header("Menus")]
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] GameObject backCanvas;
    [SerializeField] GameObject countdownCanvas;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI unlockText;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject scoreCanvas;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    [Header("Game Variables")]
    public float gravityForce;
    public float playerSpeed;
    public int unlockScore = 1000;
    [NonSerialized] public int score;

    [Header("Level Variables")]
    public float planetGravity = 2f;
    public float starGravity = 4f;
    public float blackHoleGravity = 6f;
    public int planetObstacles = 8;
    public int starObstacles = 10;
    public int blackHoleObstacles = 12;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        backCanvas.SetActive(false);
        countdownCanvas.SetActive(false);
        player.SetActive(false);
        gameOverCanvas.SetActive(false);
        scoreCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoreCanvas.SetActive(gameCanvas.activeSelf);
        
        if (gameCanvas.activeSelf && !gameOverCanvas.activeSelf)
        {
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !countdownCanvas.activeSelf)
            {                  
                // Activate prompt
                backCanvas.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }

    // Restart the level
    public void RestartButton()
    {
        EventSystem.current.SetSelectedGameObject(null);

        gameOverCanvas.SetActive(false);
        levelSelectScript.LoadLevel(gameManagerScript.levelData.currentLevel);
    }

    // If the yes button is clicked on the back menu
    public void BackYes()
    {
        EventSystem.current.SetSelectedGameObject(null);

        backCanvas.SetActive(false);
        Time.timeScale = 1.0f;

        gameCanvas.SetActive(false);
        levelSelectMenu.SetActive(true);
        player.gameObject.SetActive(false);
    }

    // If the no button is clicked on the back menu
    public void BackNo()
    {
        EventSystem.current.SetSelectedGameObject(null);

        backCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // Add the parameter to the correct score and update
    public void UpdateScore(int add, int replace = -1)
    {      
        if (replace >= 0)
        {
            score = replace;
        }
        else
        {
            score += add;
        }

        scoreText.text = "Score: " + score;
        ScoreCheck();
    }

    public void ScoreCheck()
    {
        switch (gameManagerScript.levelData.currentLevel)
        {
            case GameManagerScript.Level.Level1:
                if (gameManagerScript.levelData.level1Score < score)
                {
                    gameManagerScript.levelData.level1Score = score;
                }
                highScoreText.text = "Highscore: " + gameManagerScript.levelData.level1Score;
            break;
            case GameManagerScript.Level.Level2:
                if (gameManagerScript.levelData.level2Score < score)
                {
                    gameManagerScript.levelData.level2Score = score;
                }
                highScoreText.text = "Highscore: " + gameManagerScript.levelData.level2Score;
            break;
            case GameManagerScript.Level.Level3:
                if (gameManagerScript.levelData.level3Score < score)
                {
                    gameManagerScript.levelData.level3Score = score;
                }
                highScoreText.text = "Highscore: " + gameManagerScript.levelData.level3Score;
            break;
            default:
                //
            break;
        }
        levelSelectScript.LevelButtonCheck();
    }

    // Run when the game ends
    public void GameOver(){
        if (!canDie)
        {
            return;
        }
        
        ScoreCheck();

        Instantiate(explosionEffect, player.transform.position, Quaternion.identity);

        Time.timeScale = 0.0f;
        player.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

    // Countdown before starting the level
    public void StartCountdown()
    {
        gameOverCanvas.SetActive(false);
        
        //Do the countdown
        StartCoroutine(Countdown(3));
    }

    private IEnumerator Countdown(int start)
    {
        Time.timeScale = 0.0f;
        countdownCanvas.SetActive(true);

        switch (gameManagerScript.levelData.currentLevel)
        {
            case GameManagerScript.Level.Level1:
                if (gameManagerScript.levelData.level2Unlocked)
                {
                    unlockText.text = "Level 2 already unlocked";
                }
                else
                {
                    unlockText.text = unlockScore + " to unlock level 2";
                }
            break;
            case GameManagerScript.Level.Level2:
                if (gameManagerScript.levelData.level3Unlocked)
                {
                    unlockText.text = "Level 3 already unlocked";
                }
                else
                {
                    unlockText.text = unlockScore + " to unlock level 3";
                }
            break;
            case GameManagerScript.Level.Level3:
                if (gameManagerScript.levelData.badgeUnlocked)
                {
                    unlockText.text = "Badge already unlocked";
                }
                else
                {
                    unlockText.text = unlockScore + " to unlock badge";
                }
            break;
            default:
                //
            break;
        }
        
        for (int i = start; i > 0; i--)
        {
            countdownText.text = i.ToString();

            yield return new WaitForSecondsRealtime(1f);
        }

        countdownCanvas.SetActive(false);
        Time.timeScale = 1.0f;

    }
}
