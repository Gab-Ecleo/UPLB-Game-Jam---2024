using System;
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

    private PlayerData _playerData;

    private void Start()
    {
        _playerData = GameManager.Instance.FetchPlayerData();
    }

    private void Update()
    {
        UpdateUIBar();
    }

    private void UpdateUIBar()
    {
        currentFood.fillAmount = _playerData.FoodSupply / 5f;
        currentOxygen.fillAmount = _playerData.Oxygen;
    }
}
