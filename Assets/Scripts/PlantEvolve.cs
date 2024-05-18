using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEvolve : MonoBehaviour
{
    public GameObject PlantLvl1;
    public GameObject PlantLvl2;
    public GameObject PlantLvl3;
    public float PlantAbsorption; //variable for the plant absorption (used for progression starting from 0 then slowly to 1f)
    public bool isRaised; //Check if its cranked
    public bool ReadytoHarvestPlant;
    public bool hasCalledPlantReset;
    float PlantGrowthCooldown = 1.2f; //default is 1.7f
    CrankPlant _crankplant;
    FoodSupply supply;
    bool hasCalled;

    private void Start()
    {
        _crankplant = GetComponent<CrankPlant>();
        supply = GetComponent<FoodSupply>();
        PlantLvl1.SetActive(true);
        PlantLvl2.SetActive(false);
        PlantLvl2.SetActive(false);
        hasCalledPlantReset = false;
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
        if (PlantAbsorption >= .25f && !ReadytoHarvestPlant)
        {
            ReadytoHarvestPlant = true;

        }

        /*if (supply.hasHarvested)
        {
            ResetPlantLvl();
        }*/
        //Next if player pressed something to harvest plant
        //Sprite goes back to lvl 1 and plant absorption back to 0
    }

    IEnumerator PlantGrowth()
    {
        hasCalled = true;
        PlantAbsorption += .015f;
        Debug.Log($"Absorption at {PlantAbsorption}");

        if (PlantAbsorption >= .15f)
        {
            UpgradeToLvl2();
        }
        if (PlantAbsorption >= .2f)
        {
            UpgradeToLvl3();
        }
        if (PlantAbsorption >= 1)
        {
            PlantAbsorption = 1;
        }
        yield return new WaitForSeconds(PlantGrowthCooldown);
        hasCalled = false;
    }

    void UpgradeToLvl2()
    {
        bool hasCalledLvl2 = true;
        PlantLvl1.SetActive(false);
        PlantLvl2.SetActive(true);
        //yield return new WaitForSeconds(.1f);
        hasCalledLvl2 = false;
    }

    void UpgradeToLvl3()
    {
        bool hasCalledLvl3 = true;
        PlantLvl2.SetActive(false);
        PlantLvl3.SetActive(true);
        //yield return new WaitForSeconds(.1f);
        hasCalledLvl3 = false;
    }

    public void ResetPlantLvl()
    {
        hasCalledPlantReset = true;
        PlantLvl1.SetActive(true);
        PlantLvl2.SetActive(false);
        PlantLvl3.SetActive(false);
        PlantAbsorption = 0f;

        supply.hasHarvested = false;
        ReadytoHarvestPlant = false;

        Debug.Log("PlantReset");

        //yield return new WaitForSeconds(.1f);
        hasCalledPlantReset = false;
    }
}
