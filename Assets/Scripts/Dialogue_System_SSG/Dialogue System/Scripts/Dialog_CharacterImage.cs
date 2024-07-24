using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_CharacterImage : MonoBehaviour
{
    private Animator animator;
    private Image imageObject;

    private bool isActive;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        imageObject = GetComponent<Image>();

        imageObject.color = Color.gray;
        isActive = false;
    }

    public void SetCharacterImage(Sprite sprite)
    {
        imageObject.sprite = sprite;
    }

    public void EnlargeCharacter()
    {
        if (isActive) return;
        isActive = true;

        imageObject.color = Color.white;
        animator.SetTrigger("enlarge");
    }

    public void ShrinkCharacter()
    {
        if (!isActive) return;
        isActive = false;

        imageObject.color = Color.gray;
        animator.SetTrigger("shrink");
    }
}
