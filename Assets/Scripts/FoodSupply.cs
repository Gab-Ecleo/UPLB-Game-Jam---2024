using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSupply : MonoBehaviour
{
    public int FoodSupplyCount; //Food count point it gives to the player's food supply count and transfer it to player 'playerFoodSupplyCount'
    public bool hasHarvested;
    private void Start()
    {
        hasHarvested = false;
    }

    public void AddFoodSupplyCount()
    {
        Debug.Log("Has Collected");
        FoodSupplyCount++;
        AddToPlayerSupply();
    }

    //Add to Player's FoodSupply (Temporary Script Function)
    public void AddToPlayerSupply()
    {
        GameManager.Instance.FetchPlayerData().FoodSupply += FoodSupplyCount;
        FoodSupplyCount = 0;
    }
}
