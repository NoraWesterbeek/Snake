using UnityEngine;

public class Eye_R : MonoBehaviour
{

    public Transform snakeHead;
    public Transform EyeR;
    public SpriteRenderer sprite;

    Vector3 offset;

    Color startColor;

    void Start()
    {
        offset = new Vector3(EyeR.localScale.x/2, EyeR.localScale.y/2, 0);
        startColor = sprite.color;
    }

    void Update()
    {
        EyeR.position = snakeHead.position + snakeHead.rotation * offset;
        EyeR.rotation = snakeHead.rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            Debug.Log("Apple_R!");
            sprite.color = Color.green;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            sprite.color = startColor;
        }
    }
}



