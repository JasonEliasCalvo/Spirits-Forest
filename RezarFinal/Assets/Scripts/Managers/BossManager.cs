using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    private Animator animator;

    [Header("Boss Health UI")]
    [SerializeField]
    private float detectionRange = 5f;
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)]
    private string[] bossDialogueLines;
    [SerializeField]
    private float typingTime;

    private Transform player;
    private bool dialogueStart;
    private int lineIndex;
    private bool endDialogue = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (endDialogue) return;
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= detectionRange && !dialogueStart)
        {
            StartBossDialogue();
        }

        if (dialogueStart && Input.GetKeyDown(KeyCode.I))
        {
            if (dialogueText.text == bossDialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = bossDialogueLines[lineIndex];
            }
        }
    }

    private void StartBossDialogue()
    {
        dialogueStart = true;
        dialoguePanel.SetActive(true);
        GameManager.Instance.ChangeState(GameManager.GameState.Dialogue);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < bossDialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in bossDialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void EndDialogue()
    {
        endDialogue = true;
        animator.SetBool("Start", endDialogue);
        dialogueStart = false;
        dialoguePanel.SetActive(false);
        StartBossBattle();
    }

    private void StartBossBattle()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.WorldExplored);
    }
}
