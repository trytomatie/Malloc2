using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour {

    private static Director instance;
    public GameObject damageText;
    public GameObject canvas;
    public GameObject cursor;
    public GameObject miniHpBar;

    public float timeScale = 1;
    public System.Random globalRandom;
    public int globalRandomSeed = 1337;

    private IEnumerator darkenLevel_coroutine;
    private Material currentFadeMaterial;
    private bool endFadeInstantly = false;
	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            instance = this;
        }
        globalRandom = new System.Random(globalRandomSeed);
        Statusmanager.enemyFactionEntities = new List<GameObject>();
        Statusmanager.playerFactionEntities = new List<GameObject>();
        if(canvas == null)
        {
            canvas = GameObject.Find("Canvas");
        }
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
        GameObject textObject = Instantiate(damageText, canvas.transform);
        textObject.transform.GetChild(0).GetComponent<Text>().text = text;
        textObject.transform.GetChild(0).GetComponent<DamageTextAnimation>().origin = position.position;

    }

    public void SpawnDamageText(string text, Transform position,Color color)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position.position);
        GameObject textObject = Instantiate(damageText, canvas.transform);
        textObject.transform.GetChild(0).GetComponent<Text>().text = text;
        textObject.transform.GetChild(0).GetComponent<Text>().color = color;
        textObject.transform.GetChild(0).GetComponent<DamageTextAnimation>().origin = position.position;

    }
    public void SpawnDamageText(string text, Transform position, Color color,bool crit)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position.position);
        GameObject textObject = Instantiate(damageText, canvas.transform);
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
        GameObject textObject = Instantiate(damageText, canvas.transform);
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
        GameObject hpBar = Instantiate(miniHpBar, canvas.transform);
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
                    SpriteRenderer m = go.GetComponent<SpriteRenderer>();
                    float mTime = (time) / (baseTime +destinationColor) + destinationColor;
                    Color c = m.color;
                    m.color = new Color(mTime, mTime, mTime, 1);
                }
            }
            time -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetFadeMaterial(float fadeStart, float fadeEnd, Material material)
    {
        if(currentFadeMaterial == material)
        {
            endFadeInstantly = true;
        }
        StartCoroutine(SetFadeMaterialCoroutine(fadeStart, fadeEnd, material));
    }
    IEnumerator SetFadeMaterialCoroutine(float fadeStart, float fadeEnd, Material material)
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
            value += Time.fixedDeltaTime * mod;
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
                SpriteRenderer m = go.GetComponent<SpriteRenderer>();
                m.color = Color.white;
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
}
