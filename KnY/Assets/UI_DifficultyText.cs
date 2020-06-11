using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DifficultyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int difficulty = Director.GetInstance().difficultyScaling;
        switch(difficulty)
        {
            case 0:
                GetComponent<Text>().text = "Difficutly: Easy";
                break;
            case 1:
                GetComponent<Text>().text = "Difficutly: Normal";
                break;
            case 2:
                GetComponent<Text>().text = "Difficutly: Hard";
                break;
        }
    }

}
