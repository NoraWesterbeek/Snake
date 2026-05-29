using UnityEngine;
using System;
using System.IO.Ports;

public class SimpleSerial
{
    SerialPort port;

    public SimpleSerial(string portName, int baud)
    {
        port = new SerialPort(portName, baud);
        port.ReadTimeout = 50;

        try { port.Open(); }
        catch (Exception e) { Debug.Log("Serial error: " + e.Message); }
    }

    public void WriteLine(string msg)
    {
        if (port.IsOpen)
            port.WriteLine(msg);
    }

    public string ReadLine()
    {
        try
        {
            return port.ReadLine().Trim();
        }
        catch
        {
            return null;
        }
    }
}
