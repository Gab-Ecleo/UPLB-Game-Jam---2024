using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSettings
{
    public string name;
    public Sprite sprite;
}

[System.Serializable]
public class DialogPrompt
{
    public CharacterSettings character1Settings;
    public CharacterSettings character2Settings;

    [Header("Script Settings")]
    [Tooltip("isSameCharacters: Reference character 1 and 2 settings from Element 0.")]
    public bool isSameCharacters;
    [Tooltip("isLeftTalking: Highlight the left character on true and Highlight the right character on false.")]
    public bool isLeftTalking;
    [TextArea] public string dialog;
}

[CreateAssetMenu(fileName = "NPC Dialog Instance")]
public class Dialog_Instance : ScriptableObject
{
    [TextArea] public string dialogDescription;
    [Header("Conversation Setup:")]
    [Header("Add elements for every prompts.")]
    public DialogPrompt[] conversation;
}
