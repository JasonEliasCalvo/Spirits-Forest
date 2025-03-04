using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioMixerGroup sfxGroup;
    private AudioSource audioSource;

    [Header("Menu")]
    [SerializeField]
    private AudioClip winSound;
    [SerializeField]
    private AudioClip deathSound;

    [Header("Player")]
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip attackSound;
    [SerializeField]
    private AudioClip damegeSound;
    [SerializeField]
    private AudioClip healSound;

    [Header("Boss 1")]
    [SerializeField]
    private AudioClip catSound;

    [Header("Boss 2")]
    [SerializeField]
    private AudioClip ghostSound;

    [Header("Boss 3")]
    [SerializeField]
    private AudioClip demonSound;

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
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxGroup;
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(deathSound);
    }
    public void Win()
    {
        audioSource.PlayOneShot(winSound);
    }
    public void heal()
    {
        audioSource.PlayOneShot(healSound);
    }

    public void Jump()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    public void Attack()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void Damege()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void EnemySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void CatSound()
    {
        audioSource.PlayOneShot(catSound);
    }

    public void GhostSound()
    {
        audioSource.PlayOneShot(ghostSound);
    }

    public void DemonSound()
    {
        audioSource.PlayOneShot(demonSound);
    }
}
