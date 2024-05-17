using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractable : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] Vector2 offset;

    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        interactLayer = LayerMask.GetMask("Player");
    }

    private void FixedUpdate()
    {
        DetectObject();
    }

    private void DetectObject()
    {
        Vector2 detectionCenter = (Vector2)transform.position + offset;
        Collider2D hit = Physics2D.OverlapBox(detectionCenter, boxSize, 0f, interactLayer);

        if(hit != null)
        {
            spriteRenderer.enabled = true; 
        }
        else 
        {
            spriteRenderer.enabled = false; 
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 detectionCenter = (Vector2)transform.position + offset;
        Gizmos.DrawWireCube(detectionCenter, boxSize);
    }
}
