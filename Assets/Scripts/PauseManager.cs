using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private bool isPaused;

    private void Start()
    {
        pauseScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!isPaused)
            {
                PauseGame();
            }else if (isPaused)
            {
                UnpauseGame();
            }

        }        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

}
