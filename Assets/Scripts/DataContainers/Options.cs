using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class Options
{
    [Range(0.0f, 1.0f)] public static float MasterVolume = 1.0f;
    [Range(0.0f, 1.0f)] public static float MusicVolume = 1.0f;
    [Range(0.0f, 1.0f)] public static float SFXVolume = 1.0f;

    [Range(0.0f, 0.5f)] public static float MouseSensativity = 0.2f;

    public static void Save()
    {
        SaveSystem.SaveOptions(new OptionsData());
    }

    public static void Load()
    {
        OptionsData op = SaveSystem.LoadOptions();
        if (op == null)
        {
            Save();
        }
        else
        {
            op.SetData();
        }
    }

    public static void Reset()
    {
        MasterVolume = 1.0f;
        MusicVolume = 1.0f;
        SFXVolume = 1.0f;
        MouseSensativity = 0.2f;
    }
}

[System.Serializable]
public class OptionsData
{
    [Range(0.0f, 1.0f)] public float[] soundData = new float[3];
    [Range(0.0f, 0.5f)] public float mouseSensativity = 0.0f;

    public OptionsData()
    {
        soundData[0] = Options.MasterVolume;
        soundData[1] = Options.MusicVolume;
        soundData[2] = Options.SFXVolume;
        mouseSensativity = Options.MouseSensativity;
    }

    public void GetData()
    {
        soundData[0] = Options.MasterVolume;
        soundData[1] = Options.MusicVolume;
        soundData[2] = Options.SFXVolume;
        mouseSensativity = Options.MouseSensativity;
    }

    public void SetData()
    {
        Options.MasterVolume = soundData[0];
        Options.MusicVolume = soundData[1];
        Options.SFXVolume = soundData[2];
        Options.MouseSensativity = mouseSensativity;
    }
}