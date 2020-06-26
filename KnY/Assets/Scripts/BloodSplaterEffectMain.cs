using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplaterEffectMain : MonoBehaviour
{
    public GameObject bloodSplaterEffect;
    public Vector2 velocity;
    public int numberOfInstances = 8;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfInstances; i++)
        {
            GameObject g = Instantiate(bloodSplaterEffect, transform.position, Quaternion.identity, transform);
            g.GetComponent<BloodSplaterEffect>().velocity = velocity +  new Vector2( UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f));
        }
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
