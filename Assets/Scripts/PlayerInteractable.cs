using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private Vector2 offset;
    private WheelSpinner WheelSpinner;

    private void Awake()
    {
        interactLayer = LayerMask.GetMask("Interactables");
        WheelSpinner = WheelSpinner.Instance;
    }

    private void Update()
    {
        // Interact Button
        if(Input.GetKeyDown(KeyCode.I) && DetectObject())
        {
            WheelSpinner.StartWheelUI(OnCrankComplete, OnCrankProgress);
        }
    }

    private void OnCrankComplete()
    {
        Debug.Log("Completed Crank!");
    }
    private void OnCrankProgress(float progress)
    {
        Debug.Log("Progress: " + progress);
    }

    private bool DetectObject()
    {
        Vector2 detectionCenter = (Vector2)transform.position + offset;
        Collider2D hit = Physics2D.OverlapCircle(detectionCenter, radius, interactLayer);

        if (hit != null)
        {
            return true;
        }
        return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 detectionCenter = (Vector2)transform.position + offset;
        Gizmos.DrawWireSphere(detectionCenter, radius);
    }
}
