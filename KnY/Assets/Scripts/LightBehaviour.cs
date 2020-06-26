using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour {
    public enum LightBehaviourEnum {Flame};
    public LightBehaviourEnum behaviour;
    private float initialSize;
    private Light myLight;
    public Texture2D pattern;
    private int patternLine = 0;
    private int patternCollum = 0;
    private int patternLength;
    public float effectMultiplicationBase;
    private int frameCounter = 1; 

	// Use this for initialization

    void Start () {
        myLight = GetComponent<Light>();
        initialSize = myLight.range;
        patternLength = pattern.width;
        patternLine = Random.Range(1, pattern.height);
    }
	
	// Update is called once per frame
	void Update () {
		switch(behaviour)
        {
            case LightBehaviourEnum.Flame:
                if(patternCollum > patternLength)
                {
                    patternCollum = 0;
                }
                if(frameCounter % 4 == 0)
                { 
                myLight.range = initialSize + effectMultiplicationBase * pattern.GetPixel(patternCollum++,patternLine).r;
                }
                break;

        }
        frameCounter++;
        if(frameCounter > 30)
        {
            frameCounter = 1;
        }

    }
}
