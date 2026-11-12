using UnityEngine;
using UnityEngine.InputSystem;

public class GameScript : MonoBehaviour
{
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject levelSelectMenu;

    [Header("Game Variables")]
    public float gravityForce;
    public float playerSpeed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
