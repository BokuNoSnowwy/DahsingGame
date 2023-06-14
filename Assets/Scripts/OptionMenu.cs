using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] Slider soundSlider, musicSlider;
    [SerializeField] AudioMixer soundMixer, musicMixer;

    public void SetSoundLevel(float sliderValue)
    {
        soundMixer.SetFloat("Sound", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicLevel(float sliderValue)
    {
        musicMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }

    private void OnEnable()
    {
        float soundValue;
        soundMixer.GetFloat("Sound", out soundValue);
        soundValue = Mathf.Pow(10, (soundValue / 20));
        soundSlider.value = soundValue;

        float musicValue;
        musicMixer.GetFloat("Music", out musicValue);
        musicValue = Mathf.Pow(10, (musicValue / 20));
        musicSlider.value = musicValue;
    }
}
