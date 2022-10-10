using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioSource _music;


    public float Volume => _music.volume;
    private void Awake()
    {
        LoadValue();
    }
    public void ChangeVolume(float value)
    {
        _music.volume = value;
        SaveValue(value);
    }

    private void SaveValue(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
    }
    private void LoadValue()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            ChangeVolume(PlayerPrefs.GetFloat("Volume"));
        }
        else
            SaveValue(_music.volume);
    }
}
