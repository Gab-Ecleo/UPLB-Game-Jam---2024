using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog_DialogBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTextObject1;
    [SerializeField] private TextMeshProUGUI nameTextObject2;
    [SerializeField] private TextMeshProUGUI dialogTextObject;

    private bool isSkipped;
    private bool isComplete;

    private void Awake()
    {
        isSkipped = false;
        isComplete = false;
    }

    public void SkipDialog()
    {
        isSkipped = true;
    }

    public void SetLeftCharacterName(string name, bool highlight)
    {
        // nameTextObject1.color = GetHighlight(highlight);
        // nameTextObject1.text = name;
    }

    public void SetRightCharacterName(string name, bool highlight)
    {
        // nameTextObject2.color = GetHighlight(highlight);
        // nameTextObject2.text = name;
    }

    public void SetDialogText(string dialog)
    {
        isComplete = false;
        StartCoroutine(TextSlowType(dialog + " ", "", 0));
    }

    public bool IsComplete()
    {
        return isComplete;
    }

    private Color GetHighlight(bool active)
    {
        return active ? Color.white : Color.gray;
    }

    private IEnumerator TextSlowType(string fullText, string currentText, int index)
    {
        yield return new WaitForSeconds(0.08f);

        if (isSkipped)
        {
            dialogTextObject.text = fullText;
            isSkipped = false;
            isComplete = true;
            yield break;
        }

        string newText = currentText + fullText[index];
        dialogTextObject.text = currentText;

        index++;
        if(index >= fullText.Length)
        {
            isComplete = true;
            yield break;
        }
        else dialogTextObject.text += "_";

        StartCoroutine(TextSlowType(fullText, newText, index));
    }
}
