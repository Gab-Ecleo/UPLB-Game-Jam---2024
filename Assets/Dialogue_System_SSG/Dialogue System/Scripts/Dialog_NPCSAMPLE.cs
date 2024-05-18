using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_NPCSAMPLE : MonoBehaviour
{
    [SerializeField] private Dialog_PlayerDetection playerDetection;
    [SerializeField] private Dialog_Instance stephenStrauDialog;
    [SerializeField] private Dialog_Instance tutorial;

    // Start is called before the first frame update
    void Start()
    {
        playerDetection.SetDialog(tutorial);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
