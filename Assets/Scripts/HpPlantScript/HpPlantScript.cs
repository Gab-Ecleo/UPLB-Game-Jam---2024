using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPlantScript : MonoBehaviour
{
    [SerializeField] private int PlantHp;
    [SerializeField] private float _HpDecayTime;

    [SerializeField] private GameObject Plant1Decayed;
    [SerializeField] private GameObject Plant2Decayed;
    [SerializeField] private GameObject Plant3Decayed;

    private bool isDecaying;
    private bool isDecayingInCloud;
    PlantEvolve evolve;
    CrankPlant _checkPlant;

    private void Start()
    {
        _checkPlant = GetComponent<CrankPlant>();
        evolve = GetComponent<PlantEvolve>();
    }

    private void Update()
    {
        if (evolve.isRaised)
        {
            isDecaying = false;
            isDecayingInCloud = false;
        }
        if (isDecaying)
        {
            isDecayingInCloud = false;
            _HpDecayTime += Time.deltaTime;
            if (_HpDecayTime >= 2.5f)
            {
                PlantHp -= 3;
                _HpDecayTime = 0;

            }
        }
        if (isDecayingInCloud)
        {
            isDecaying = false;
            _HpDecayTime += Time.deltaTime;
            if (_HpDecayTime >= 1.5f)
            {
                PlantHp -= 4;
                _HpDecayTime = 0;

            }
        }

    }
}
