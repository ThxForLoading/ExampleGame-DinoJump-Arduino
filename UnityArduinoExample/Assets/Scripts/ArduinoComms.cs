using UnityEngine;
using UnityEngine.Events;
using System.IO.Ports;
using System;

public class ArduinoComms : MonoBehaviour
{
    SerialPort _serial = new SerialPort("COM3", 9600);

    public bool jumpButton = false;

    public bool duckButton = false;

    public bool playerAlive = true;

    private bool _lastAlive;

    public static ArduinoComms instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _serial.Open();
        _serial.ReadTimeout = 100;
        _serial.WriteTimeout = 100;
    }

    void Update()
    {
        if (_serial.IsOpen)
        {
            //try
            //{
            //    string data = _serial.ReadLine();
            //    if (data != null)
            //    {
            //        switch (data)
            //        {
            //            case "JumpTrue":
            //                jumpButton = true;
            //                break;
            //            case "JumpFalse":
            //                jumpButton = false;
            //                break;
            //            case "DuckTrue":
            //                duckButton = true;
            //                break;
            //            case "DuckFalse":
            //                duckButton = false;
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}
            //catch (TimeoutException)
            //{
            //    //If needed throw or handle here
            //}
        }


        if(playerAlive != _lastAlive)           //Only write when change occurs to avoid flooding the serial port
        {
            if (playerAlive)
            {
                //write to arduino -> player alive
                WriteToSerial("1");
            }
            else
            {
                //write to arduino -> player dead
                WriteToSerial("0");
            }
            _lastAlive = playerAlive;
        }
    }

    private void OnApplicationQuit()
    {
        _serial.Close();
    }

    private void WriteToSerial(string text)
    {
        if (_serial.IsOpen && _serial != null)
        {
            _serial.Write(text);
        }
    }
}
