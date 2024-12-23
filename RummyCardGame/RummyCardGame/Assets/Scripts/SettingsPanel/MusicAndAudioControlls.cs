using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicAndAudioControlls : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Button musicButton = null;
    [SerializeField] private Button volumeButton = null;
    [SerializeField] private Sprite musicOn = null, musicOff = null, volumeOn = null, volumeOff = null;
    void Start()
    {
        musicButton.onClick.AddListener(MusicButtonClicked);
        volumeButton.onClick.AddListener(VolumeButtonClicked);
        volumeSlider.onValueChanged.AddListener(VolumeAdjuster);
        musicSlider.onValueChanged.AddListener(MusicAdjuster);
    }
    void MusicButtonClicked()
    {
        if (musicButton.GetComponent<Image>().sprite == musicOff)
        {
            musicButton.GetComponent<Image>().sprite = musicOn;
            musicSlider.value = 1;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicOff;
            musicSlider.value = 0;
        }
    }
    void VolumeButtonClicked()
    {
        if (volumeButton.GetComponent<Image>().sprite == volumeOff)
        {
            volumeButton.GetComponent<Image>().sprite = volumeOn;
            volumeSlider.value = 1;
        }
        else
        {
            volumeButton.GetComponent<Image>().sprite = volumeOff;
            volumeSlider.value = 0;
        }
    }
    void VolumeAdjuster(float value)
    {
        if (value == 0)
            volumeButton.GetComponent<Image>().sprite = volumeOff;
else
            volumeButton.GetComponent<Image>().sprite = volumeOn;
    }
    void MusicAdjuster(float value)
    {
        if (value == 0)
            musicButton.GetComponent<Image>().sprite = musicOff;
        else
            musicButton.GetComponent<Image>().sprite = musicOn;
    }
}
