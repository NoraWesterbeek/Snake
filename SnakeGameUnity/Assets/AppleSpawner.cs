using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    public int gridWidth = 20;
    public int gridHeight = 20;

    private GameObject currentApple;

    void Start()
    {
        SpawnApple();
    }

    public void SpawnApple()
    {
        // Verwijder oude appel
        if (currentApple != null)
        {
            Destroy(currentApple);
        }

        // Kies een random gridpositie
        int x = Random.Range(0, gridWidth);
        int y = Random.Range(0, gridHeight);

        // Zet grid in het midden van de wereld
        float worldX = x - (gridWidth / 2);
        float worldY = y - (gridHeight / 2);

        Vector2 spawnPos = new Vector2(worldX, worldY);

        // Spawn nieuwe appel
        currentApple = Instantiate(applePrefab, spawnPos, Quaternion.identity);
        currentApple.tag = "Apple"; // extra veiligheid
    }
}
