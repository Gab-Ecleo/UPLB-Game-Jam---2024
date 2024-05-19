using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionOnTimer : MonoBehaviour
{
    [SerializeField] private float delayTime;

    private void Update()
    {
        delayTime -=Time.deltaTime;

        if (delayTime <= 0 )
        {
            // load game scene
            int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
