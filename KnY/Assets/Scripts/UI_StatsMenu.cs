using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatsMenu : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        SetStatsText();
    }

    private void OnEnable()
    {
        SetStatsText();
    }

    private void SetStatsText()
    {
        text.text = string.Format("Total kills: {0}\n", Api.TotalKills.ToString("C0", Director.numberFormat));
        text.text += string.Format("Total damage done: {0}\n", Api.TotalDamageDone.ToString("C0", Director.numberFormat));
        text.text += string.Format("Total damage taken: {0}\n", Api.TotalDamageTaken.ToString("C0",Director.numberFormat));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
