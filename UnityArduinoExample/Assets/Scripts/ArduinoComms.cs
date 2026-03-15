using UnityEngine;
using UnityEngine.Events;
using System.IO.Ports;

public class ArduinoComms : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM3", 9600);

    public bool jumpButton = false;

    public bool duckButton = false;

    public bool playerAlive = true;

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
        serial.Open();
        serial.ReadTimeout = 100;
    }

    void Update()
    {
        //string data = serial.ReadLine();
        //int value = 0;
        //if (data != null || data.Length != 0)
        //{
        //    value = int.Parse(data);
        //}
        //Debug.Log(value);


        //if (value > 0)       //Here we wanna do a check for the arduino input
        //{
        //    duckButton = false;
        //    jumpButton = true;
        //}
        //else if(value < 0) 
        //{
        //    duckButton = true;
        //    jumpButton = false;
        //}
        //else
        //{
        //    jumpButton = false;
        //    duckButton = false;
        //}

        if (playerAlive)
        {
            //write to arduino -> player alive
            serial.Write("1");
        }
        else
        {
            //write to arduino -> player dead
            serial.Write("0");
        }
    }

    private void OnApplicationQuit()
    {
        serial.Close();
    }
}
