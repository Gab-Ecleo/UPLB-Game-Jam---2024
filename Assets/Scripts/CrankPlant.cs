using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankPlant : MonoBehaviour
{
    public bool playerCollision;
    public Vector3 PlantAscendValue;
    [SerializeField] private bool hasCranked;
    [SerializeField] private bool isLoweringDown;
    [SerializeField] private float CrankLimit = 2.80f;
    [SerializeField] private float Timedecay; //default is 3f (don't touch unless you want to speed the decay by lowering values)
    [SerializeField] private float LoweringCooldown; //default is 2.5f

    public bool collided;

    PlantEvolve evolve;
    FoodSupply foodSupply;
    // Start is called before the first frame update
    void Start()
    {
        PlantAscendValue = gameObject.transform.position;
        hasCranked = false;
        isLoweringDown = false;
        playerCollision = false;
        evolve = GetComponent<PlantEvolve>();
        foodSupply = GetComponent<FoodSupply>();
    }

    // Update is called once per frame
    void Update()
    {
        //Detects if it collides with player (has set value to 'true') and has not yet cranked
        if (Input.GetKey(KeyCode.Mouse0) && !hasCranked && playerCollision)
        {
            StartCoroutine(TriggerCrankPlant());
            hasCranked = true;
        }
        if (/*transform.position.y >= 0 && */PlantAscendValue.y >= 0)
        {
            Timedecay += Time.deltaTime; //Timedecay counts by default
        }

        if (/*transform.position.y == 0 && */PlantAscendValue.y == 0)
        {
            Timedecay = 0f;
        }

        if (Timedecay >= 3f && !hasCranked && !isLoweringDown)
        {
            StartCoroutine(LowerPlantPlatform());
        }
    }

    IEnumerator TriggerCrankPlant()
    {
        Debug.Log("Pressed");
        PlantAscendValue.y += .57f;
        gameObject.transform.position = PlantAscendValue;
        hasCranked = true;
        Timedecay = 0f;

        if (transform.position.y >= CrankLimit) //Plant should not exceed to its origin height
        {
            PlantAscendValue.y = CrankLimit;
            transform.position = PlantAscendValue;
            evolve.isRaised = true;
        }
        yield return new WaitForSeconds(.4f);
        hasCranked = false;
    }

    IEnumerator LowerPlantPlatform()
    {
        Debug.Log("Lowering Down");
        PlantAscendValue.y -= .20f;
        gameObject.transform.position = PlantAscendValue;
        isLoweringDown = true;

        if (transform.position.y <= 0 && PlantAscendValue.y <= 0)
        {
            PlantAscendValue.y = 0;
            transform.position = PlantAscendValue;
        }
        yield return new WaitForSeconds(LoweringCooldown);
        isLoweringDown = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collided = true;
        Debug.Log("Something hit me!");

        if (collision.tag == "Player")
        {
            Debug.Log("Player Detected");
            playerCollision = true;
        }
        if (collision.tag == "Player" && evolve.ReadytoHarvestPlant && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Clicked");
            foodSupply.hasHarvested = true;
            if (foodSupply.hasHarvested) foodSupply.AddFoodSupplyCount();
            evolve.ResetPlantLvl();
        }
        if (collision.tag == "Sunlight")
        {
            Debug.Log("Sunlight Detected");
            evolve.isRaised = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCollision = false;
        collided = false;
    }
}
