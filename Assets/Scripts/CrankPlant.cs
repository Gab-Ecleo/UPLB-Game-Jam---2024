using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankPlant : MonoBehaviour
{
    public bool playerCollision;
    [SerializeField] private Vector3 PlantAscendValue;
    [SerializeField] private bool hasCranked;
    [SerializeField] private bool isLoweringDown;
    [SerializeField] private float CrankLimit = 2.80f;
    [SerializeField] private float Timedecay; //default is 3f
    [SerializeField] private float LoweringCooldown; //default is 2.5f

    PlantEvolve evolve;
    // Start is called before the first frame update
    void Start()
    {
        PlantAscendValue = gameObject.transform.position;
        hasCranked = false;
        isLoweringDown = false;
        playerCollision = false;
        evolve = GetComponent<PlantEvolve>();
    }

    // Update is called once per frame
    void Update()
    {
        //Detects if it collides with player (has set value to 'true') and has not yet cranked
        if (Input.GetKey(KeyCode.Mouse0) && !hasCranked /*&& playerCollision*/)
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

        //if plant hitbox hits sunlight hitbox, 'isRaised' is activated
        //if it does function here
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerCollision = true;
        }
        if (collision.tag == "Sunlight")
        {
            evolve.isRaised = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCollision = false;
    }
}
