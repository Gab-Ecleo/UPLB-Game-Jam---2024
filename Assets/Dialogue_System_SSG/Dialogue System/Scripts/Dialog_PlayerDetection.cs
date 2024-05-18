using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Dialog_PlayerDetection : MonoBehaviour
{
    [Header("Must Assign")]
    [SerializeField] private Transform player;
    [SerializeField] private Dialog_Instance dialog;

    [Header("Default Settings")]
    [SerializeField] private GameObject prompt;
    [SerializeField] private KeyCode key;
    [SerializeField] private float detectionRange;

    private UnityAction onCompleteAction;
    private float distance;
    private bool allowAction;

    private void Awake()
    {
        allowAction = false;
    }

    public void SetDialog(Dialog_Instance newDialog)
    {
        dialog = newDialog;
        allowAction = false;
    }

    public void SetDialog(Dialog_Instance newDialog, UnityAction action)
    {
        dialog = newDialog;
        onCompleteAction = action;
        allowAction = true;
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);
        if (prompt.activeSelf) prompt.SetActive(false);

        if (detectionRange < distance) return;
        if(!prompt.activeSelf) prompt.SetActive(true);

        if (Input.GetKeyDown(key))
        {
            if (allowAction) DialogManager.Instance.SetDialog(dialog, onCompleteAction);
            else DialogManager.Instance.SetDialog(dialog);
        }
    }
}
