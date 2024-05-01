using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMusic : MonoBehaviour
{
    public Slider _slider;

    public enum TypeAudio
    {
        Music,
        Sound
    }

    public TypeAudio _audio;

    private void OnEnable()
    {
        if (_audio == TypeAudio.Music)
        {
            _slider.value = GameManager.Instance.LoadMusic();
        }
        else
        {
            _slider.value = GameManager.Instance.LoadSound();
        }
    }

    public void SetAudioGame()
    {
        if (_audio == TypeAudio.Music)
        {
            GameManager.Instance.SaveMusic(_slider.value);
        }
        else
        {
            GameManager.Instance.SaveSound(_slider.value);
        }
    }
}
