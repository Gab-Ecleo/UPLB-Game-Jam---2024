using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    [Header("Player hud")]
    [SerializeField] private Image currentOxygen;
    [SerializeField] private Image currentFood;
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text oxygenText;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool isPaused;

    [Header("Tween Animation")]
    [SerializeField] private GameObject pausePanelGO;
    [SerializeField] private CanvasGroup pausefadePanel;
    [SerializeField] private float topY;
    [SerializeField] private float midY;
    [SerializeField, Range(0,1)] private float tweenDuration;
    
    private RectTransform pauseRectTrans;
    private PlayerData _playerData;

    private void Awake()
    {
        if(pausePanelGO != null)
            pauseRectTrans = pausePanelGO.GetComponent<RectTransform>();
    }

    private void Start()
    {
        _playerData = GameManager.Instance.FetchPlayerData();
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    private void Update()
    {
        UpdateUIBar();

        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                isPaused = true;
                PauseGame();
            }
            else
            {
                isPaused = false;
                UnpauseGame();
            }

        }
    }

    private void UpdateUIBar()
    {
        currentFood.fillAmount = _playerData.FoodSupply / 5f;
        currentOxygen.fillAmount = _playerData.Oxygen;

        foodText.text = _playerData.FoodSupply.ToString();
        oxygenText.text = _playerData.Oxygen.ToString("P0").Replace("%", "");
    }

    #region Pause Panel
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        PausePanelIntro();
    }

    public async void UnpauseGame()
    {
        await PausePanelOutro();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void PausePanelIntro()
    {
        pausefadePanel.DOFade(1, tweenDuration).SetUpdate(true);
        pauseRectTrans.DOAnchorPosY(midY, tweenDuration).SetUpdate(true);
    }

    async Task PausePanelOutro()
    {
        pausefadePanel.DOFade(0, tweenDuration).SetUpdate(true);
        await pauseRectTrans.DOAnchorPosY(topY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
    #endregion
}
