using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public Animator animate;
    public bool HasAppeared = false;

    private void Awake()
    {
        animate = GetComponent<Animator>();
    }
    void Start()
    {
        //To start the comic, the StartofComic needs to be
        //taken off through animation so the player can scroll
        if (this.gameObject.CompareTag("StartofComic"))
            PlayFadeIn();
        if (this.gameObject.name == "FadeOutCredits")
        {
            Debug.Log("this is the gameObject");
            PlayFadeOut();
            Invoke("CreditsFadeToMenu", 1.5f);
        }
    }

    public void PlayFadeIn()
    {
        animate.SetTrigger("TR_DoFadeIn");
        HasAppeared = true;
    }
    public void PlayFadeOut()
    {
        animate.SetTrigger("TR_DoFadeOut");
    }

    public void CloseComic() //For Comic 1,2, and 3 (Basically for Comic Headers tht contains blackbars)
    {
        animate.SetTrigger("TR_CloseComic");
    }

    public void OpenComic()
    {
        animate.SetTrigger("TR_OpenComic");
    }

    //For NewGame Button
    public void StartNewGameFade()
    {
        Invoke("LoadNewGameTimer",2f);
    }

    //Do not call this, this is only for new game
    void LoadNewGameTimer()
    {
        SceneManager.LoadScene("PrologueCutscene");
    }
    public void CreditsFadeToMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

}
