using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

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

    bool update_input_cooldown;

    private SerialPort serialPort = new SerialPort("COM9", 9600);
    private Thread readThread;
    private bool running = true;
    private string arduinoDecision = "";

    Vector3 lastPos_0;
    List<Vector3> last_positions = new List<Vector3>();
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

    public int cycle;
    float rotation = 0;

    public Eye_L EyeL;
    public Eye_R EyeR;

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

        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 100;
            readThread = new Thread(ReadArduino);
            readThread.Start();
            Debug.Log("Arduino connected!");
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Arduino not connected: " + e.Message);
        }
    }

    void ReadArduino()
    {
        while (running)
        {
            try
            {
                arduinoDecision = serialPort.ReadLine().Trim();
            }
            catch (System.Exception) { }
        }
    }

    void SendSignals()
    {
        if (!serialPort.IsOpen) return;

        bool appleLeft = EyeL.L_sees_apple;
        bool appleRight = EyeR.R_sees_apple;
        bool obstacleLeft = EyeL.L_sees_obstacle;
        bool obstacleRight = EyeR.R_sees_obstacle;

        byte[] signals = new byte[4];
        signals[0] = appleLeft ? (byte)1 : (byte)0;  // index 0
        signals[1] = appleRight ? (byte)1 : (byte)0;  // index 1
        signals[2] = obstacleLeft ? (byte)1 : (byte)0;  // index 2
        signals[3] = obstacleRight ? (byte)1 : (byte)0;  // index 3

        serialPort.Write(signals, 0, 4);
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

    void Update()
    {
        time += Time.deltaTime;
        vector_input = controls.Player.Move.ReadValue<Vector2>();
        rl_input = vector_input.x;
        
        SendSignals();

        if (arduinoDecision == "L" && !input_on_cooldown)
        {
            rl_input = -1;
            arduinoDecision = "";

        }
        else if (arduinoDecision == "R" && !input_on_cooldown)
        {
            rl_input = 1;
            arduinoDecision = "";

        }
        else if (arduinoDecision == "F")
        {
            rl_input = 0;
            arduinoDecision = "";
        }

        if (rl_input == 1 && !input_on_cooldown)
        {
            if (direction == Vector3.up && !input_on_cooldown)
            {
                direction = Vector3.right;
                rotation = -90f;
                input_on_cooldown = true;
                rl_input = 0;
                //Debug.Log("To Right");
            }
            else if (direction == Vector3.right && !input_on_cooldown)
            {
                direction = Vector3.down;
                rotation = 180f;
                input_on_cooldown = true;
                rl_input = 0;
                //Debug.Log("To down");
            }
            else if (direction == Vector3.down && !input_on_cooldown)
            {
                direction = Vector3.left;
                rotation = -270f;
                input_on_cooldown = true;
                rl_input = 0;
                //Debug.Log("To left");
            }
            else if (!input_on_cooldown)
            {
                direction = Vector3.up;
                rotation = 0f;
                input_on_cooldown = true;
                rl_input = 0;
                //Debug.Log("To up");
            }
            input_on_cooldown = true;
            rl_input = 0;
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

        if (time > 1)
        {
            update = true;
        }

        if (update_input_cooldown)
        {
            input_cooldown += Time.deltaTime;
                if (input_cooldown > .5f)
                {
                    input_on_cooldown = false;
                    input_cooldown = 0;
                    update_input_cooldown = false;
                }
        }

        if (update)
        {
            lastPos_0 = snake_transform.position;
            snake_transform.rotation = Quaternion.Euler(0, 0, rotation);
            snake_transform.Translate(new Vector3(0, 1, 0));
            if (input_on_cooldown)
            {
                update_input_cooldown = true;
            }

            time = 0;
            update = false;
            input_cooldown = 0;
            snake_body[0].position = last_positions[0];
            last_positions[0] = snake_transform.position;

            for (var i = 1; i < snake_body.Count; i++)
            {
                snake_body[i].position = last_positions[i];
                last_positions[i] = snake_body[i - 1].position;
            }

            cycle = cycle + 1;
        }
    }

    void OnApplicationQuit()
    {
        running = false;
        readThread?.Join();
        if (serialPort.IsOpen) serialPort.Close();
    }
}