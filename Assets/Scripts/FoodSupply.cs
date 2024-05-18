using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSupply : MonoBehaviour
{
    public int FoodSupplyCount;
    public bool hasHarvested;
    PlantEvolve _plantEvolve;

    private void Start()
    {
        _plantEvolve = GetComponent<PlantEvolve>();
        hasHarvested = false;

    }

    public void AddFoodSupplyCount()
    {
        Debug.Log("Has Collected");
        FoodSupplyCount++;
    }
}
