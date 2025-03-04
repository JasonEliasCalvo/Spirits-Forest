using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicQuality : MonoBehaviour
{
    public TMP_Dropdown dropdownQuality;
    public int quality;

    void Start()
    {
        quality = PlayerPrefs.GetInt("ActualQuality", 3);
        dropdownQuality.value = quality;
        ChangeQuality();
    }

    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(dropdownQuality.value);
        PlayerPrefs.SetInt("ActualQuality", dropdownQuality.value);
        quality = dropdownQuality.value;
    }
}
