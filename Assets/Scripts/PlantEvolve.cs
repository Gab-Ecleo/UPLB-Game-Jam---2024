using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEvolve : MonoBehaviour
{
    float PlantAbsorption = 0f; //variable for the plant absorption (used for progression starting from 0 then slowly to 1f)
    public bool isRaised; //Check if its cranked
    float PlantGrowthCooldown = 1.7f; //default is 1.7f
    CrankPlant _crankplant;

    private void Update()
    {
        AccumulateSunlight();
    }

    private void AccumulateSunlight()
    {
        if (isRaised)
        {
            //Absorb sunlight by .15 every 1.7 seconds
            StartCoroutine(PlantGrowth());
        }
    }

    IEnumerator PlantGrowth()
    {
        PlantAbsorption += .15f;
        Debug.Log($"Absorption at {PlantAbsorption}");
        yield return new WaitForSeconds(PlantGrowthCooldown);
    }
}
