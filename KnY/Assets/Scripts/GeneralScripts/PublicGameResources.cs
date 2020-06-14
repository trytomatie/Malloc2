using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicGameResources : MonoBehaviour {
    public GameObject damageObject;
    public GameObject damageFx;
    public GameObject bloodFx;
    public GameObject[] player;
    public Texture2D cursor;
    private static PublicGameResources instance;
    [ColorUsage(true,true)]
    public Color32 blightDamageColor;
    [ColorUsage(true, true)]
    public Color32 afflictionDamageColor;
    public Material[] itemMaterials;
    public Material[] itemDescriptionMaterials;
    public Material[] statusEffectMaterials;
    public Material healingMaterial;
    public Material defenceSpellMaterial;
    public Material poisonMaterial;
    public static int FLOOR_LAYER = -9999;
    public GameObject corruptedAmethystFollower;
    public GameObject gazeFollower;
    public GameObject blackMarble;
    public GameObject itemContextMenuItem;
    public GameObject elusiveDodgeEffect;
	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
	}

    void Start()
    {
        UnityEngine.Cursor.SetCursor(cursor, new Vector2(0, -1), CursorMode.ForceSoftware);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static PublicGameResources GetResource()
    {
        return instance;
    }

    public static void FadeSprite(SpriteRenderer sr,float time,bool destroy)
    {
        instance.GetComponent<PublicGameResources>().FadeSpriteInstanced(sr,time,destroy);
    }

    private void FadeSpriteInstanced(SpriteRenderer sr,float time,bool destroy)
    {
        StartCoroutine(FadeSpriteThread(sr, time));
        if(destroy)
        {
            Destroy(sr.gameObject, time);
        }
    }

    IEnumerator FadeSpriteThread(SpriteRenderer sr, float time)
    {
        Color c = sr.color;
        float baseTime = time;
        while(time > 0)
        {
            sr.color = new Color(c.r, c.g, c.b, time / baseTime);
            time -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

    }

    public static Vector2 CalculateNormalizedDirection(Vector2 origin, Vector2 target)
    {
        float distance = Vector2.Distance(target, origin);
        Vector2 heading = target - origin;
        Vector2 direction = heading / distance;
        return direction;
    }

}
