using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CrankPlant : MonoBehaviour, IInteractable
{
    [Header("Transform Values")]
    public Vector3 PlantPos;
    
    [Header("Default Settings")]
    [SerializeField] private float CrankLimit = 2.80f;
    [SerializeField] private float Timedecay = 3f; //default is 3f (don't touch unless you want to speed the decay by lowering values)
    [SerializeField] private float LoweringCooldown = 2.5f; //default is 2.5f
    

    [SerializeField] private bool hasCranked;
    [SerializeField] private bool isLoweringDown;

    private HpPlantScript _plantHp;
    public bool collided;
    
    [Header("Must Assign")] 
    [SerializeField] private GameObject _plantPlatform;

    private PlantEvolve evolve;
    private FoodSupply foodSupply;
    
    void Start()
    {
        //Set Booleans
        hasCranked = false;
        isLoweringDown = false;

        //Set Values
        PlantPos = _plantPlatform.transform.position;
        
        //Set Components
        evolve = GetComponent<PlantEvolve>();
        foodSupply = GetComponent<FoodSupply>();
        _plantHp = GetComponent<HpPlantScript>();
    }
    
    void Update()
    {
        if (_plantPlatform.transform.position.y >= -1.95 && PlantPos.y >= -1.95)
        {
            Timedecay += Time.deltaTime; //Time decay counts by default
        }

        if (_plantPlatform.transform.position.y == -1.95f && PlantPos.y == -1.95f)
        {
            Timedecay = 0f;
        }

        if (Timedecay >= 3f && !hasCranked && !isLoweringDown)
        {
            StartCoroutine(LowerPlantPlatform());
        }
    }

    public void Interact()
    {
        WheelSpinner.Instance.StartWheelUI(TriggerPlantMachine, DisplayProgress);
    }

    private void TriggerPlantMachine()
    {
        GameManager.Instance.playerCanMove = true;
        StartCoroutine(TriggerCrankPlant());
        RefillOxygen();
        
        hasCranked = true;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.plantRaise, this.transform.position);
        
        if(evolve.ReadytoHarvestPlant)
            HarvestPlant();
    }

    private void DisplayProgress(float num)
    {
        Math.Round(num, 1);
        Debug.Log($"Crank Progress: {num}%");
    }

    private void RefillOxygen()
    {
        PlayerOxygen.Instance.RefillOxygen();
    }

    private void HarvestPlant()
    {
        Debug.Log("Refilling Resources");
        foodSupply.hasHarvested = true;
        if (foodSupply.hasHarvested) foodSupply.AddFoodSupplyCount();
        evolve.ResetPlantLvl();
    }

    IEnumerator TriggerCrankPlant()
    {
        PlantPos.y += .4f;
        _plantPlatform.transform.position = PlantPos;

        hasCranked = true;
        Timedecay = 0f;

        if (_plantPlatform.transform.position.y >= CrankLimit) //Plant should not exceed to its origin height
        {
            PlantPos.y = CrankLimit;
            _plantPlatform.transform.position = PlantPos;
            evolve.isRaised = true;
        }
        yield return new WaitForSeconds(.8f);
        hasCranked = false;
    }

    IEnumerator LowerPlantPlatform()
    {
        Debug.Log("Lowering Down");
        PlantPos.y -= .20f;
        _plantPlatform.transform.position = PlantPos;

        isLoweringDown = true;

        if (PlantPos.y <= -1.95f)
        {
            PlantPos.y = -1.95f;
            _plantPlatform.transform.position = PlantPos;
        }
        yield return new WaitForSeconds(LoweringCooldown);
        isLoweringDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && evolve.ReadytoHarvestPlant && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("PRESSED");
            foodSupply.hasHarvested = true;
            if (foodSupply.hasHarvested) foodSupply.AddFoodSupplyCount();
            evolve.ResetPlantLvl();
            PlayerOxygen oxygen = collision.GetComponent<PlayerOxygen>();
            oxygen.RefillOxygen();
        }
        if (collision.tag == "Sunlight")
        {
            Debug.Log("Sunlight Detected");
            evolve.isRaised = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        collided = false;
    }
}
