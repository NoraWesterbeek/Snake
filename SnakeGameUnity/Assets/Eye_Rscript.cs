using UnityEngine;
using System.Collections.Generic;

public class Eye_R : MonoBehaviour
{

    public Transform snakeHead;
    public Transform EyeR;
    Vector3 rotation;
    public bool R_sees_apple;
    public bool R_sees_obstacle;

    public SpriteRenderer sprite;

    Color startColor;

    Vector3 offset;

    public float distance;
    public List<Transform> objects;
    int closest_index;

    void Start()
    {
        offset = new Vector3(EyeR.localScale.x / 2, EyeR.localScale.y / 2, 0);
        startColor = sprite.color;
        distance = 10000;
    }

    void Update()
    {
        EyeR.position = snakeHead.position + snakeHead.rotation * offset;
        EyeR.rotation = snakeHead.rotation;

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
            Debug.Log(objects[closest_index]);
            Debug.Log(objects[closest_index].tag);
            if (objects[closest_index].tag == "Obstacle")
            {
                R_sees_obstacle = true;
                R_sees_apple = false;
                sprite.color = Color.red;
            }
            else if (objects[closest_index].tag == "Apple")
            {
                R_sees_apple = true;
                R_sees_obstacle = false;
                Debug.Log("R_apple!");
                sprite.color = Color.green;
            }
            else
            {
                R_sees_apple = false;
                R_sees_obstacle = false;
                sprite.color = startColor;
            }
        }
        else
        {
            closest_index = 0;
            distance = 0;
            R_sees_apple = false;
            R_sees_obstacle = false;
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
