using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class Options
{
    public static int detailedDescriptions = 0;
    public static float musicVolume = 0;


    public static void RestorePlayerPrefs()
    {
        //Debug.Log(PlayerPrefs.GetFloat("musicVolume"));
        detailedDescriptions = PlayerPrefs.GetInt("detailedDescriptions");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");

    }

    public static void ApplyMusicSettings()
    {
        AudioSource[] allAudioSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach(AudioSource audioSource in allAudioSources)
        {
            if(audioSource.priority == 128)
            {
                audioSource.volume = musicVolume;
            }
        }
    }

    public static void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("detailedDescriptions", detailedDescriptions);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        Debug.Log(PlayerPrefs.GetFloat("musicVolume"));
    }

}