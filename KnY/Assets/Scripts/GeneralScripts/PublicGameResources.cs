using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicGameResources : MonoBehaviour {
    public GameObject damageObject;
    public GameObject bloodFx;
    public GameObject[] player;
    public Texture2D cursor;
    private static PublicGameResources instance;
    public Color32 BlightDamageColor;
    public Material[] itemMaterials;
    public Material[] itemDescriptionMaterials;
    public static int FLOOR_LAYER = -9999;
    public GameObject corruptedAmethystFollower;
	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            instance = this;
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


}
