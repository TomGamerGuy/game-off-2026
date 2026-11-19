using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Private
    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] GameObject infoCanvas;
    
    [Header("Audio")]
    private AudioSource audioSource;
    [SerializeField] AudioClip interactSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ShowMenu();
    }

    // Update is called once per frame
    void Update()
    {
        // No updating needed here
    }

    void ShowMenu()
    {
        mainMenu.SetActive(true);
    }

    void HideMenu()
    {
        mainMenu.SetActive(false);
    }

    // When the PlayButton is clicked, disable the menu
    public void PlayButton()
    {
        audioSource.clip = interactSound;
        audioSource.Play();
        HideMenu();
        levelSelectMenu.SetActive(true);
        infoCanvas.SetActive(true);
    }
}
