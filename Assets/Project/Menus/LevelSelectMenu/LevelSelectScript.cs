using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;
using TMPro;

public class LevelSelectScript : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] Canvas levelSelectMenu;
    [SerializeField] Canvas cheatMenu;
    [SerializeField] Canvas badgeCanvas;
    [SerializeField] GameObject infoCanvas;
    [SerializeField] GameObject statCanvas;

    [Header("Buttons")]
    [SerializeField] Button muteButton;
    [SerializeField] Sprite muted;
    [SerializeField] Sprite unmuted;
    [SerializeField] Button level1Button;
    [SerializeField] Button level2Button;
    [SerializeField] Button level3Button;
    [SerializeField] Button badgeButton;
    [SerializeField] Button cheatButton;

    [Header("GameManager")]
    [SerializeField] GameObject gameManager;
    private GameManagerScript gameManagerScript;
    private GameScript gameScript;
    private ObstacleSpawnerScript obstacleSpawnerScript;
    [SerializeField] GameObject player;
    [SerializeField] PlayerScript playerScript;

    [Header("Audio")]
    [SerializeField] AudioClip interactSound;
    public AudioSource audioSource;
    private AudioSource menuAudio;

    [Header("Levels")]
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Sprite level1Background;
    [SerializeField] Sprite level2Background;
    [SerializeField] Sprite level3Background;
    [SerializeField] GameObject levelBackground;

    [Header("Stats")]
    [SerializeField] TextMeshProUGUI planetText;
    [SerializeField] TextMeshProUGUI starText;
    [SerializeField] TextMeshProUGUI blackHoleText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameManager.GetComponent<AudioSource>();
        cheatMenu.gameObject.SetActive(false);
        menuAudio = GetComponent<AudioSource>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        gameCanvas.gameObject.SetActive(false);
        gameScript = gameManager.GetComponent<GameScript>();
        obstacleSpawnerScript = gameManager.GetComponent<ObstacleSpawnerScript>();
        badgeButton.gameObject.SetActive(false);
        badgeCanvas.gameObject.SetActive(false);
        infoCanvas.SetActive(false);
        statCanvas.SetActive(false);

        LevelButtonCheck();

        HideMenu();
    }

    // Update is called once per frame
    void Update()
    {
        // Disabled currently
        /* if (badgeCanvas.gameObject.activeSelf && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            badgeCanvas.gameObject.SetActive(false);
        }
        if (infoCanvas.activeSelf && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            infoCanvas.SetActive(false);
        } */
    }

    void ShowMenu()
    {
        levelSelectMenu.gameObject.SetActive(true);
    }

    void HideMenu()
    {
        levelSelectMenu.gameObject.SetActive(false);
    }

    // Shows info prompt
    public void InfoButton()
    {
        menuAudio.clip = interactSound;
        menuAudio.Play();
        
        infoCanvas.SetActive(!infoCanvas.activeSelf);
        EventSystem.current.SetSelectedGameObject(null);
    }

    // Update stats
    void UpdateStats()
    {
        planetText.text = gameManagerScript.levelData.level1Score.ToString();
        starText.text = gameManagerScript.levelData.level2Score.ToString();
        blackHoleText.text = gameManagerScript.levelData.level3Score.ToString();
    }

    // Show stats
    public void StatButton()
    {
        menuAudio.clip = interactSound;
        menuAudio.Play();

        UpdateStats();
        
        statCanvas.SetActive(!statCanvas.activeSelf);
        EventSystem.current.SetSelectedGameObject(null);
    }

    // Mutes and unmutes music
    public void MuteButton()
    {
        menuAudio.clip = interactSound;
        menuAudio.Play();
        
        EventSystem.current.SetSelectedGameObject(null);
        Image muteImage = muteButton.GetComponent<Image>();

        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            muteImage.sprite = muted;
        }
        else
        {
            audioSource.UnPause();
            muteImage.sprite = unmuted;
        }
    }

    // Opens a prompt asking if the user is sure. If yes, unlock all levels
    public void CheatButton()
    {
        menuAudio.clip = interactSound;
        menuAudio.Play();
        
        EventSystem.current.SetSelectedGameObject(null);
        cheatMenu.gameObject.SetActive(!cheatMenu.gameObject.activeSelf);
    }

    // Unlocks all levels if the user clicks yes for the prompt in the cheat menu
    public void UnlockAllLevels()
    {
        menuAudio.clip = interactSound;
        menuAudio.Play();

        cheatMenu.gameObject.SetActive(false);
        cheatButton.interactable = false;
        
        gameManagerScript.levelData.level2Unlocked = true;
        gameManagerScript.levelData.level3Unlocked = true;
        gameManagerScript.levelData.badgeUnlocked = true;

        LevelButtonCheck();
    }

    // Load level 1
    public void Level1Button()
    {
        EventSystem.current.SetSelectedGameObject(null);

        menuAudio.clip = interactSound;
        menuAudio.Play();

        LoadLevel(GameManagerScript.Level.Level1);
    }

    // Load level 2
    public void Level2Button()
    {
        EventSystem.current.SetSelectedGameObject(null);

        menuAudio.clip = interactSound;
        menuAudio.Play();

        LoadLevel(GameManagerScript.Level.Level2);
    }

    // Load level 3
    public void Level3Button()
    {
        EventSystem.current.SetSelectedGameObject(null);

        menuAudio.clip = interactSound;
        menuAudio.Play();

        LoadLevel(GameManagerScript.Level.Level3);
    }

    // Show a prompt that the player has earned the Captain's badge
    public void BadgeButton()
    {
        menuAudio.clip = interactSound;
        menuAudio.Play();
        
        badgeCanvas.gameObject.SetActive(!badgeCanvas.gameObject.activeSelf);
        EventSystem.current.SetSelectedGameObject(null);
    }

    // Check the tint and interactivity of buttons based on level data
    public void LevelButtonCheck()
    {     
        if (gameManagerScript.levelData.level1Score > gameScript.unlockScore && !gameManagerScript.levelData.level2Unlocked)
        {
            gameManagerScript.levelData.level2Unlocked = true;
        }
        if (gameManagerScript.levelData.level2Score > gameScript.unlockScore && !gameManagerScript.levelData.level3Unlocked)
        {
            gameManagerScript.levelData.level3Unlocked = true;
        }
        if (gameManagerScript.levelData.level3Score > gameScript.unlockScore && !gameManagerScript.levelData.badgeUnlocked)
        {
            gameManagerScript.levelData.badgeUnlocked = true;
        }
        
        if (gameManagerScript.levelData.level2Unlocked)
        {
            level2Button.interactable = true;
        }
        else
        {
            level2Button.interactable = false;
        }

        if (gameManagerScript.levelData.level3Unlocked)
        {
            level3Button.interactable = true;
        }
        else
        {
            level3Button.interactable = false;
        }

        if (gameManagerScript.levelData.badgeUnlocked)
        {
            badgeButton.gameObject.SetActive(true);
        }
        else
        {
            badgeButton.gameObject.SetActive(false);
        }
    }

    public void LoadLevel(GameManagerScript.Level level)
    {
        Sprite backgroundImage = level1Background; // To avoid an error
        player.SetActive(true);
        playerScript.transform.position = playerScript.spawnPosition;
        playerScript.rb.linearVelocity = Vector2.zero;

        switch (level)
        {
            case GameManagerScript.Level.Level1:
                // Load level 1 assets and variables. Art, gravity and score
                backgroundImage = level1Background;
                gameScript.gravityForce = gameScript.planetGravity;
                gameManagerScript.levelData.currentLevel = GameManagerScript.Level.Level1;
                obstacleSpawnerScript.maxObstacles = gameScript.planetObstacles;
                obstacleSpawnerScript.baseSpeed = gameScript.planetSpeed;
            break;
            case GameManagerScript.Level.Level2:
                // Load level 2 assets and variables
                backgroundImage = level2Background;
                gameScript.gravityForce = gameScript.starGravity;
                gameManagerScript.levelData.currentLevel = GameManagerScript.Level.Level2;
                obstacleSpawnerScript.maxObstacles = gameScript.starObstacles;
                obstacleSpawnerScript.baseSpeed = gameScript.starSpeed;
            break;
            case GameManagerScript.Level.Level3:
                // Load level 3 assets and variables
                backgroundImage = level3Background;
                gameScript.gravityForce = gameScript.blackHoleGravity;
                gameManagerScript.levelData.currentLevel = GameManagerScript.Level.Level3;
                obstacleSpawnerScript.maxObstacles = gameScript.blackHoleObstacles;
                obstacleSpawnerScript.baseSpeed = gameScript.blackHoleSpeed;
            break;
            default:
                //
            break;
        }

        // Load level and start game
        levelBackground.GetComponent<Image>().sprite = backgroundImage;
        levelSelectMenu.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
        gameScript.UpdateScore(0, 0);
        playerScript.SetHealth();
        obstacleSpawnerScript.obstacleCount = 0;

        // Destroy active obstacles
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        // 2. Loop through the array and destroy each one
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        gameScript.StartCountdown();
    }
}
