using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Director : MonoBehaviour {

    private static Director instance;
    public GameObject damageText;
    private GameObject canvas;
    public GameObject cursor;
    public GameObject miniHpBar;
    public GameObject groundAoeIndicator;
    public GameObject laserFx;

    public float timeScale = 1;
    public System.Random globalRandom;
    public int difficultyScaling = 2;
    public Statusmanager.CharacterClass characterClass = Statusmanager.CharacterClass.Undefined;
    

    public static int globalRandomSeed = 1337;
    public bool isMobile;

    private IEnumerator darkenLevel_coroutine;
    private Material currentFadeMaterial;
    private bool endFadeInstantly = false;


    public static string damageColorText = "<Color=orange>";
    public static string variableColorText = "<Color=blue>";
    public static string colorEndText = "</Color>";

    public TimeSpan timePassed = new TimeSpan(0, 0, 0, 0, 0);

    public GameObject Canvas
    {
        get
        {
            if (canvas == null)
            {

                Canvas = GameObject.Find("Canvas");
            }
            return canvas;
        }

        set
        {
            canvas = value;
        }
    }

    // Use this for initialization
    void Awake () {
		if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Api.ReadSaveData();
        Api.ResetCurrent();
        globalRandom = new System.Random();
        Statusmanager.EnemyFactionEntities = new List<GameObject>();
        Statusmanager.PlayerFactionEntities = new List<GameObject>();
        Physics.IgnoreLayerCollision(12, 0);
        Physics.IgnoreLayerCollision(12, 12);
        Options.RestorePlayerPrefs();

    }

    private void OnLevelWasLoaded(int level)
    {
        if((level == 3 || level == 1 ) && isMobile)
        {
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);

        }
        Options.ApplyMusicSettings();
        print("I did shit");
    }

    // Update is called once per frame
    void Update () {
        Animator[] anims = GameObject.FindObjectsOfType<Animator>();
		foreach(Animator a in anims)
        {
            float bonusSpeed = 0;
            try
            {
                bonusSpeed =  a.GetFloat("SpeedIncrease");
            }
            catch
            {

            }
            a.speed = timeScale + bonusSpeed;
        }
        timePassed += new TimeSpan(0, 0, 0, 0, (int)(Time.deltaTime *1000));
	}

    public static Director GetInstance()
    {
        return instance;
    }

    public static float TimeScale()
    {
        return GetInstance().timeScale;
    }
    public static float TimeScale(float value)
    {
        GetInstance().timeScale = value;
        return GetInstance().timeScale;
    }

    public void SpawnDamageText(string text,Transform position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position.position);
        GameObject textObject = Instantiate(damageText, Canvas.transform);
        textObject.transform.GetChild(0).GetComponent<Text>().text = text;
        textObject.transform.GetChild(0).GetComponent<DamageTextAnimation>().origin = position.position;

    }

    public void SpawnDamageText(string text, Transform position,Color color)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position.position);
        GameObject textObject = Instantiate(damageText, Canvas.transform);
        textObject.transform.GetChild(0).GetComponent<Text>().text = text;
        textObject.transform.GetChild(0).GetComponent<Text>().color = color;
        textObject.transform.GetChild(0).GetComponent<DamageTextAnimation>().origin = position.position;

    }
    public void SpawnDamageText(string text, Transform position, Color color,bool crit)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position.position);
        GameObject textObject = Instantiate(damageText, Canvas.transform);
        textObject.transform.GetChild(0).GetComponent<Text>().text = text;
        textObject.transform.GetChild(0).GetComponent<Text>().color = color;
        textObject.transform.GetChild(0).GetComponent<DamageTextAnimation>().origin = position.position;
        if (crit)
        {
            textObject.transform.GetChild(0).GetComponent<Text>().text += "!";
            textObject.transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Bold;
        }

    }
    public void SpawnDamageText(string text, Transform position, Color color, bool crit, Vector2 direction)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position.position);
        GameObject textObject = Instantiate(damageText, Canvas.transform);
        textObject.transform.GetChild(0).GetComponent<Text>().text = text;
        textObject.transform.GetChild(0).GetComponent<Text>().color = color;
        textObject.transform.GetChild(0).GetComponent<DamageTextAnimation>().origin = position.position;
        if (crit)
        {
            textObject.transform.GetChild(0).GetComponent<Text>().text += "!";
            textObject.transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Bold;
        }

    }

    public GameObject SpawnMiniHpBar(Statusmanager statusmanager, float time, float heightOffset)
    {
        GameObject hpBar = Instantiate(miniHpBar, Canvas.transform);
        hpBar.GetComponent<UI_MiniHpBarManager>().heigthOffset = heightOffset;
        hpBar.GetComponent<UI_MiniHpBarManager>().timer = time;
        hpBar.GetComponent<UI_MiniHpBarManager>().statusmanager = statusmanager;
        return hpBar;

    }
    public void DarkenLevel(float time)
    {
        darkenLevel_coroutine = DarkenLevelThread(time);
        StartCoroutine(darkenLevel_coroutine);
    }
    IEnumerator DarkenLevelThread(float time)
    {
        GameObject[] bgObjects = GameObject.FindGameObjectsWithTag("Background");
        GameObject[] fgObjects = GameObject.FindGameObjectsWithTag("Foreground");
        List<GameObject> toDarkenObjects = new List<GameObject>();
        toDarkenObjects.AddRange(bgObjects);
        toDarkenObjects.AddRange(fgObjects);
        float destinationColor = 0.5f;
        float baseTime = time;
        while (time > 0)
        { 
            foreach(GameObject go in toDarkenObjects)
            {
                if(go != null)
                {
                    if (go.GetComponent<SpriteRenderer>() == null && go.GetComponent<TilemapRenderer>() != null)
                    {
                        TilemapRenderer tmr = go.GetComponent<TilemapRenderer>();
                        float mTime = (time) / (baseTime + destinationColor) + destinationColor;
                        Color c = tmr.material.color;
                        tmr.material.color = new Color(mTime, mTime, mTime, 1);
                    }
                    else
                    { 
                    SpriteRenderer m = go.GetComponent<SpriteRenderer>();
                    float mTime = (time) / (baseTime +destinationColor) + destinationColor;
                    Color c = m.color;
                    m.color = new Color(mTime, mTime, mTime, 1);
                    }
                }
            }
            time -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetFadeMaterial(float fadeStart, float fadeEnd, Material material, float time)
    {
        if(currentFadeMaterial == material)
        {
            endFadeInstantly = true;
        }
        StartCoroutine(SetFadeMaterialCoroutine(fadeStart, fadeEnd, material,time));
    }
    IEnumerator SetFadeMaterialCoroutine(float fadeStart, float fadeEnd, Material material,float time)
    {
        while(endFadeInstantly)
        {
            yield return new WaitForFixedUpdate();
        }
        endFadeInstantly = false;
        currentFadeMaterial = material;
        material.SetFloat("_fade",fadeStart);
        int mod = 1;
        float value = fadeStart;
        int timeout = 0;
        if (fadeStart > fadeEnd)
        {
            mod = -1;
        }
        while(material.GetFloat("_fade") != fadeEnd && timeout < 1000)
        {
            yield return new WaitForFixedUpdate();
            value += Time.fixedDeltaTime * mod * time;
            if((value < fadeEnd && mod == -1 )||(value > fadeEnd && mod == 1) || endFadeInstantly)
            {
                value = fadeEnd;
            }
            material.SetFloat("_fade", value);
            timeout++;
        }
        endFadeInstantly = false;
        currentFadeMaterial = null;
    }
    public void BrightenLevel()
    {
        StopCoroutine(darkenLevel_coroutine);
        GameObject[] bgObjects = GameObject.FindGameObjectsWithTag("Background");
        GameObject[] fgObjects = GameObject.FindGameObjectsWithTag("Foreground");
        List<GameObject> toBrightenObjects = new List<GameObject>();
        toBrightenObjects.AddRange(bgObjects);
        toBrightenObjects.AddRange(fgObjects);
        foreach (GameObject go in toBrightenObjects)
        {
            if (go != null)
            {
                if (go.GetComponent<SpriteRenderer>() == null && go.GetComponent<TilemapRenderer>() != null)
                {
                    TilemapRenderer tmr = go.GetComponent<TilemapRenderer>();
                    tmr.material.color = Color.white;
                }
                else
                { 
                    SpriteRenderer m = go.GetComponent<SpriteRenderer>();
                m.color = Color.white;
                }
            }
        }
    }

    public static float RoundToGrid(float number)
    {
        double dnumber = Math.Round((double)number, 2);
        return (float)dnumber;
    }

    public static void SetTimeScale(float number)
    {
        Time.timeScale = number;
        Time.fixedDeltaTime = number * 0.02f;
    }

    public static void SetDifficulty()
    {
        int i = GameObject.Find("DifficultyDropDown").GetComponent<Dropdown>().value;
        GetInstance().difficultyScaling = i;
    }

    public static void SetContact(int i)
    {
        GetInstance().characterClass = (Statusmanager.CharacterClass)i;
    }

    public static void SetGlobalSeed()
    {
        string seedString = GameObject.Find("SeedInputField").GetComponent<InputField>().text;
        int seed = 0;
        if(Int32.TryParse(seedString, out seed))
        {
            globalRandomSeed = seed;
        }
        else
        {
            globalRandomSeed = UnityEngine.Random.Range(0, 10000);
        }
    }

    public static Vector2 RotateDirectionalVector(Vector2 direction,float angle)
    {
        float x = direction.x * Mathf.Cos(angle) + direction.y * Mathf.Sin(angle);
        float y = -direction.x * Mathf.Sin(angle) + direction.y * Mathf.Cos(angle);
        return new Vector2(x, y);
    }


    #region Options
    public static void SetMusicVolume(Slider slider)
    {
        Options.musicVolume = slider.value;
        Options.SavePlayerPrefs();
    }

    public static void SetDetailedDescription(Toggle toggle)
    {
        if (toggle.isOn)
        {
            Options.detailedDescriptions = 1;
        }
        else
        {
            Options.detailedDescriptions = 0;
        }
        Options.SavePlayerPrefs();

    }
    #endregion
}
