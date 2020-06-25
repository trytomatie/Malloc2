using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OptionsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider volumeSlider;
    public Toggle descriptionToggle;

    public void Start()
    {
        SetOptionsValues();
    }
    public void OnEnable()
    {
        SetOptionsValues();

    }

    private void SetOptionsValues()
    {
        musicSlider.value = Options.musicVolume;
        volumeSlider.value = Options.soundVolume;
        if (Options.detailedDescriptions == 0)
        {
            descriptionToggle.isOn = false;
        }
        else
        {
            descriptionToggle.isOn = true;
        }
    }

    public void SetMusicVolume(Slider slider)
    {
        Director.SetMusicVolume(slider);
    }
    public void SetSoundVolume(Slider slider)
    {
        Director.SetSoundVolume(slider);
    }
    public void SetDetailedDescription(Toggle toggle)
    {
        Director.SetDetailedDescription(toggle);
    }
}
