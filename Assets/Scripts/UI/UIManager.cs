using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image currentOxygen;
    [SerializeField] private Image currentFood;

    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text oxygenText;

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

        foodText.text = _playerData.FoodSupply.ToString();
        oxygenText.text = _playerData.Oxygen.ToString("P0").Replace("%", "");
    }
}
