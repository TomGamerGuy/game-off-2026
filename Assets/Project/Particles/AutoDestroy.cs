using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float destroyTime = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
