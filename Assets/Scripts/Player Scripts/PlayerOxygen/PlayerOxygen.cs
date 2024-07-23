using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    private static PlayerOxygen instance;
    public static PlayerOxygen Instance => instance;

    private PlayerData _playerData;
    
    public float OxygenDecayTime = 3.2f; //This value is for area if it doesnt have clouds yet
    [SerializeField] private float decayInterval = 2.8f;
    [SerializeField] private float oxygenGainVal = .25f;
    [SerializeField] private float decayValue = 0.020f;
    
    public bool isWeatherNeutral;
    public bool isWeatherCloudy;
    bool isWeatherCalled;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance !=this) Destroy(gameObject);

        EventManager.ON_END_CUTSCENE += RunOxygenDecay;
    }

    private void OnDestroy()
    {
        EventManager.ON_END_CUTSCENE -= RunOxygenDecay;
    }

    private void Start()
    {
        _playerData = GameManager.Instance.FetchPlayerData();
        _playerData.Oxygen = Mathf.Clamp(_playerData.Oxygen, 0, 1);
        
        if (isWeatherNeutral)
        {
            StartCoroutine("DrainOxygen_Neutral");
        }
    }
    private void Update()
    {
        if (isWeatherCloudy && !isWeatherCalled)
        {
            isWeatherNeutral = false;
            StartCoroutine(DrainOxygen_Cloudy());
        }
        if (isWeatherNeutral && !isWeatherCalled)
        {
            isWeatherCloudy = false;
            StartCoroutine(DrainOxygen_Neutral());
        }
    }

    void DrainOxygenNeutral()
    {
        Debug.Log($"Decaying, Oxygen left {_playerData.Oxygen}");
        _playerData.Oxygen -= decayValue;
    }
    IEnumerator DrainOxygen_Cloudy()
    {
        isWeatherCalled = true;
        Debug.Log($"Decaying, Oxygen left {_playerData.Oxygen}");
        _playerData.Oxygen -= .045f;
        yield return new WaitForSeconds(1f);
        isWeatherCalled = false;
    }

    IEnumerator DrainOxygen_Neutral()
    {
        isWeatherCalled = true;
        var oxygen = Mathf.Clamp(_playerData.Oxygen, 0, 1);
        
        Debug.Log($"Decaying, Oxygenleft {oxygen}");
        _playerData.Oxygen -= decayValue;
        yield return new WaitForSeconds(decayInterval);
        isWeatherCalled = false;

        if (!GameManager.Instance.inCutscene)
            StartCoroutine(DrainOxygen_Neutral());
    }

    private void RunOxygenDecay()
    {
        StartCoroutine(DrainOxygen_Neutral());
    }

    public void RefillOxygen()
    {
        Debug.Log($"Gained Oxygen Count by {.25f}");
        _playerData.Oxygen += oxygenGainVal;
        _playerData.Oxygen = Mathf.Clamp(_playerData.Oxygen, 0, 1);
    }
}
