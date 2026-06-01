using UnityEngine;
using TMPro;



public class SnakeHead : MonoBehaviour
{
    private AppleSpawner appleSpawner;
    public float score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public controller controller;
    public int cycle;
    float score_per_cycle;

    //public controller controller;

    void SetScoreText()
    {
        scoreText.text = "apples: " + score.ToString();
        score_per_cycle = score / cycle * 100;
        scoreText2.text = "apples/cycles: " + score_per_cycle.ToString();
    }
    
    void Start()
    {
        appleSpawner = FindObjectOfType<AppleSpawner>();
        SetScoreText();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            appleSpawner.SpawnApple();
            //doubled for some reason
            score = score + 0.5f;
            SetScoreText();
        }
    }
    void Update()
    {
        cycle = controller.cycle;
        SetScoreText() ;
    }
}



