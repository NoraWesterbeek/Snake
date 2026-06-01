using UnityEngine;

public class ArduinoLinken : MonoBehaviour
{
    public controller snake;

    // Simpele serial via Unity's standaard API
    SimpleSerial serial;

    void Start()
    {
        serial = new SimpleSerial("COM3", 9600);
    }

    void Update()
    {
        SendSignals();
        ReadDecision();
    }

    void SendSignals()
    {
        int L = snake.seesAppleLeft || snake.seesObstacleLeft ? 1 : 0;
        int R = snake.seesAppleRight || snake.seesObstacleRight ? 1 : 0;

        serial.WriteLine(L + "," + R);
    }

    void ReadDecision()
    {
        string msg = serial.ReadLine();

        if (msg == null) return;

        if (msg == "L") snake.rl_input = -1;
        if (msg == "R") snake.rl_input = 1;
        if (msg == "F") snake.rl_input = 0;
    }
}
