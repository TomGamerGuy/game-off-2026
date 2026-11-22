using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

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
    [SerializeField] Button nextButton;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip interactSound;
    [SerializeField] AudioClip countdownSound;
    public AudioSource audioSource2;

    [Header("Game Variables")]
    public float gravityForce;
    public float playerSpeed;
    public int unlockScore = 1000;
    [NonSerialized] public int score;

    [SerializeField] int countdownStart = 3;

    [Header("Level Variables")]
    public float planetGravity = 2f;
    public float starGravity = 4f;
    public float neutronStarGravity = 6f;
    public float blackHoleGravity = 6f;
    public int planetObstacles = 8;
    public int starObstacles = 10;
    public int neutronStarObstacles = 12;
    public int blackHoleObstacles = 12;
    public float planetSpeed = 10f;
    public float starSpeed = 10f;
    public float neutronStarSpeed = 10f;
    public float blackHoleSpeed = 10f;

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
                audioSource2.clip = interactSound;
                audioSource2.Play();

                backCanvas.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }

    // Restart the level
    public void RestartButton()
    {
        audioSource2.clip = interactSound;
        audioSource2.Play();
        
        EventSystem.current.SetSelectedGameObject(null);

        gameOverCanvas.SetActive(false);
        levelSelectScript.LoadLevel(gameManagerScript.levelData.currentLevel);
    }

    // If the yes button is clicked on the back menu
    public void BackYes()
    {
        audioSource2.clip = interactSound;
        audioSource2.Play();
        
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
        audioSource2.clip = interactSound;
        audioSource2.Play();
        
        EventSystem.current.SetSelectedGameObject(null);

        backCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // Add the parameter to the correct score and update
    public void UpdateScore(int add, int replace = -1)
    {      
        if (!gameCanvas.activeSelf)
        {
            return;
        }
        
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
            case GameManagerScript.Level.Level4:
                if (gameManagerScript.levelData.level4Score < score)
                {
                    gameManagerScript.levelData.level4Score = score;
                }
                highScoreText.text = "Highscore: " + gameManagerScript.levelData.level4Score;
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
        
        gameManagerScript.levelData.deathCount++;
        ScoreCheck();
        NextCheck();

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
        StartCoroutine(Countdown(countdownStart));
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
                    unlockText.text = "Star already unlocked";
                }
                else
                {
                    unlockText.text = unlockScore + " to unlock Star";
                }
            break;
            case GameManagerScript.Level.Level2:
                if (gameManagerScript.levelData.level4Unlocked)
                {
                    unlockText.text = "Neutron Star already unlocked";
                }
                else
                {
                    unlockText.text = unlockScore + " to unlock Neutron Star";
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
            case GameManagerScript.Level.Level4:
                if (gameManagerScript.levelData.level3Unlocked)
                {
                    unlockText.text = "Black Hole already unlocked";
                }
                else
                {
                    unlockText.text = unlockScore + " to unlock Black Hole";
                }
            break;
            default:
                //
            break;
        }
        
        for (int i = start; i > 0; i--)
        {
            countdownText.text = i.ToString();

            audioSource2.clip = countdownSound;
            audioSource2.Play();

            yield return new WaitForSecondsRealtime(1f);
        }

        countdownCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // Load next level is available
    public void NextButton()
    {
        switch (gameManagerScript.levelData.currentLevel)
        {
            case GameManagerScript.Level.Level1:
                levelSelectScript.Level2Button();
            break;
            case GameManagerScript.Level.Level2:
                levelSelectScript.Level4Button();
            break;
            case GameManagerScript.Level.Level4:
                levelSelectScript.Level3Button();
            break;
            default:
                //
            break;
        }
    }

    // Check if next should be active
    void NextCheck()
    {
        switch (gameManagerScript.levelData.currentLevel)
        {
            case GameManagerScript.Level.Level1:
                if (gameManagerScript.levelData.level2Unlocked)
                {
                    nextButton.interactable = true;
                }
                else
                {
                    nextButton.interactable = false;
                }
            break;
            case GameManagerScript.Level.Level2:
                if (gameManagerScript.levelData.level4Unlocked)
                {
                    nextButton.interactable = true;
                }
                else
                {
                    nextButton.interactable = false;
                }
            break;
            case GameManagerScript.Level.Level4:
                if (gameManagerScript.levelData.level3Unlocked)
                {
                    nextButton.interactable = true;
                }
                else
                {
                    nextButton.interactable = false;
                }
            break;
            case GameManagerScript.Level.Level3:
                nextButton.interactable = false;
            break;
        }
    }
}
