using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankPlant : MonoBehaviour
{
    [SerializeField] private Vector3 PlantAscendValue;
    [SerializeField] private bool hasCranked;
    [SerializeField] private bool isLoweringDown;
    float CrankLimit = 2.80f;
    public float Timedecay;
    // Start is called before the first frame update
    void Start()
    {
        PlantAscendValue = gameObject.transform.position;
        hasCranked = false;
        isLoweringDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !hasCranked)
        {
            StartCoroutine(TriggerCrankPlant());
            hasCranked = true;
        }
        if (transform.position.y >= 0 && PlantAscendValue.y >= 0)
        {
            Timedecay += Time.deltaTime; //Timedecay counts by default
        }

        if (transform.position.y == 0 && PlantAscendValue.y == 0)
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
        yield return new WaitForSeconds(2.5f);
        isLoweringDown = false;
    }
}
