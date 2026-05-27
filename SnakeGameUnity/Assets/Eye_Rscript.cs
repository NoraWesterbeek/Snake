using UnityEngine;

public class Eye_R : MonoBehaviour
{

    public Transform snakeHead;
    public Transform EyeR;
    public SpriteRenderer sprite;
    public bool R_sees_apple;

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
        if (R_sees_apple)
        {
            Debug.Log("R_apple!");
            sprite.color = Color.green;
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
            R_sees_apple = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            R_sees_apple = false;
        }
    }
}



