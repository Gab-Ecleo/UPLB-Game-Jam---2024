using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui_ItemRequirement : MonoBehaviour
{
    GameObject Player;
    [SerializeField] private GameObject UIRequirement;
    [SerializeField] private PlayerData playerItem;
    [SerializeField] private TMP_Text PlantAmount;
    [SerializeField] private TMP_Text OxygenAmount;
    float ClampValue;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        UIRequirement.SetActive(false);
        if (UIRequirement == null &&
            PlantAmount == null && OxygenAmount == null&& this.gameObject.name == "Ui_ReqBorder")
            Debug.LogError("Missing UI Requirement tooltip border");
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerItem();
        float distance = Vector2.Distance(this.gameObject.transform.position, Player.transform.position);
        if (distance < 3) UIRequirement.SetActive(true);
        if (distance > 3) UIRequirement.SetActive(false);
    }
    void UpdatePlayerItem()
    {
        ClampValue = Mathf.Round(playerItem.Oxygen * 100f) / 100;
        PlantAmount.text = playerItem.FoodSupply.ToString();
        OxygenAmount.text = ClampValue.ToString();
    }
}
