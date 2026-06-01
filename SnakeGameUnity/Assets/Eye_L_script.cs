using UnityEngine;
using System.Collections.Generic;

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

    public float distance;
    public List<Transform> objects;
    int closest_index;
    
    void Start()
    {
        offset = new Vector3(-EyeL.localScale.x/2, EyeL.localScale.y/2, 0);
        startColor = sprite.color;
        distance = 10000;
    }

    void Update()
    {
        EyeL.position = snakeHead.position + snakeHead.rotation * offset;
        EyeL.rotation = snakeHead.rotation;

        if (objects.Count != 0)
        {
            if (distance != 10000)
            {
                distance = Vector3.Distance(objects[closest_index].transform.position, snakeHead.position);
            }

            for (int i = 0; i < objects.Count; i++)
            {
                if (distance > Vector3.Distance(objects[i].transform.position, snakeHead.position))
                {
                    distance = Vector3.Distance(objects[i].transform.position, snakeHead.position);
                    closest_index = i;
                }
            }
            //Debug.Log(objects[closest_index]);
            //Debug.Log(objects[closest_index].tag);
            if(objects[closest_index].tag == "Obstacle")
            {
                L_sees_obstacle = true;
                L_sees_apple = false;
                sprite.color = Color.red;
                Debug.Log("L_obstacle!");
            }
            else if (objects[closest_index].tag == "Apple")
            {
                L_sees_apple = true;
                L_sees_obstacle = false;
                Debug.Log("L_apple!");
                sprite.color = Color.green;
            }
            else
            {
                L_sees_apple = false;
                L_sees_obstacle = false;
                sprite.color = startColor;
            }
        }
        else
        {
            closest_index = 0;
            distance = 0;
            L_sees_apple = false;
            L_sees_obstacle = false;
            sprite.color = startColor;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        objects.Add(other.transform);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        objects.Remove(other.transform);
        distance = 10000;
    }
}
