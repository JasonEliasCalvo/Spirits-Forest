using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public Animator animator;

    public void Continue()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        animator.SetTrigger("Play");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
