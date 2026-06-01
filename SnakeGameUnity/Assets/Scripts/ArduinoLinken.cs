using System.IO.Ports;
using UnityEngine;

public class SnakeBrainLink : MonoBehaviour
{
    SerialPort port = new SerialPort("COM3", 9600);
    public controller snake;

    void Start() {
        port.Open();
        port.ReadTimeout = 20;
    }

    void Update() {

        // 1. Beslissing ontvangen van Arduino
        try {
            string cmd = port.ReadLine().Trim();

            if (cmd == "L") snake.TurnLeft();
            if (cmd == "R") snake.TurnRight();
            if (cmd == "F") snake.GoForward();
        }
        catch {}

        // 2. Sensor info sturen naar Arduino
        string sensor = GetSensorInfo();
        port.WriteLine(sensor);
    }

    string GetSensorInfo() {
        // Voorbeeld: appel richting bepalen
        Vector3 appleDir = snake.GetAppleDirection();

        if (appleDir.x < 0) return "APPLE_LEFT";
        if (appleDir.x > 0) return "APPLE_RIGHT";
        return "APPLE_FORWARD";
    }

    void OnApplicationQuit() {
        if (port.IsOpen) port.Close();
    }
}
