using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ajustes : MonoBehaviour
{
    public Slider volume;
    public Slider sensitivity;
    public AudioListener listener;

    public float volumeValue;
    public float sensitivityValue;

    public void Start()
    {
        volume.value = PlayerPrefs.GetFloat("volumeAudio", 0.5f);
        AudioListener.volume = volume.value;
    }


    public void VolumeSlider(float valor)
    {
        volumeValue = valor;
        PlayerPrefs.SetFloat("volumeAudio", volumeValue);
        AudioListener.volume = volume.value;
    }

}
