using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Options
{
    public static int detailedDescriptions = 0;
    public static float musicVolume = 0.2f;
    public static float soundVolume = 0.5f;

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


    public static void ReadOptionsData()
    {
        if (File.Exists(Application.persistentDataPath + "/options.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/options.data", FileMode.Open);
            OptionsData save = (OptionsData)bf.Deserialize(file);
            file.Close();

            detailedDescriptions = save.DetailedDescriptions;
            musicVolume = save.MusicVolume;
            soundVolume = save.SoundVolume;
        }
    }

    public static void SaveCurrent()
    {
        OptionsData save = OptionsData.CreateOptionsData();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/options.data");
        bf.Serialize(file, save);
        file.Close();

    }

}


[System.Serializable]
public class OptionsData
{
    public int DetailedDescriptions { get; set; }
    public float MusicVolume { get; set; }
    public float SoundVolume { get; set; }
    public static OptionsData CreateOptionsData()
    {
        OptionsData save = new OptionsData();
        save.DetailedDescriptions = Options.detailedDescriptions;
        save.MusicVolume = Options.musicVolume;
        save.SoundVolume = Options.soundVolume;

        return save;
    }
}