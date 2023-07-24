using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] Text targetText;
    [SerializeField] Text nameText;
    [SerializeField] Image portrait;

    DialogueContainer currentDialogue;
    int currentTextLine;

    [Range(0f, 1f)]
    [SerializeField] float visibleTextPercentage;
    [SerializeField] float timePerLetter;
    float totalTimetoType, currentTime;
    string lineToShow;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }
        TypeOutText();
    }

    private void TypeOutText()
    {
        if (visibleTextPercentage >= 1f) { return; }
        currentTime += Time.deltaTime;
        visibleTextPercentage = currentTime / totalTimetoType;
        visibleTextPercentage = Mathf.Clamp(visibleTextPercentage, 0f, 1f);
        UpdateText();
    }

    void UpdateText()
    {
        int letterCount = (int)(lineToShow.Length * visibleTextPercentage);
        targetText.text = lineToShow.Substring(0, letterCount);
    }

    private void PushText()
    {
        if (visibleTextPercentage < 1f)
        {
            visibleTextPercentage = 1f;
            UpdateText();
            return;
        }
        currentTextLine += 1;
        if (currentTextLine >= currentDialogue.line.Count)
        {
            Conclude();
        }
        else
        {
            CycleLine();
        }
    }
    void CycleLine()
    {

        lineToShow = currentDialogue.line[currentTextLine];
        totalTimetoType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercentage = 0f;
        targetText.text = "";

        currentTextLine += 1;
    }

    public void Initialize(DialogueContainer dialogueContainer)
    {
        Show(true);
        currentDialogue = dialogueContainer;
        currentTextLine = 0;
        CycleLine();
        UpdatePortrait();
    }

    private void UpdatePortrait()
    {
        portrait.sprite = currentDialogue.actor.portrait;
        nameText.text = currentDialogue.actor.name;
    }

    private void Show(bool v)
    {
        gameObject.SetActive(v);
    }

    private void Conclude()
    {
        Debug.Log("Finished");
    }
}
