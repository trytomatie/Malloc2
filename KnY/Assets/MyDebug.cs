using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyDebug : MonoBehaviour {
    public GameObject targetDummy;
    public GameObject chest;
    public GameObject enemy2;
    public Material playerEffects;
    [ColorUsage(true, true)]
    public Color color1;
    [ColorUsage(true, true)]
    public Color color2;
    private bool coloring = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(targetDummy, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Instantiate(enemy2, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale += 0.1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale -= 0.1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(chest, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            FindObjectOfType<PlayerController>().GetComponent<Statusmanager>().Mana += 10000;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if(coloring)
            {
                print("yay");
                playerEffects.SetColor("_color", color1);
                coloring = !coloring;
            }
            else
            {
                print("nay");
                playerEffects.SetColor("_color", color2);
                coloring = !coloring;
            }
        }
    }
}
