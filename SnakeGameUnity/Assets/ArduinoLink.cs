using System.IO.Ports;
using UnityEngine;

public class ArduinoLink : MonoBehaviour
{
    SerialPort port = new SerialPort("COM3", 9600);

    public controller snake;

    void Start()
    {
        port.Open();
        port.ReadTimeout = 20;
    }

    void Update()
    {
        SendSignalsToArduino();
        ReadDecisionFromArduino();
    }

    void SendSignalsToArduino()
    {
        bool leftSignal = snake.seesAppleLeft || snake.seesObstacleLeft;
        bool rightSignal = snake.seesAppleRight || snake.seesObstacleRight;

        byte[] data = new byte[2];
        data[0] = (byte)(leftSignal ? 1 : 0);
        data[1] = (byte)(rightSignal ? 1 : 0);

        port.Write(data, 0, 2);
    }

    void ReadDecisionFromArduino()
    {
        try
        {
            string msg = port.ReadLine().Trim();

            if (msg == "L") snake.TurnLeft();
            else if (msg == "R") snake.TurnRight();
            else if (msg == "F") snake.GoForward();
        }
        catch {}
    }

    void OnApplicationQuit()
    {
        if (port.IsOpen) port.Close();
    }
}
