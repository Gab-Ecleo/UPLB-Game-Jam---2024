using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class PlayerInteractable : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private Vector2 offset;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private void Awake()
    {
        interactLayer = LayerMask.GetMask("Interactables");
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            InteractObject();
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

    private void InteractObject()
    {
        float playerDirection = PlayerDirection();
        Vector2 detectionCenter = (Vector2)transform.position + offset * playerDirection;
        Collider2D hit = Physics2D.OverlapCircle(detectionCenter, radius, interactLayer);

        if (hit.gameObject.TryGetComponent(out IInteractable interactObj))
            interactObj.Interact();
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        
        float playerDirection = PlayerDirection();
        Gizmos.color = Color.green;
        Vector2 detectionCenter = (Vector2)transform.position + offset * playerDirection;
        Gizmos.DrawWireSphere(detectionCenter, radius);
    }

    private float PlayerDirection()
    {
        return PlayerMovement.Instance.IsPlayerFacingLeft() ? -1 : 1;
    }
}
