using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincipalMenu : MonoBehaviour
{
    public Animator animator;
    public GameObject continueButton;

    void Start()
    {
        if (PlayerPrefs.HasKey("LastSceneIndex"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void Play()
    {
        StartCoroutine(ChangeScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        int lastSceneIndex = PlayerPrefs.GetInt("LastSceneIndex");
        StartCoroutine(ChangeScene(lastSceneIndex));
    }

    IEnumerator ChangeScene(int sceneIndex)
    {
        animator.SetTrigger("Play");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneIndex);
    }
}
