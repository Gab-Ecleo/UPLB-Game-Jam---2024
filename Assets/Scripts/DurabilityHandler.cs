using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabilityHandler : MonoBehaviour
{
    [SerializeField] private int minUseCount = 5;
    [SerializeField] private int maxUseCount = 10;

    private int useCount;

    private void Awake()
    {
        useCount = UnityEngine.Random.Range(minUseCount, maxUseCount + 1);
    }

    //check durability first before activating Action
    public void DeductDurability(Action OnDeductDurabilitySuccess)
    {
        if( useCount <= 0 )
        {
            Debug.Log("Deduction Cancelled: Durability is zero");
            return;
        }

        useCount--;
        Debug.Log("Deducted Durability...");

        OnDeductDurabilitySuccess?.Invoke();
    }
}
