using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CrankPlant : MonoBehaviour, IInteractable
{
<<<<<<< HEAD
    public bool playerCollision;
    HpPlantScript _plantHp;
    public Vector3 PlantAscendValue;
    public bool InteractionEnabled;
=======
    [Header("Transform Values")]
    public Vector3 PlantPos;
    
    [Header("Default Settings")]
    [SerializeField] private float CrankLimit = 2.80f;
    [SerializeField] private float Timedecay = 3f; //default is 3f (don't touch unless you want to speed the decay by lowering values)
    [SerializeField] private float LoweringCooldown = 2.5f; //default is 2.5f
    
>>>>>>> main
    [SerializeField] private bool hasCranked;
    [SerializeField] private bool isLoweringDown;

    public bool collided;

<<<<<<< HEAD
    PlantEvolve evolve;
    FoodSupply foodSupply;
    PlayerMovement movement;
    // Start is called before the first frame update
=======
    [Header("Must Assign")] 
    [SerializeField] private GameObject _plantPlatform;

    private PlantEvolve evolve;
    private FoodSupply foodSupply;
>>>>>>> main
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
        movement = FindObjectOfType<PlayerMovement>();
    }
    
    void Update()
    {
<<<<<<< HEAD
        //Detects if it collides with player (has set value to 'true') and has not yet cranked
        if (Input.GetKeyDown(KeyCode.E) && !hasCranked && playerCollision && _plantHp.PlantHp >= 0 && !InteractionEnabled &&movement.canMove) //Player Can crank the machine as long as the hp is > 0
        {
            movement.canMove = false;
            TriggerPlantMachine();
        }
        if (/*transform.position.y >= 0 && */PlantAscendValue.y >= 0)
        {
            Timedecay += Time.deltaTime; //Timedecay counts by default
=======
        if (_plantPlatform.transform.position.y >= 0 && PlantPos.y >= 0)
        {
            Timedecay += Time.deltaTime; //Time decay counts by default
>>>>>>> main
        }

        if (_plantPlatform.transform.position.y == 0 && PlantPos.y == 0)
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
        StartCoroutine(TriggerCrankPlant());
        hasCranked = true;
    }

    private void DisplayProgress(float num)
    {
        Math.Round(num, 1);
        Debug.Log($"Crank Progress: {num}%");
    }

    IEnumerator TriggerCrankPlant()
    {
        Debug.Log("Pressed");
<<<<<<< HEAD
        PlantAscendValue.y += .85f; //Default Value is .57f
        gameObject.transform.position = PlantAscendValue;
=======
        PlantPos.y += .4f;
        _plantPlatform.transform.position = PlantPos;
>>>>>>> main
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
<<<<<<< HEAD
        PlantAscendValue.y -= .15f; //Default is .20f
        gameObject.transform.position = PlantAscendValue;
=======
        PlantPos.y -= .20f;
        _plantPlatform.transform.position = PlantPos;
>>>>>>> main
        isLoweringDown = true;

        if (_plantPlatform.transform.position.y <= 0 && PlantPos.y <= 0)
        {
            PlantPos.y = 0;
            _plantPlatform.transform.position = PlantPos;
        }
        yield return new WaitForSeconds(LoweringCooldown);
        isLoweringDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
<<<<<<< HEAD
        collided = true;
        Debug.Log("Something hit me!");

        if (collision.tag == "Player")
        {
            Debug.Log("Player Detected");
            playerCollision = true;
        }
        if (collision.tag == "Player" && evolve.ReadytoHarvestPlant && Input.GetKeyDown(KeyCode.F) && collided)
=======
        if (collision.tag == "Player" && evolve.ReadytoHarvestPlant && Input.GetKeyDown(KeyCode.E))
>>>>>>> main
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
<<<<<<< HEAD

    void RaisePlant()
    {
        StartCoroutine(TriggerCrankPlant());
        hasCranked = true;
        movement.canMove = true;
        Debug.Log("CompletedSpinning");
    }
    void SecondFunction(float numHere)
    {
        Debug.Log("SpinningProgress:" + numHere);
    }
    public void TriggerPlantMachine()
    {
        InteractionEnabled = true;
        WheelSpinner.Instance.StartWheelUI(RaisePlant, SecondFunction);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCollision = false;
        collided = false;
        InteractionEnabled = false;
    }
=======
>>>>>>> main
}
