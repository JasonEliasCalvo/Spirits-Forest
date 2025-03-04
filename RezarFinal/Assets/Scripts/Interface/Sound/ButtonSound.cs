using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip click;
    [SerializeField]
    private AudioClip hover;
    [SerializeField, Range(0f, 1f)]
    private float hoverVolume = 0.5f;

    public void ClickAudio()
    {
        audioSource.PlayOneShot(click);
    }

    public void HoverAudio()
    {
        audioSource.PlayOneShot(hover, hoverVolume);
    }
}
