using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerOxygen PlayerOxygen;
    [SerializeField] FoodSupply FoodSupply;

    [SerializeField] private Image currentOxygen;
    [SerializeField] private Image currentFood;

    private void Update()
    {
        UpdateUIBar();
    }

    private void UpdateUIBar()
    {
        currentFood.fillAmount = FoodSupply.FoodSupplyCount / 5f;
        currentOxygen.fillAmount = PlayerOxygen.oxygenCount;
    }
}
