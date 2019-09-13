using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    //public Text slider;
    public AudioMixer audioMixer;//For the music

    public void SetVolume(float volume)//Sets the volume of the music for the game
    {
        audioMixer.SetFloat("Volume", volume);

        //Debug.Log(volume);
        //slider = GetComponent<Text>();
        //Update(volume);
    }

    public void SetQuality(int qualityCount)//Sets the quality of the game graphics
    {
        QualitySettings.SetQualityLevel(qualityCount);
    }

    public void SetFullscreen(bool setFull)//Makes the screen either fullscreen or not
    {
        Screen.fullScreen = setFull;
    }

    //public void Update(float volume)
    //{
    //    slider.text = Mathf.RoundToInt(volume * 100) + "%";
    //}
}
