using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] Vector2 offset;

    private void Awake()
    {
        interactLayer = LayerMask.GetMask("Interactables");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            DetectObject();
        }
    }

    private void DetectObject()
    {
        Vector2 detectionCenter = (Vector2)transform.position + offset;
        Collider2D hit = Physics2D.OverlapCircle(detectionCenter, radius, interactLayer);

        if (hit != null)
        {
            Debug.Log("hit");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 detectionCenter = (Vector2)transform.position + offset;
        Gizmos.DrawWireSphere(detectionCenter, radius);
    }
}
