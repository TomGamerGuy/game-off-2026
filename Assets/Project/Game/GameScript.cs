using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.EventSystems;
using System.Collections;

public class GameScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] GameManagerScript gameManagerScript;
    [SerializeField] LevelSelectScript levelSelectScript;

    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("Menus")]
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] GameObject backCanvas;
    [SerializeField] GameObject countdownCanvas;
    [SerializeField] TextMeshProUGUI countdownText;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    [Header("Game Variables")]
    public float gravityForce;
    public float playerSpeed;
    public int unlockScore = 1000;
    [NonSerialized] public int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        backCanvas.SetActive(false);
        countdownCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameCanvas.activeSelf)
        {
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {                  
                // Activate prompt
                backCanvas.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }

    // If the yes button is clicked on the back menu
    public void BackYes()
    {
        EventSystem.current.SetSelectedGameObject(null);

        backCanvas.SetActive(false);
        Time.timeScale = 1.0f;

        gameCanvas.SetActive(false);
        levelSelectMenu.SetActive(true);
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
        ScoreCheck();

        // TEMP
        Debug.Log("Death");
    }

    // Countdown before starting the level
    public void StartCountdown()
    {
        //Do the countdown
        StartCoroutine(Countdown(3));
    }

    private IEnumerator Countdown(int start)
    {
        Time.timeScale = 0.0f;
        countdownCanvas.SetActive(true);
        
        for (int i = start; i > 0; i--)
        {
            countdownText.text = i.ToString();

            yield return new WaitForSecondsRealtime(1f);
        }

        countdownCanvas.SetActive(false);
        Time.timeScale = 1.0f;

    }
}
