using System;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Structs
    [System.Serializable]
    public struct LevelData
    {
        public int level1Score;
        public int level2Score;
        public int level3Score;
        public bool level2Unlocked;
        public bool level3Unlocked;
        public bool badgeUnlocked;
    };
    
    [Header("Audio")]
    [SerializeField] AudioClip backgroundMusic;
    private AudioSource audioSource;
    
    [Header("LevelData")]
    public LevelData levelData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;

        audioSource.Play();

        levelData.level1Score = 0;
        levelData.level2Score = 0;
        levelData.level3Score = 0;

        levelData.level2Unlocked = false;
        levelData.level3Unlocked = false;
        levelData.badgeUnlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
