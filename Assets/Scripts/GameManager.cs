using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private PlayerData defaultData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ExpeditionChecker expeditionChecker;

    [SerializeField] private GameObject GameoverScreen;
    [SerializeField] private GameObject PlayerObj;

    public bool CanFixRadio;
    public bool playerCanMove;
    public bool inCutscene;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        CanFixRadio = false;
        inCutscene = false;
        playerCanMove = true;

        GameoverScreen.SetActive(false);

        SET_DEFAULT_PLAYER_DATA();
    }

    private void Update()
    {
        if (PlayerLost())
            GameOver();
    }

    public void GameOver()
    {
        playerCanMove = false;
        //Disable Audio
        AudioManager.instance.StopAmbience();
        AudioManager.instance.StopMusic();
        //Start Audio
        AudioManager.instance.InitializeMusic(FMODEvents.instance.loseMusic);

        GameoverScreen.SetActive(true); //Play Cutscene
        
        StartCoroutine(ReturnTimer());
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

    IEnumerator ReturnTimer()
    {
        yield return new WaitForSeconds(5);
        
        SceneManagerScript.Instance.QuitGameMainMenu();
    }

    #region Data Methods

    public void SET_DEFAULT_PLAYER_DATA()
    {
        playerData.Oxygen = defaultData.Oxygen;
        playerData.FoodSupply = defaultData.FoodSupply;
    }
    
    public PlayerData FetchPlayerData() 
    { 
        return playerData;
    }

    public GameObject FetchPlayerObj()
    {
        return PlayerObj;
    }

    public ExpeditionChecker FetchExpeditionChecker() 
    { 
        return expeditionChecker;
    }
    
    #endregion
    
}
