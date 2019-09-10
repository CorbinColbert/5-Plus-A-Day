using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    //public Text slider;
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);

        //Debug.Log(volume);
        //slider = GetComponent<Text>();
        //Update(volume);
    }

    public void SetQuality(int qualityCount)
    {
        QualitySettings.SetQualityLevel(qualityCount);
    }

    public void SetFullscreen(bool setFull)
    {
        Screen.fullScreen = setFull;
    }

    //public void Update(float volume)
    //{
    //    slider.text = Mathf.RoundToInt(volume * 100) + "%";
    //}
}
