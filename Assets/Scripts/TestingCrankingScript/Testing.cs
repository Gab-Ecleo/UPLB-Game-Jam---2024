using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    void FunctionHere()
    {
        Debug.Log("CompletedSpinning");

    }
    void SecondFunction(float numHere)
    {
        Debug.Log("SpinningProgress:" + numHere);
    }
    public void ButtonFunctionHere()
    {
        DuppedWheelSpinner.Instance.StartWheelUI(FunctionHere,SecondFunction);
    }
}
