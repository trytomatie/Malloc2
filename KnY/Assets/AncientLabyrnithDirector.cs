using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Director-class for the Ancien-Labyrinth gamemode
/// </summary>
public class AncientLabyrnithDirector : MonoBehaviour
{
    private int difficulty;
    public static float kingsContractScaling = 0.75f;
    private static float playerBonusRegen = 10;
    private static int playerBonusFlatDamageReduction = 25;
    private static int playerBonusDamage = 10;

    public int Difficulty
    {
        get
        {

            return difficulty;
        }

        set
        {
            difficulty = value;
            if (difficulty == 0)
            {
                kingsContractScaling = 0.5f;
                playerBonusRegen = 25;
                playerBonusFlatDamageReduction = 25;
                playerBonusDamage = 10;
            }
            if (difficulty == 1)
            {
                kingsContractScaling = 0.75f;
                playerBonusRegen = 10;
                playerBonusFlatDamageReduction = 10;
                playerBonusDamage = 20;
            }
            if (difficulty == 2)
            {
                kingsContractScaling = 1f;
                playerBonusRegen = 0;
                playerBonusFlatDamageReduction = 0;
                playerBonusDamage = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UI_InputManager.Instance.OpenInfoPopup();

        Difficulty = Director.GetInstance().difficultyScaling;
        Statusmanager playerStatus =  GameObject.FindObjectOfType<PlayerController>().GetComponent<Statusmanager>();
        playerStatus.healthRegeneration += playerBonusRegen;
        playerStatus.flatDamageReduction += playerBonusFlatDamageReduction;
        playerStatus.AttackDamageFlatBonus += playerBonusDamage;

        Director.GetInstance().timePassed = new System.TimeSpan();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
