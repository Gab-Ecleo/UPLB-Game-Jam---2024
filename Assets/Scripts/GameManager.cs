using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private ExpeditionChecker expeditionChecker;

    public bool CanFixRadio;
    public bool playerCanMove;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        CanFixRadio = false;
        playerCanMove = true;
    }

    private void Update()
    {
        if (PlayerLost())
            GameOver();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public bool CanExped()
    {
        Debug.Log(expeditionChecker.GetStatus(playerData.FoodSupply, playerData.Oxygen).message);
        return expeditionChecker.GetStatus(playerData.FoodSupply, playerData.Oxygen).status;
    }

    public bool PlayerLost()
    {
        return playerData.Oxygen <= 0;
    }
    
    #region Data Methods
    
    public PlayerData FetchPlayerData() 
    { 
        return playerData;
    }

    public ExpeditionChecker FetchExpeditionChecker() 
    { 
        return expeditionChecker;
    }
    
    #endregion
    
}
