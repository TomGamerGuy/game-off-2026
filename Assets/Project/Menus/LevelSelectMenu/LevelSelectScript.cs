using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
    [SerializeField] Canvas levelSelectMenu;
    [SerializeField] Canvas cheatMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        //
    }

    // Opens a prompt asking if the user is sure. If yes, unlock all levels
    public void CheatButton()
    {
        //
    }
}
