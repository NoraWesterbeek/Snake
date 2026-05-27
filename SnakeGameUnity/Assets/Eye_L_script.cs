using UnityEngine;

public class Eye_L : MonoBehaviour
{

    public Transform snakeHead;
    public Transform EyeL;
    Vector3 rotation;
    public bool L_sees_apple;
    public bool L_sees_obstacle;

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
        if (L_sees_apple && !L_sees_obstacle)
        {
            Debug.Log("L_apple!");
            sprite.color = Color.green;
        }
        
        else if (L_sees_apple && L_sees_obstacle) 
        {
            sprite.color = Color.purple;
        }
        else if (L_sees_obstacle)
        {
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = startColor;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            L_sees_apple = true;
        }
        if (other.CompareTag("Obstacle"))
        {
            L_sees_obstacle = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            L_sees_apple = false;
        }
        if (other.CompareTag("Obstacle"))
        {
            L_sees_obstacle = false;
        }
    }



}
