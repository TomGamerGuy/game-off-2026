using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] AudioClip backgroundMusic;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
