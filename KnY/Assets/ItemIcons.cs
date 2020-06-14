using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIcons : MonoBehaviour
{
    public List<Sprite> icons = new List<Sprite>();
    public List<Sprite> seriesIcons = new List<Sprite>();
    public List<Sprite> skillIcons = new List<Sprite>();
    private static ItemIcons instance;

    public static ItemIcons Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<ItemIcons>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetIcon(int id)
    {
        return icons[id];
    }
    public static Sprite GetIconFromInstance(int id)
    {
        return Instance.icons[id];
    }
    public static Sprite GetSeriesIcon(int id)
    {
        return Instance.seriesIcons[id];
    }
    public static Sprite GetSkillIcon(int id)
    {
        return Instance.skillIcons[id];
    }
}
