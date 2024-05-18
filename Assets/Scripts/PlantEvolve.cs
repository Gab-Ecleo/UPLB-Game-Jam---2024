using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEvolve : MonoBehaviour
{
    public float PlantAbsorption = 0f; //variable for the plant absorption (used for progression starting from 0 then slowly to 1f)
    public bool isRaised; //Check if its cranked
    float PlantGrowthCooldown = 1.7f; //default is 1.7f
    CrankPlant _crankplant;
    bool hasCalled;

    private void Start()
    {
        _crankplant = GetComponent<CrankPlant>();
    }

    private void Update()
    {
        if (isRaised && !hasCalled && _crankplant.PlantAscendValue.y >= 1.81f)
        {
            Debug.Log("Now Raised");
            //Absorb sunlight by .15 every 1.7 seconds
            StartCoroutine(PlantGrowth());
        }
        if (_crankplant.PlantAscendValue.y <= 1.8f)
        {
            isRaised = false;
        }
    }

    IEnumerator PlantGrowth()
    {
        hasCalled = true;
        PlantAbsorption += .015f;
        Debug.Log($"Absorption at {PlantAbsorption}");

        if (PlantAbsorption >= .15f)
        {
            Debug.Log("Plant to lvl 2");
        }
        yield return new WaitForSeconds(PlantGrowthCooldown);
        hasCalled = false;
    }
}
