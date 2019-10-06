using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] Slider m_masterVolume = null; 
    [SerializeField] Slider m_musicVolume = null; 
    [SerializeField] Slider m_SFXVolume = null; 
    [SerializeField] Slider m_sensativitySlider = null;
    [SerializeField] TMP_InputField m_sensativityValue = null;

    public OptionsData m_optionsData = new OptionsData();


    void Start()
    {
        Options.Load();
        m_optionsData.GetData();
        m_masterVolume.value = Options.MasterVolume;
        m_musicVolume.value = Options.MusicVolume;
        m_SFXVolume.value = Options.SFXVolume;
        m_sensativitySlider.value = Options.MouseSensativity * 10000;
        ValidateSlider();
    }


    public void ValidateInput()
    {
        m_sensativitySlider.value = int.Parse(m_sensativityValue.text);
    }

    public void ValidateSlider()
    {
        m_sensativityValue.text = Mathf.RoundToInt(m_sensativitySlider.value).ToString();
    }

    public void Confirm()
    {
        m_optionsData.soundData[0] = m_masterVolume.value;
        m_optionsData.soundData[1] = m_musicVolume.value;
        m_optionsData.soundData[2] = m_SFXVolume.value;
        m_optionsData.mouseSensativity = m_sensativitySlider.value / 10000;
        m_optionsData.SetData();
        Options.Save();
    }
}