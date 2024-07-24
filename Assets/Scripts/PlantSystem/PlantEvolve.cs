using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEvolve : MonoBehaviour
{
    public GameObject PlantLvl1;
    public GameObject PlantLvl2;
    public GameObject PlantLvl3;
    public float PlantAbsorption; //variable for the plant absorption (used for progression starting from 0 then slowly to 1f)
    public float PlantHeightRequirement; //A speciic value for the plant to trigger isRaised value (1.8f is default)
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
        if (isRaised && !hasCalled && _crankplant.PlantPos.y >= 1.81f)
        {
            Debug.Log("Now Raised");
            //Absorb sunlight by .45 every 1.7 seconds
            StartCoroutine(PlantGrowth());
        }
        if (_crankplant.PlantPos.y <= 1.8f)
        {
            isRaised = false;
        }

        if (PlantAbsorption >= .25f && !ReadytoHarvestPlant)
        {
            ReadytoHarvestPlant = true;

        }
    }

    IEnumerator PlantGrowth()
    {
        hasCalled = true;
        PlantAbsorption += .045f;
        Debug.Log($"Absorption at {PlantAbsorption}");

        if (PlantAbsorption >= .15f)
        {
            //UpgradeToLvl2();
            PlantLvl1.SetActive(false);
            PlantLvl2.SetActive(true);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.plantGrow, this.transform.position);
        }
        if (PlantAbsorption >= .2f)
        {
            //UpgradeToLvl3();
            PlantLvl2.SetActive(false);
            PlantLvl3.SetActive(true);
            ReadytoHarvestPlant = true;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.plantGrow, this.transform.position);
        }
        if (PlantAbsorption >= 1)
        {
            //PlantAbsorption = 1;
        }
        yield return new WaitForSeconds(PlantGrowthCooldown);
        hasCalled = false;
    }

    public void ResetPlantLvl()
    {
        hasCalledPlantReset = true;
        PlantLvl1.SetActive(true);
        PlantLvl2.SetActive(false);
        PlantLvl3.SetActive(false);
        PlantAbsorption = 0f;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.plantGrow, this.transform.position);

        supply.hasHarvested = false;
        ReadytoHarvestPlant = false;

        Debug.Log("PlantReset");

        hasCalledPlantReset = false;
    }
}
