using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OptionsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Toggle descriptionToggle;
    public void OnEnable()
    {
        musicSlider.value = Options.musicVolume;
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
    public void SetDetailedDescription(Toggle toggle)
    {
        Director.SetDetailedDescription(toggle);
    }
}
