using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEvolve : MonoBehaviour
{
    float PlantAbsorption; //variable for the plant absorption (used for progression starting from 0 then slowly to 1f)
    public bool isRaised; //Check if its cranked
    float PlantGrowthCooldown; //default is 1.7f

    private void AccumulateSunlight()
    {
        if (isRaised)
        {
            //Absorb sunlight by .15 every 1.7 seconds
        }
    }

    IEnumerator PlantGrowth()
    {
        PlantAbsorption += .15f;
        Debug.Log($"Absorption at {PlantAbsorption}");
        yield return new WaitForSeconds(PlantGrowthCooldown);
    }
}
