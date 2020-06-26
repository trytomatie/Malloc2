using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLogbook : MonoBehaviour
{


    public GameObject button;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text mainText;
    public Image sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateLogbookDataToCanvas(LogbookData_ScriptableObject logbookData)
    {
        text1.text = logbookData.text1;
        text2.text = logbookData.text2;
        text3.text = logbookData.text3;
        mainText.text = logbookData.mainText;
        sprite.sprite = logbookData.sprite;
    }
    
   
}
