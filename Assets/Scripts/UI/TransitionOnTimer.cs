using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionOnTimer : MonoBehaviour
{
    [SerializeField] private float delayTime;
    [SerializeField] string SceneName;
    private void Update()
    {
        delayTime -=Time.deltaTime;

        if (delayTime <= 0 )
        {
            LoadScene(SceneName);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScene(SceneName);
        }
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == "Credits")
        {
            SceneManager.LoadScene(0);
        }
        else if (sceneName == "Transition")
        {
            int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(sceneToLoad);
        }

    }
}
