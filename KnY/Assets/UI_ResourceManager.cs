﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ResourceManager : MonoBehaviour
{
    public Text hpText;
    public Text manaText;
    public Text levelText;
    public Statusmanager statusmanager;
    public Image hpBar;
    private static UI_ResourceManager instance;

    public static UI_ResourceManager Instance
    {
        get
        {
            if(instance == null)
            {
                Instance = GameObject.FindObjectOfType<UI_ResourceManager>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateUI()
    {
        UpdateUpBar();
        Instance.hpText.text = "" + Instance.statusmanager.Hp;
        Instance.manaText.text = "" + Instance.statusmanager.Mana;
        Instance.levelText.text = "Level: " + Instance.statusmanager.level;
    }

    private static void UpdateUpBar()
    {
        float hpPercent = (float)Instance.statusmanager.Hp / (float)Instance.statusmanager.maxHp;
        Instance.hpBar.GetComponent<Image>().fillAmount = hpPercent;
    }

}