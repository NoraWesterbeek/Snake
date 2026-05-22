using UnityEngine;

public class Eye_L : MonoBehaviour
{

    public Transform snakeHead;
    public Transform EyeL;
    Vector3 rotation;

    public SpriteRenderer sprite;

    Color startColor; 

    Vector3 offset;
    
    void Start()
    {
        offset = new Vector3(-EyeL.localScale.x/2, EyeL.localScale.y/2, 0);
        startColor = sprite.color;
    }

    void Update()
    {
        EyeL.position = snakeHead.position + snakeHead.rotation * offset;
        EyeL.rotation = snakeHead.rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            Debug.Log("Apple_L!");
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
