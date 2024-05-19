using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorSequence : MonoBehaviour
{
    [SerializeField] private Animator[] doors;

    private bool isDoorOpen;

    private void Start()
    {
        isDoorOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isDoorOpen)
                OpenDoor();
            else if (isDoorOpen)
                CloseDoor();
        }
            
    }

    public void OpenDoor()
    {
        StartCoroutine("OpenDoorSeq");
        isDoorOpen = true;
    }

    public void CloseDoor()
    {
        StartCoroutine("CloseDoorSeq");
        isDoorOpen = false;
    }

    IEnumerator OpenDoorSeq()
    {
        Debug.Log("Opening Door");
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetTrigger("Open");
            yield return new WaitForSeconds(.2f);
        }
    }
    
    IEnumerator CloseDoorSeq()
    {
        Debug.Log("Closing Door");
        for (int i = doors.Length - 1; i >= 0; i--)
        {
            doors[i].SetTrigger("Close");
            yield return new WaitForSeconds(.2f);
        }
    }
}
