using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Snake"))
        {
            Debug.Log("Snake hit obstacle!");
            Time.timeScale = 0; // stop de game
        }
    }
}
