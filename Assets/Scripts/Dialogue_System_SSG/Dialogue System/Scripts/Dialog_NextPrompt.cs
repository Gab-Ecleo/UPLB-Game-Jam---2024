using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_NextPrompt : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        DialogManager.Instance.NextPrompt();
    }
}
