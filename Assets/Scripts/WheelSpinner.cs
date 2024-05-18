using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WheelSpinner : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private static WheelSpinner instance;
    public static WheelSpinner Instance => instance;

    [SerializeField] private GameObject inputBlocker;
    [SerializeField] private RectTransform wheelTransform;

    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;

    [SerializeField] private float minRotations = 3;
    [SerializeField] private float maxRotations = 5;

    private Vector2 startPosition;
    private Vector2 pivotPoint;

    private float totalRotation;
    private float targetRotation;

    private bool allowCranking = false;

    private Action<float> crankProgress;
    private Action crankComplete;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        pivotPoint = wheelTransform.position;
    }

    public void StartWheelUI(Action onCrankComplete)
    {
        wheelTransform.rotation = Quaternion.identity;
        totalRotation = 0;
        allowCranking = true;

        inputBlocker.SetActive(true);
        wheelTransform.gameObject.SetActive(true);

        var rotationCount = UnityEngine.Random.Range(minRotations, maxRotations);
        rotationCount *= UnityEngine.Random.value < 0.5f ? 1 : -1;

        SetArrow(rotationCount >= 0);

        targetRotation = rotationCount * 360;
        crankComplete = onCrankComplete;
    }

    public void StartWheelUI(Action onCrankComplete, Action<float> onCrankProgress)
    {
        StartWheelUI(onCrankComplete);
        crankProgress = onCrankProgress;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = eventData.position - pivotPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!allowCranking) return;

        var currentPosition = eventData.position - pivotPoint;

        var angle = Vector2.SignedAngle(startPosition, currentPosition);

        totalRotation += angle;

        SendProgress();

        wheelTransform.Rotate(0f, 0f, angle);

        startPosition = currentPosition;

        Debug.Log(targetRotation + " " + totalRotation);
        if (totalRotation < targetRotation) return;

        OnCloseUI();
    }

    private void SendProgress()
    {
        var progress = totalRotation / targetRotation;
        progress = Mathf.Clamp(progress, 0, 1);
        crankProgress?.Invoke(progress);
    }

    private void OnCloseUI()
    {
        crankComplete?.Invoke();
        inputBlocker.SetActive(false);
        wheelTransform.gameObject.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);

        allowCranking = false;
    }

    private void SetArrow(bool isLeft)
    {
        leftArrow.SetActive(isLeft);
        rightArrow.SetActive(!isLeft);
    }
}
