using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPlantScript : MonoBehaviour
{
    public int PlantHp;
    [SerializeField] private float _HpDecayTime;

    [SerializeField] private GameObject Plant1Decayed;
    [SerializeField] private GameObject Plant2Decayed;
    [SerializeField] private GameObject Plant3Decayed;

    private bool isDecaying;
    private bool isDecayingInCloud;
    private bool isDecayingPaused; //Checks if the plant is raised height enough, the decaying stops until it lowers back again
    PlantEvolve plantMachine;

    private void Start()
    {
        plantMachine = GetComponent<PlantEvolve>();
    }

    private void Update()
    {
        if (plantMachine.isRaised && !isDecayingPaused)
        {
            isDecayingPaused = true;
        }
        if (isDecaying && !isDecayingPaused)
        {
            isDecayingInCloud = false;
            _HpDecayTime += Time.deltaTime;
            if (_HpDecayTime >= 2.5f)
            {
                PlantHp -= 3;
                _HpDecayTime = 0;

            }
        }
        if (isDecayingInCloud && !isDecayingPaused)
        {
            isDecaying = false;
            _HpDecayTime += Time.deltaTime;
            if (_HpDecayTime >= 1.5f)
            {
                PlantHp -= 4;
                _HpDecayTime = 0;

            }
        }

        if (PlantHp <= 0 && plantMachine.PlantLvl1.activeSelf)
        {
            plantMachine.PlantLvl1.SetActive(false);
            Plant1Decayed.SetActive(true);
        }
        if (PlantHp <= 0 && plantMachine.PlantLvl2.activeSelf)
        {
            plantMachine.PlantLvl1.SetActive(false);
            Plant2Decayed.SetActive(true);
        }
        if (PlantHp <= 0 && plantMachine.PlantLvl3.activeSelf)
        {
            plantMachine.PlantLvl1.SetActive(false);
            Plant3Decayed.SetActive(true);
        }
    }
}
