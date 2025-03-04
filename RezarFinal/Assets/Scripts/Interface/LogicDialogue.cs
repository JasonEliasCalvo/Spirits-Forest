using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicDialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)]
    private string[] dialogeLines;
    [SerializeField]
    private float typingTime; 

    private int lineIndex;

    private void Start()
    {
        StartDialogue();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(dialogueText.text == dialogeLines[lineIndex])
            {
                nextDialoguLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogeLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        GameManager.Instance.ChangeState(GameManager.GameState.Dialogue);
        StartCoroutine(ShowLine());
    }

    private void nextDialoguLine()
    {
        lineIndex++;
        if (lineIndex < dialogeLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
            GameManager.Instance.ChangeState(GameManager.GameState.WorldExplored);
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogeLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }
}
