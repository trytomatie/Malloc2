using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitleManager : MonoBehaviour
{
    public List<Material> materials = new List<Material>();
    private bool animate = false;
    private float fade = 1;
    private int mod = 1;
    private float timer = 0;

    public Text levelName;
    public Text levelDescription;

    private static List<UI_TitleManager> instances = new List<UI_TitleManager>();
    // Start is called before the first frame update
    void Start()
    {
        instances.RemoveAll(item => item == null);
        instances.Add(this);
        foreach (Material material in materials)
        {
            material.SetFloat("_Fade", 0);
        }
        Show(3);
    }
    



    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            fade += mod * Time.deltaTime * 1.2f;
            if (fade >= 1 || fade <= 0)
            {
                animate = false;
            }
            foreach (Material material in materials)
            {
                material.SetFloat("_Fade", fade);
            }
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            Hide();
            timer = 0;
        }
    }

    public void Show()
    {
        animate = true;
        fade = 0;
        mod = 1;
    }

    public void Show(float duration)
    {
        animate = true;
        fade = 0;
        timer = duration;
        mod = 1;
    }

    public void Hide()
    {
        animate = true;
        fade = 1;
        mod = -1;
    }

    public void SetTitleProperties(string name, string description)
    {
        levelName.text = name;
        levelDescription.text = description;
    }

    public static void Show(string title, string desc, float duration)
    {
        foreach(UI_TitleManager instance in instances)
        {
            instance.levelName.text = title;
            instance.levelDescription.text = desc;
            instance.animate = true;
            instance.fade = 0;
            instance.timer = duration;
            instance.mod = 1;
        }
    }
}
