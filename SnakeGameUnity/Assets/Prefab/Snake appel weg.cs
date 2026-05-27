using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private AppleSpawner appleSpawner;
    public float score = 0;

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
            //doubled for some reason
            score = score + 0.5f;
        }
    }
}



