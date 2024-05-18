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
    float PlantGrowthCooldown = 1.7f; //default is 1.7f
    CrankPlant _crankplant;
    bool hasCalled;

    private void Start()
    {
        _crankplant = GetComponent<CrankPlant>();
        PlantLvl1.SetActive(true);
        PlantLvl2.SetActive(false);
        PlantLvl2.SetActive(false);
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
        if (PlantAbsorption >= 1 && !ReadytoHarvestPlant)
        {
            ReadytoHarvestPlant = true;

        }

        //Next if player pressed something to harvest plant
        //Sprite goes back to lvl 1 and plant absorption back to 0
    }

    IEnumerator PlantGrowth()
    {
        hasCalled = true;
        bool calledLvl2 = false;
        bool calledLvl3 = false;
        PlantAbsorption += .015f;
        Debug.Log($"Absorption at {PlantAbsorption}");

        if (PlantAbsorption >= .3f && !calledLvl2)
        {
            Debug.Log("CALLED! Plant to lvl 2");
            PlantLvl1.SetActive(false);
            PlantLvl2.SetActive(true);
            calledLvl2 = true;
        }
        if (PlantAbsorption >= .6f && !calledLvl3)
        {
            Debug.Log("CALLED! Plant to lvl 3");
            PlantLvl2.SetActive(false);
            PlantLvl3.SetActive(true);
            calledLvl3 = true;
        }
        if (PlantAbsorption >= 1)
        {
            PlantAbsorption = 1;
        }
        yield return new WaitForSeconds(PlantGrowthCooldown);
        hasCalled = false;
        if (calledLvl2) calledLvl2 = false;
        if (calledLvl3) calledLvl3 = false;
    }
}
