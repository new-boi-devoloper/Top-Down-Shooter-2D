using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{

    public Slider slider;

    public AudioClip clip;
    public AudioSource newAudio;
    void Start()
    {
        newAudio.volume = 100;
    }

    void Update() 
    {
        newAudio.volume = slider.value;
    }
}

