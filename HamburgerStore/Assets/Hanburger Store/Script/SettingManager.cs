using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _sound_img;
    [SerializeField] private Slider _slider;
    [SerializeField] private Toggle _toggle;

    private float originSound;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            _slider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            _slider.value = 1f;
            PlayerPrefs.SetFloat("Volume", _slider.value);
        }
            
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("Volume", _slider.value);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        SliderToToggle();
        AudioListener.volume = _slider.value;
    }

    private void SliderToToggle()
    {
        if (_slider.value == 0)
        {
            _toggle.isOn = false;
            _toggle.targetGraphic.GetComponent<Image>().sprite = _sound_img[0];
        }
        else
        {
            _toggle.isOn = true;
            _toggle.targetGraphic.GetComponent<Image>().sprite = _sound_img[1];
        }
    }

    public void ToggleToSlider()
    {
        if (_toggle.isOn)
            _slider.value = originSound;
        else
        {
            originSound = _slider.value;
            _slider.value = 0;
        }
    }
}
