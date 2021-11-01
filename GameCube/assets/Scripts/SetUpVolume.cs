using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetUpVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public static float getMusicSliderValue;
    public static float getSFXSliderValue;

    void Start()
    {
        getMusicSliderValue = 0.5f;
        getSFXSliderValue = 0.7f;
    }

    void Update()
    {
        // Get the volume of the Music in case it is changed by the user
        getMusicSliderValue = GameObject.Find("SliderMusic").GetComponent<Slider>().value;
        //Debug.Log("Music Slider Value = " + getMusicSliderValue);

        // Get the volume of the SFX in case it is changed by the user
        getSFXSliderValue = GameObject.Find("SliderSFX").GetComponent<Slider>().value;
        //Debug.Log("SFX Slider Value = " + getSFXSliderValue);
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }

}
