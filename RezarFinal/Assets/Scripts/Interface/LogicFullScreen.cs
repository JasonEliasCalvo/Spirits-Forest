using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicFullScreen : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown resolution;
    private List<Resolution> uniqueResolutions;

    private void Start()
    {
        if (toggle != null)
        {
            toggle.isOn = Screen.fullScreen;
            toggle.onValueChanged.AddListener(ActivateFullScreen);
        }
        else
        {
            Debug.LogError("Toggle component is not assigned.");
        }

        CheckResolution();
        SetCurrentResolution();

        if (resolution != null)
        {
            resolution.onValueChanged.AddListener(delegate { ChangeResolution(resolution.value); });
        }
        else
        {
            Debug.LogError("Resolution Dropdown component is not assigned.");
        }
    }

    public void ActivateFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void CheckResolution()
    {
        Resolution[] allResolutions = Screen.resolutions;

        HashSet<string> uniqueResolutionSet = new HashSet<string>();
        uniqueResolutions = new List<Resolution>();

        foreach (Resolution resolution in allResolutions)
        {
            string resolutionString = resolution.width + " x " + resolution.height;
            if (uniqueResolutionSet.Add(resolutionString))
            {
                uniqueResolutions.Add(resolution);
            }
        }

        resolution.ClearOptions();

        List<string> options = new List<string>();

        foreach (Resolution resolution in uniqueResolutions)
        {
            options.Add(resolution.width + " x " + resolution.height);
        }

        resolution.AddOptions(options);
    }

    public void ChangeResolution(int index)
    {
        Resolution selectedResolution = uniqueResolutions[index];

        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }

    private void SetCurrentResolution()
    {
        Resolution currentResolution = Screen.currentResolution;

        for (int i = 0; i < uniqueResolutions.Count; i++)
        {
            if (uniqueResolutions[i].width == currentResolution.width && uniqueResolutions[i].height == currentResolution.height)
            {
                resolution.value = i;
                break;
            }
        }
    }
}
