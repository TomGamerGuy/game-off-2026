using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

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

    [Header("Game Variables")]
    public float gravityForce;
    public float playerSpeed;
    public int unlockScore = 1000;
    [NonSerialized] public int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameCanvas.activeSelf)
        {
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                // TEMP
                gameCanvas.SetActive(false);
                levelSelectMenu.SetActive(true);
                
                // Activate prompt
            }
        }
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
        UpdateScore(10);
    }
}
