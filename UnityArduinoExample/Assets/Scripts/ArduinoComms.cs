using UnityEngine;
using UnityEngine.Events;

public class ArduinoComms : MonoBehaviour
{

    UnityEvent jumpTrigger;

    bool playerAlive = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (false)       //Here we wanna do a check for the arduino input
        {
            jumpTrigger.Invoke();
        }
        if (playerAlive)
        {
            //write to arduino -> player alive
        }
        else
        {
            //write to arduino -> player dead
        }
    }
}
