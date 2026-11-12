using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManagerScript gameManagerScript;

    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("Menus")]
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Game Variables")]
    public float gravityForce;
    public float playerSpeed;
    public int unlockScore = 1000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = gameObject;
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
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
    void UpdateScore(int add)
    {
        switch (gameManagerScript.levelData.currentLevel)
        {
            case GameManagerScript.Level.Level1:
                gameManagerScript.levelData.level1Score += add;
                scoreText.text = "Score: " + gameManagerScript.levelData.level1Score;
            break;
            case GameManagerScript.Level.Level2:
                gameManagerScript.levelData.level2Score += add;
                scoreText.text = "Score: " + gameManagerScript.levelData.level2Score;
            break;
            case GameManagerScript.Level.Level3:
                gameManagerScript.levelData.level3Score += add;
                scoreText.text = "Score: " + gameManagerScript.levelData.level3Score;
            break;
            default:
                //
            break;
        }
    }
}
