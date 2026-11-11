using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Private
    [SerializeField] private Canvas mainMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowMenu();
    }

    // Update is called once per frame
    void Update()
    {
        // No updating needed here
    }

    void ShowMenu()
    {
        mainMenu.gameObject.SetActive(true);
    }

    void HideMenu()
    {
        mainMenu.gameObject.SetActive(false);
    }

    // When the PlayButton is clicked, disable the menu
    public void PlayButton()
    {
        HideMenu();
    }
}
