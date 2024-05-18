using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [Header("Parameter Change")]

    [SerializeField] private string parameterName;
    [SerializeField] private float parameterValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.SetAmbianceParameter(parameterName, parameterValue);
        }
    }

}
