using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Director : MonoBehaviour {

    private static Director instance;
    public GameObject damageText;
    private GameObject canvas;
    public GameObject cursor;
    public GameObject miniHpBar;
    public GameObject statusEffectIndicator;
    public GameObject groundAoeIndicator;
    public GameObject laserFx;
    public GameObject sound;

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
    public static NumberFormatInfo numberFormat = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = ".", CurrencySymbol = "" };

    public TimeSpan timePassed = new TimeSpan(0, 0, 0, 0, 0);


    private static MapGenerator mapGenerator;


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
        Options.ReadOptionsData();

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


    /// <summary>
    /// Spawns damage text
    /// </summary>
    /// <param name="text"></param>
    /// <param name="position"></param>
    public void SpawnDamageText(string text,Transform position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position.position);
        GameObject textObject = Instantiate(damageText, Canvas.transform);
        textObject.transform.GetChild(0).GetComponent<Text>().text = text;
        textObject.transform.GetChild(0).GetComponent<DamageTextAnimation>().origin = position.position;

    }


    /// <summary>
    /// Spawns damage text
    /// </summary>
    /// <param name="text"></param>
    /// <param name="position"></param>
    /// <param name="color"></param>
    public void SpawnDamageText(string text, Transform position,Color color)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position.position);
        GameObject textObject = Instantiate(damageText, Canvas.transform);
        textObject.transform.GetChild(0).GetComponent<Text>().text = text;
        textObject.transform.GetChild(0).GetComponent<Text>().color = color;
        textObject.transform.GetChild(0).GetComponent<DamageTextAnimation>().origin = position.position;

    }


    /// <summary>
    /// Spawns damage text
    /// </summary>
    /// <param name="text"></param>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="crit"></param>
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


    /// <summary>
    /// Spawns damage text
    /// </summary>
    /// <param name="text"></param>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="crit"></param>
    /// <param name="direction"></param>
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


    /// <summary>
    /// Spawns an mini-hpbar ui element
    /// </summary>
    /// <param name="statusmanager"></param>
    /// <param name="time"></param>
    /// <param name="heightOffset"></param>
    /// <returns></returns>
    public GameObject SpawnMiniHpBar(Statusmanager statusmanager, float time, float heightOffset)
    {
        GameObject hpBar = Instantiate(miniHpBar, Canvas.transform);
        hpBar.GetComponent<UI_MiniHpBarManager>().heigthOffset = heightOffset;
        hpBar.GetComponent<UI_MiniHpBarManager>().timer = time;
        hpBar.GetComponent<UI_MiniHpBarManager>().statusmanager = statusmanager;
        return hpBar;

    }

    /// <summary>
    /// Darken level in a seperate thread
    /// </summary>
    /// <param name="time"></param>
    public void DarkenLevel(float time)
    {
        darkenLevel_coroutine = DarkenLevelThread(time);
        StartCoroutine(darkenLevel_coroutine);
    }


    /// <summary>
    /// Darkens the level
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Fades a material in a seperate thread
    /// </summary>
    /// <param name="fadeStart"></param>
    /// <param name="fadeEnd"></param>
    /// <param name="material"></param>
    /// <param name="time"></param>
    public void SetFadeMaterial(float fadeStart, float fadeEnd, Material material, float time)
    {
        if(currentFadeMaterial == material)
        {
            endFadeInstantly = true;
        }
        StartCoroutine(SetFadeMaterialCoroutine(fadeStart, fadeEnd, material,time));
    }

    /// <summary>
    /// Thread to fade material
    /// </summary>
    /// <param name="fadeStart"></param>
    /// <param name="fadeEnd"></param>
    /// <param name="material"></param>
    /// <param name="time"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Brightens the level
    /// </summary>
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


    /// <summary>
    /// Sets the timescale
    /// </summary>
    /// <param name="number"></param>
    public static void SetTimeScale(float number)
    {
        Time.timeScale = number;
        Time.fixedDeltaTime = number * 0.02f;
    }


    /// <summary>
    /// Sets the difficulty (MainMenu)
    /// </summary>
    public static void SetDifficulty()
    {
        int i = GameObject.Find("DifficultyDropDown").GetComponent<Dropdown>().value;
        GetInstance().difficultyScaling = i;
    }


    /// <summary>
    /// Sets the contract (MainMenu)
    /// </summary>
    /// <param name="i"></param>
    public static void SetContact(int i)
    {
        GetInstance().characterClass = (Statusmanager.CharacterClass)i;
    }


    /// <summary>
    /// Sets the global seed
    /// </summary>
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



    /// <summary>
    /// Creates a sound
    /// </summary>
    /// <param name="source"></param>
    /// <param name="soundId"></param>
    /// <param name="pitchOffset"></param>
    public static void CreateSound(GameObject source,int soundId,float pitchOffset)
    {
        GameObject soundObject = Instantiate(instance.sound, source.transform.position, Quaternion.identity);
        AudioClip clip = PublicGameResources.GetResource().sounds[soundId];
        soundObject.GetComponent<AudioSource>().volume = Options.soundVolume;
        soundObject.GetComponent<AudioSource>().pitch += pitchOffset + UnityEngine.Random.Range(-0.05f, 0.05f);
        soundObject.GetComponent<AudioSource>().clip = clip;
        soundObject.GetComponent<AudioSource>().Play();
        Destroy(soundObject, clip.length + 0.2f);

    }


    public static MapGenerator MapGenerator
    {
       get
       {
            if(mapGenerator == null)
            {
                mapGenerator = GameObject.FindObjectOfType<MapGenerator>();
            }
            return mapGenerator;
       }
    }

    #region Options
    public static void SetMusicVolume(Slider slider)
    {
        Options.musicVolume = slider.value;
        Options.SaveCurrent();
    }

    public static void SetSoundVolume(Slider slider)
    {
        Options.soundVolume = slider.value;
        Options.SaveCurrent();
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
        Options.SaveCurrent();

    }
    #endregion
}
