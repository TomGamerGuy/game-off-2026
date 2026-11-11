using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
    [SerializeField] Canvas levelSelectMenu;

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
}
