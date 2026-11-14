using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;

public class LevelSelectScript : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] Canvas levelSelectMenu;
    [SerializeField] Canvas cheatMenu;
    [SerializeField] Canvas badgeCanvas;

    [Header("Buttons")]
    [SerializeField] Button muteButton;
    [SerializeField] Sprite muted;
    [SerializeField] Sprite unmuted;
    [SerializeField] Button level1Button;
    [SerializeField] Button level2Button;
    [SerializeField] Button level3Button;
    [SerializeField] Button badgeButton;

    [Header("GameManager")]
    [SerializeField] GameObject gameManager;
    private GameManagerScript gameManagerScript;
    private GameScript gameScript;
    [SerializeField] GameObject player;
    private PlayerScript playerScript;

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

    // Enums
    enum Level
    {
        Level1,
        Level2,
        Level3
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameManager.GetComponent<AudioSource>();
        cheatMenu.gameObject.SetActive(false);
        menuAudio = GetComponent<AudioSource>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        gameCanvas.gameObject.SetActive(false);
        gameScript = gameManager.GetComponent<GameScript>();
        playerScript = player.GetComponent<PlayerScript>();
        badgeButton.gameObject.SetActive(false);
        badgeCanvas.gameObject.SetActive(false);

        LevelButtonCheck();

        HideMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (badgeCanvas.gameObject.activeSelf && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            badgeCanvas.gameObject.SetActive(false);
        }
    }

    void ShowMenu()
    {
        levelSelectMenu.gameObject.SetActive(true);
    }

    void HideMenu()
    {
        levelSelectMenu.gameObject.SetActive(false);
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
        
        gameManagerScript.levelData.level2Unlocked = true;
        gameManagerScript.levelData.level3Unlocked= true;

        LevelButtonCheck();
    }

    // Load level 1
    public void Level1Button()
    {
        EventSystem.current.SetSelectedGameObject(null);

        menuAudio.clip = interactSound;
        menuAudio.Play();

        LoadLevel(Level.Level1);
    }

    // Load level 2
    public void Level2Button()
    {
        EventSystem.current.SetSelectedGameObject(null);

        menuAudio.clip = interactSound;
        menuAudio.Play();

        LoadLevel(Level.Level2);
    }

    // Load level 3
    public void Level3Button()
    {
        EventSystem.current.SetSelectedGameObject(null);

        menuAudio.clip = interactSound;
        menuAudio.Play();

        LoadLevel(Level.Level3);
    }

    // Show a prompt that the player has earned the Captain's badge
    public void BadgeButton()
    {
        badgeCanvas.gameObject.SetActive(!badgeCanvas.gameObject.activeSelf);
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

    void LoadLevel(Level level)
    {
        Sprite backgroundImage = level1Background; // To avoid an error
        playerScript.transform.position = playerScript.spawnPosition;
        playerScript.rb.linearVelocity = Vector2.zero;

        switch (level)
        {
            case Level.Level1:
                // Load level 1 assets and variables. Art, gravity and score
                backgroundImage = level1Background;
                gameScript.gravityForce = 2f;
                gameManagerScript.levelData.currentLevel = GameManagerScript.Level.Level1;
            break;
            case Level.Level2:
                // Load level 2 assets and variables
                backgroundImage = level2Background;
                gameScript.gravityForce = 4f;
                gameManagerScript.levelData.currentLevel = GameManagerScript.Level.Level2;
            break;
            case Level.Level3:
                // Load level 3 assets and variables
                backgroundImage = level3Background;
                gameScript.gravityForce = 8f;
                gameManagerScript.levelData.currentLevel = GameManagerScript.Level.Level3;
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

        gameScript.StartCountdown();
    }
}
