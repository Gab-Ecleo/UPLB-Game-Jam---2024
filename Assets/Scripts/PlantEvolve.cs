using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEvolve : MonoBehaviour
{
    [SerializeField] private GameObject PlantLvl1;
    [SerializeField] private GameObject PlantLvl2;
    [SerializeField] private GameObject PlantLvl3;
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
        if (isRaised && !hasCalled && _crankplant.PlantPos.y >= 1.81f)
        {
            Debug.Log("Now Raised");
            //Absorb sunlight by .15 every 1.7 seconds
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
        PlantAbsorption += .015f;
        Debug.Log($"Absorption at {PlantAbsorption}");

        if (PlantAbsorption >= .15f)
        {
            //UpgradeToLvl2();
            PlantLvl1.SetActive(false);
            PlantLvl2.SetActive(true);
        }
        if (PlantAbsorption >= .2f)
        {
            //UpgradeToLvl3();
            PlantLvl2.SetActive(false);
            PlantLvl3.SetActive(true);
            ReadytoHarvestPlant = true;
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

        supply.hasHarvested = false;
        ReadytoHarvestPlant = false;

        Debug.Log("PlantReset");

        //yield return new WaitForSeconds(.1f);
        hasCalledPlantReset = false;
    }

    /* void UpgradeToLvl2()
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
   }*/
}
