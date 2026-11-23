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
    [SerializeField] float goldTextDuration = 2f;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip interactSound;
    [SerializeField] AudioClip countdownSound;
    public AudioSource audioSource2;
    [SerializeField] AudioSource audioSource3;
    [SerializeField] AudioClip unlockSound;

    [Header("Game Variables")]
    public float gravityForce;
    public float playerSpeed;
    [NonSerialized] public int unlockScore;
    [NonSerialized] public int score;

    [SerializeField] int countdownStart = 3;
    [NonSerialized] public bool unlockSoundPlayed = false;

    [Header("Level Variables")]
    [Header("Gravity")]
    public float planetGravity = 2f;
    public float starGravity = 4f;
    public float neutronStarGravity = 6f;
    public float blackHoleGravity = 6f;

    [Header("Obstacles")]
    public int planetObstacles = 8;
    public int starObstacles = 10;
    public int neutronStarObstacles = 12;
    public int blackHoleObstacles = 12;

    [Header("Speed")]
    public float planetSpeed = 10f;
    public float starSpeed = 10f;
    public float neutronStarSpeed = 10f;
    public float blackHoleSpeed = 10f;

    [Header("Score")]
    public int planetScore = 10;
    public int starScore = 10;
    public int neutronStarScore = 5;
    public int blackHoleScore = 5;

    [Header("Unlock Score")]
    public int starUnlockScore = 500;
    public int neutronStarUnlockScore = 1000;
    public int blackHoleUnlockScore = 1500;
    public int badgeUnlockScore = 2000;

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
        StartCoroutine(Countdown(countdownStart));
    }

    void UnlockCheck()
    {
        if (gameManagerScript.levelData.level1Score >= unlockScore && !gameManagerScript.levelData.level2Unlocked)
        {
            gameManagerScript.levelData.level2Unlocked = true;
            UnlockSound();
        }
        if (gameManagerScript.levelData.level2Score >= unlockScore && !gameManagerScript.levelData.level4Unlocked)
        {
            gameManagerScript.levelData.level4Unlocked = true;
            UnlockSound();
        }
        if (gameManagerScript.levelData.level4Score >= unlockScore && !gameManagerScript.levelData.level3Unlocked)
        {
            gameManagerScript.levelData.level3Unlocked = true;
            UnlockSound();
        }
        if (gameManagerScript.levelData.level3Score >= unlockScore && !gameManagerScript.levelData.badgeUnlocked)
        {
            gameManagerScript.levelData.badgeUnlocked = true;
            UnlockSound();
        }
    }

    // Play the unlock sound
    void UnlockSound()
    {
        if (unlockSoundPlayed)
        {
            return;
        }
        
        unlockSoundPlayed = true;

        audioSource3.clip = unlockSound;
        audioSource3.Play();

        StartCoroutine(UnlockRoutine(goldTextDuration));
    }

    private IEnumerator UnlockRoutine(float seconds)
    {
        highScoreText.color = Color.gold;
        scoreText.color = Color.gold;
    
        yield return new WaitForSeconds(seconds);

        highScoreText.color = Color.white;
        scoreText.color = Color.white;
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
        UnlockCheck();
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
