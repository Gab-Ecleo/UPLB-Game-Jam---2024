using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySequence : MonoBehaviour
{
    public bool SequenceDone;
    public int counter;
    bool CanPlaySequence;
    [SerializeField] private GameObject NextComic;
    [SerializeField] private GameObject[] BlackBars;
    Cutscene animate;

    void Start()
    {
        counter = 0;
        animate = BlackBars[counter].GetComponent<Cutscene>();
        CanPlaySequence = true;
        Cutscene StartComic = NextComic.GetComponent<Cutscene>();
        StartComic.CloseComic();
        AudioManager.instance.InitializeAmbiance(FMODEvents.instance.spaceAmbience);
    }

    void Update()
    {
        if (CanPlaySequence)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && !SequenceDone)
                LoadNextSequencePrologue();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && SequenceDone && !CanPlaySequence)
        {
            OpenNextComic();
        }
    }

    void LoadNextSequencePrologue()
    {
        animate.PlayFadeIn();
        counter++;
        if (counter < BlackBars.Length)
        {
            if (SceneManagerScript.Instance.GetCurrentSceneName() == "EndingCutScene")
            {
                AudioManager.instance.InitializeAmbiance(FMODEvents.instance.morseCode);
            }
            animate = BlackBars[counter].GetComponent<Cutscene>();
        }

        if (counter == BlackBars.Length)
        {
            SequenceDone = true;
            Invoke("ConfirmSequence", .5f);
            //NextComic.SetActive(true);
            //animate.CloseComic();
        }
    }
    void ConfirmSequence()
    {
        CanPlaySequence = false;
        Debug.Log("Sequence Complete");
    }
    void OpenNextComic()
    {
        Invoke("CloseThisComic", .5f);
        Invoke("DisableThisComic", 2f);
        //NextComic.SetActive(true);
    }
    void CloseThisComic()
    {
        Cutscene _thisComic = GetComponent<Cutscene>();
        _thisComic.CloseComic();
    }
    void DisableThisComic()
    {
        this.gameObject.SetActive(false);
        NextComic.SetActive(true);
        if (NextComic.CompareTag("EndOfComic"))
        {
            Debug.Log("End Of Comic Sequence");
            Invoke("LoadSceneTimer", 2f);

        }
        
        //For Comic Endings
        if (NextComic.CompareTag("EndFinaleComic"))
        {
            Debug.Log("End Of Game Finale Comic Sequence");
            //Invoke("LoadMenuSceneTimer", 2f);
            Invoke("LoadCredits", 2f);
        }

    }

    void LoadSceneTimer()
    {
        //SceneManager.LoadScene("GameScene");
        SceneManagerScript.Instance.LoadScene("GameScene");
    }

    void LoadMenuSceneTimer()
    {
        SceneManager.LoadScene("GameMenu");
    }
    void LoadCredits()
    {
        AudioManager.instance.StopAmbience();
        SceneManager.LoadScene("Credits");
    }
}
