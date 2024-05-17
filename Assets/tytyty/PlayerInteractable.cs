using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] Vector2 offset;
    bool canInteract;

    private void Awake()
    {
        interactLayer = LayerMask.GetMask("Interactables");
    }

    private void Update()
    {
        // Interact Button
        if(Input.GetKeyDown(KeyCode.I) && DetectObject())
        {
            Debug.Log("Can Interact");
        }
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
