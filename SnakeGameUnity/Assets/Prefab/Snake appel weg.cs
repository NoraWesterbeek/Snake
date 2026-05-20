using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private AppleSpawner appleSpawner;

    void Start()
    {
        appleSpawner = FindObjectOfType<AppleSpawner>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            appleSpawner.SpawnApple();
        }
    }
}



