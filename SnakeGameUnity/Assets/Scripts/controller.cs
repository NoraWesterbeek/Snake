using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class controller : MonoBehaviour
{
    public GameObject snake;
    public Transform snake_transform;
    public float time;
    public bool W;
    Vector3 direction = Vector3.up;
    public Vector2 vector_input;
    public float rl_input;
    public float input_cooldown;
    public bool input_on_cooldown;


    
    Vector3 lastPos_0;
    Vector3 lastPos_1;
    Vector3 lastPos_2;
    Vector3 lastPos_3;
    Vector3 lastPos_4;
    Vector3 lastPos_5;
    Vector3 lastPos_6;
    Vector3 lastPos_7;
    Vector3 lastPos_8;
    List<Vector3> last_positions = new List<Vector3>();

    Quaternion lastRotation_0;
    Quaternion lastRotation_1;
    Quaternion lastRotation_2;
    Quaternion lastRotation_3;
    Quaternion lastRotation_4;
    Quaternion lastRotation_5;
    Quaternion lastRotation_6;
    Quaternion lastRotation_7;
    Quaternion lastRotation_8;
    List<Quaternion> last_rotations = new List<Quaternion>();

    bool update;

    public Transform snake_body_0;
    public Transform snake_body_1;
    public Transform snake_body_2;
    public Transform snake_body_3;
    public Transform snake_body_4;
    public Transform snake_body_5;
    public Transform snake_body_6;
    public Transform snake_body_7;
    public Transform snake_body_8;

    List<Transform> snake_body = new List<Transform>();

    int cycle;

    float rotation = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        snake_body.Add(snake_body_0);
        snake_body.Add(snake_body_1);
        snake_body.Add(snake_body_2);
        snake_body.Add(snake_body_3);
        snake_body.Add(snake_body_4);
        snake_body.Add(snake_body_5);
        snake_body.Add(snake_body_6);
        snake_body.Add(snake_body_7);
        snake_body.Add(snake_body_8);

        for (var i = 0; i < snake_body.Count; i++)
        {
            last_positions.Add(snake_transform.position);
            last_rotations.Add(snake_transform.rotation);
        }
            
    }
    private InputSystem_Actions controls;

    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        vector_input = controls.Player.Move.ReadValue<Vector2>();
        rl_input = vector_input.x;


        if (input_on_cooldown)
        {
            input_cooldown += Time.deltaTime;
            if (input_cooldown > .1 && rl_input == 0)
            {
                input_on_cooldown = false;
                input_cooldown = 0;
            }
        }

        if (rl_input == 1 && !input_on_cooldown)
        {
            if (direction == Vector3.up)
            {
                direction = Vector3.right;
                rotation = -90f;
            
            }
            else if (direction == Vector3.right)
            {
                direction = Vector3.down;
                rotation = 180f;
               
            }
            else if (direction == Vector3.down)
            {
                direction = Vector3.left;
                rotation = -270f;
            }
            else
            {
                direction = Vector3.up;
                rotation = 0f;
            }
            input_on_cooldown = true;
        }
        if (rl_input == -1 && !input_on_cooldown)
        {
            if (direction == Vector3.up)
            {
                direction = Vector3.left;
                rotation = -270f;
            }
            else if (direction == Vector3.left)
            {
                direction = Vector3.down;
                rotation = 180f;
            }
            else if (direction == Vector3.down)
            {
                direction = Vector3.right;
                rotation = -90f;
            }
            else
            {
                direction = Vector3.up;
                rotation = 0f;
            }
            input_on_cooldown = true;
        }

            if (time > .5)
        {
            update = true;
        }
        

        if (update)
        {
            lastPos_0 = snake_transform.position;
            snake_transform.rotation = Quaternion.Euler(0, 0, rotation);
            snake_transform.Translate(new Vector3(0,1,0));

            time = 0;
            update = false;
            input_cooldown = 0;
            if (rl_input == 0)
            {
                input_on_cooldown = false;
            }

      
            snake_body[0].position = last_positions[0];
            last_positions[0] = snake_transform.position;

            for (var i = 1; i < snake_body.Count; i++)
            {

                snake_body[i].position = last_positions[i];
                last_positions[i] = snake_body[i - 1].position;
    
            }

            cycle =+ 1;
/*
            for (var i = 0; i < snake_body.Count; ++i)
            {
                for (var j = 0; ++i)
                {
                    
                }
            }
*/

        }
    }
}
