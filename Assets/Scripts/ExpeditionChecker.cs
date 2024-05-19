using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Expedition Checker")]
public class ExpeditionChecker : ScriptableObject
{
    [SerializeField] private ExpeditionRequirement[] expeditionRequirements;
    [SerializeField] private string notEnoughResourcesMessage; //not enough food and oxygen
    [SerializeField] private string notEnoughFoodMessage;
    [SerializeField] private string notEnoughOxygenMessage;
    [SerializeField] private string expeditionAllowedMessage;

    private int currentIndex = 0;

    //set the index back to zero for game restart
    public void ResetRequirements()
    {
        currentIndex = 0;
    }

    //concatenate to the next requirement
    public void NextRequirement()
    {
        currentIndex++;
        currentIndex = Mathf.Clamp(currentIndex + 1, 0, expeditionRequirements.Length - 1);
    }

    //for checking if we passed through all requirements (later stage)
    public bool IsLastRequirement()
    {
        return currentIndex >= expeditionRequirements.Length - 1;
    }

    //get status boolean (if expedition is active) and a status message for not enough resources
    public ExpeditionStatus GetStatus(float currentFood, float currentOxygen)
    {
        var status = new ExpeditionStatus();
        var requirement = expeditionRequirements[currentIndex];
        bool enoughFood = currentFood > requirement.food;
        bool enoughOxygen = currentOxygen > requirement.oxygen;

        status.status = enoughFood && enoughOxygen;

        if (!enoughFood && !enoughOxygen) 
            status.message = notEnoughResourcesMessage;

        else if (!enoughFood)
            status.message = notEnoughFoodMessage;

        else if (!enoughOxygen)
            status.message = notEnoughOxygenMessage;

        else status.message = expeditionAllowedMessage;

        return status;
    }

    [System.Serializable]
    private class ExpeditionRequirement
    {
        public float oxygen = 0.2f;
        public float food = 0.2f;
    }

    public class ExpeditionStatus
    {
        public bool status;
        public string message;
    }
}
