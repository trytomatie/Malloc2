
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ResourceManager : MonoBehaviour
{
    public Text hpText;
    public Text spText;
    public Text manaText;
    public Text levelText;
    private Statusmanager statusmanager;
    public Image hpBar;
    public Image spBar;
    public Image expBar;
    private static UI_ResourceManager instance;
    public Image hpLowEffect;

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

    public Statusmanager Statusmanager
    {
        get
        {
            if(statusmanager == null)
            {
                statusmanager = FindObjectOfType<PlayerController>().GetComponent<Statusmanager>();
            }
            return statusmanager;
        }

        set
        {
            statusmanager = value;
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
        UpdateSpBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateUI()
    {
        UpdateHpBar();
        Instance.manaText.text = "" + Instance.Statusmanager.Mana;
        Instance.levelText.text = "Level: " + Instance.Statusmanager.level;
        float expPercent = (float)Instance.Statusmanager.Experinece / (float)Instance.Statusmanager.maxExperience;
        Instance.expBar.GetComponent<Image>().fillAmount = expPercent;
    }

    public static void UpdateHpBar()
    {
        string barrier = "";
        if (Instance.Statusmanager.Barrier > 0)
        {
            barrier = "+(" + Instance.statusmanager.Barrier + ")";
        }
        Instance.hpText.text = "" + Instance.Statusmanager.Hp + barrier;

        float hpPercent = (float)Instance.Statusmanager.Hp / (float)Instance.Statusmanager.TotalMaxHp;
        Instance.hpBar.GetComponent<Image>().fillAmount = hpPercent;
        if(Instance.hpLowEffect != null)
        {
            Material material = Instance.hpLowEffect.GetComponent<Image>().material;
            if (hpPercent < 0.40f)
            {
                material.SetFloat("_isVisible", 1);
            }
            else
            {
                material.SetFloat("_isVisible", 0);
            }
        }
        
    }
    public static void UpdateSpBar()
    {
        Instance.spText.text = "" + Instance.Statusmanager.Sp;

        float spPercent = (float)Instance.Statusmanager.Sp / (float)Instance.Statusmanager.maxSp;
        Instance.spBar.GetComponent<Image>().fillAmount = spPercent;
    }

}
