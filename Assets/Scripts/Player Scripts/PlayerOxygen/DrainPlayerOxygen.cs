using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrainPlayerOxygen : MonoBehaviour
{
    private PlayerData _playerdata;
    
    public bool hasCalled;

    private void Start()
    {
        _playerdata = GameManager.Instance.FetchPlayerData();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasCalled = true;
        if (collision.CompareTag("Player"))
        {
            if (hasCalled)
            {
                //StartCoroutine(_drainOxygen());
                InvokeRepeating("drainoxygen", 1.2f, 2.4f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasCalled = false;
    }

    /*IEnumerator _drainOxygen()
    {
        PlayerOxygen oxygen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOxygen>();
        oxygen.oxygenCount -= .02f;
        yield return new WaitForSeconds(1.2f);
    }*/

    void drainoxygen()
    {
        PlayerOxygen oxygen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOxygen>();
        Debug.Log($"Decaying, Oxygenleft {_playerdata.Oxygen}");
        _playerdata.Oxygen -= .02f;
    }
}
