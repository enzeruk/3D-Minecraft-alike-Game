using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UpdateVolume : MonoBehaviour
{
    // FPS contains 2 Audio Source components
    private AudioSource[] sources;
    private float setSFXVol;
    private float setMusicVol;

    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponents<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(SetUpVolume.getSFXSliderValue != 0 && SetUpVolume.getMusicSliderValue != 0)
        {
            setSFXVol = SetUpVolume.getSFXSliderValue;
            sources[0].volume = setSFXVol;     // @ FPS: 1st Audio Source component is the SFX
            Debug.Log("SFX vol @ FPS = " + sources[0].volume);

            setMusicVol = SetUpVolume.getMusicSliderValue;
            sources[1].volume = setMusicVol;   // @ FPS: 2d Audio Source component is the background Music
            Debug.Log("Music vol @ FPS = " + sources[1].volume);
        }
    }

}
