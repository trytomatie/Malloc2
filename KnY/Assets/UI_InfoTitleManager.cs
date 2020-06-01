using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InfoTitleManager : MonoBehaviour
{
    public List<Material> materials = new List<Material>();
    private bool animate = false;
    private float fade = 1;
    private int mod = 1;
    private float timer = 0;

    public Text titleText;
    public Text descriptionText;
    public static UI_InfoTitleManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        foreach (Material material in materials)
        {
            material.SetFloat("_Fade", 0);
        }
    }




    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            print(fade);
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

    public static void Show()
    {
        instance.animate = true;
        instance.fade = 0;
        instance.mod = 1;
    }

    public static void Show(string title,string description,float durration)
    {
        instance.titleText.text = title;
        instance.descriptionText.text = description;
        instance.animate = true;
        instance.fade = 0.2f;
        instance.timer = durration;
        instance.mod = 1;
    }

    public static void Hide()
    {
        instance.animate = true;
        instance.fade = 1;
        instance.mod = -1;
    }
}
