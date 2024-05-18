using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void myFunc()
    { 
        Debug.Log("Cranking is complete");
    }

    private void myOtherFunct(float num)
    {
        Debug.Log($"Current Progress: {num}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("This is being called");
            WheelSpinner.Instance.StartWheelUI(myFunc, myOtherFunct);
        }
            
    }
}
