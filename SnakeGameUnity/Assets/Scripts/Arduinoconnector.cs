/*using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class ArduinoConnector : MonoBehaviour
{
    private SerialPort serialPort = new SerialPort("COM8", 9600); 
    public string decision = "";  // "L", "R" or "F"
    private Thread readThread;
    private bool running = true;

    void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 100;

        readThread = new Thread(ReadArduino);
        readThread.Start();

        Debug.Log("Arduino connected!");
    }

    // Listens to Arduino in the background
    void ReadArduino()
    {
        while (running)
        {
            try
            {
                decision = serialPort.ReadLine().Trim();
            }
            catch (System.Exception) { }
        }
    }

    // Sends 2 bytes to Arduino
    public void SendSignals(bool left, bool right)
    {
        if (!serialPort.IsOpen) return;

        byte[] signals = new byte[2];
        signals[0] = left ? (byte)1 : (byte)0;
        signals[1] = right ? (byte)1 : (byte)0;

        serialPort.Write(signals, 0, 2);
    }

    void OnApplicationQuit()
    {
        running = false;
        readThread?.Join();
        if (serialPort.IsOpen) serialPort.Close();
    }
}
*/