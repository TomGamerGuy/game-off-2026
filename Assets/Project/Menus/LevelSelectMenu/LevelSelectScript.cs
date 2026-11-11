using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelSelectScript : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] Canvas levelSelectMenu;
    [SerializeField] Canvas cheatMenu;

    [Header("Buttons")]
    [SerializeField] Button muteButton;
    [SerializeField] Sprite muted;
    [SerializeField] Sprite unmuted;

    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("Audio")]
    [SerializeField] AudioClip interactSound;
    private AudioSource audioSource;
    private AudioSource menuAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameManager.GetComponent<AudioSource>();
        cheatMenu.gameObject.SetActive(false);
        menuAudio = GetComponent<AudioSource>();

        ShowMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        //TEMP
        CheatButton();
    }
}
