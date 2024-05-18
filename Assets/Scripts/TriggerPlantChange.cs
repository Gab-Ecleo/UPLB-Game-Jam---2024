using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlantChange : MonoBehaviour
{
    [SerializeField] private GameObject PlantMachine;
    PlantEvolve evolve;
    FoodSupply foodsupply;
    // Start is called before the first frame update
    void Start()
    {
        evolve = PlantMachine.GetComponent<PlantEvolve>();
        foodsupply = PlantMachine.GetComponent<FoodSupply>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && evolve.ReadytoHarvestPlant && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Clicked");
            foodsupply.hasHarvested = true;
            if (foodsupply.hasHarvested) foodsupply.AddFoodSupplyCount();
            evolve.ResetPlantLvl();
            PlayerOxygen oxygen = collision.GetComponent<PlayerOxygen>();
            oxygen.RefillOxygen();
        }
    }
}
