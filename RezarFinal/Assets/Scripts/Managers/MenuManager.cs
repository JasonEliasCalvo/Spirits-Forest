using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private Image[] healthBarFill;
    [SerializeField] private Sprite[] healthBarSprites;
    [SerializeField] private PlayerStats playerStats;
    public GameObject combatInterface, pauseMenu, GameOver, Victory;
    int clampedHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ShowPlayerInfo();
    }

    public void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.WorldExplored) return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            if (PauseMenu.Intance.isPause)
                PauseMenu.Intance.Play();
            else
                PauseMenu.Intance.Pause();
        }
    }

    public void SaveScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LastSceneIndex", currentSceneIndex);
        PlayerPrefs.Save();
    }

    private void UpdateHealthBar()
    {
        clampedHealth = Mathf.Clamp(playerStats.health, 0, playerStats.healthMax);

        if (clampedHealth > 0 && clampedHealth <= healthBarSprites.Length)
        {
            healthBarFill[0].sprite = healthBarSprites[clampedHealth - 1];
        }
        else
        {
            return;
        }
    }

    public IEnumerator AnimDamage()
    {
        for (int i = 0; i < 6; i++)
        {
            healthBarFill[1].enabled = !healthBarFill[1].enabled;
            yield return new WaitForSeconds(0.07f);
        }
        if (clampedHealth > 0 && clampedHealth <= healthBarSprites.Length)
        {
            healthBarFill[1].sprite = healthBarSprites[clampedHealth - 1];
        }
        else
        {
            yield break;
        }            
    }

    public void ShowPlayerInfo()
    {
        combatInterface.SetActive(true);
        UpdateHealthBar();   
    }
}
