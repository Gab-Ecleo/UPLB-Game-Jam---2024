using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    private static PlayerOxygen instance;
    public static PlayerOxygen Instance => instance;

    private PlayerData _playerData;
    
    public float OxygenDecayTime = 3.2f; //This value is for area if it doesnt have clouds yet
    
    public bool isWeatherNeutral;
    public bool isWeatherCloudy;
    bool isWeatherCalled;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance !=this) Destroy(gameObject);
    }
    private void Start()
    {
        _playerData = GameManager.Instance.FetchPlayerData();
        _playerData.Oxygen = Mathf.Clamp(_playerData.Oxygen, 0, 1);
        
        if (isWeatherNeutral)
        {
            InvokeRepeating("DrainOxygenNeutral", 1.3f, 2.6f);
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
        Debug.Log($"Decaying, Oxygenleft {_playerData.Oxygen}");
        _playerData.Oxygen -= .020f;
    }
    IEnumerator DrainOxygen_Cloudy()
    {
        isWeatherCalled = true;
        Debug.Log($"Decaying, Oxygenleft {_playerData.Oxygen}");
        _playerData.Oxygen -= .045f;
        yield return new WaitForSeconds(1f);
        isWeatherCalled = false;
    }

    IEnumerator DrainOxygen_Neutral()
    {
        isWeatherCalled = true;
        Debug.Log($"Decaying, Oxygenleft {_playerData.Oxygen}");
        _playerData.Oxygen -= .020f;
        yield return new WaitForSeconds(1.3f);
        isWeatherCalled = false;
    }

    public void RefillOxygen()
    {
        Debug.Log($"Gained Oxygen Count by {.25f}");
        _playerData.Oxygen += .25f;
        if (_playerData.Oxygen >= 1f) _playerData.Oxygen = 1f;
    }
}
