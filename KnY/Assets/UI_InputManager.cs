
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputManager : MonoBehaviour
{
    private static UI_InputManager instance;
    public GameObject inventory;
    public GameObject traderInventory;
    public GameObject infoPopup;
    public UI_PassiveSkillExchangeManager passiveSkillExchangeManager;
    public UI_ActiveSkillExchangeManager activeSkillExchangeManager;
    public GameObject statsPanel;
    public Text stats;
    public static UI_InputManager Instance
    {
        get
        {
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
        UI_ActiveSkillExchangeManager.Instances.Add(activeSkillExchangeManager);
        UI_PassiveSkillExchangeManager.Instances.Add(passiveSkillExchangeManager);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("OpenInventory"))
        {
            if(!traderInventory.activeSelf)
            { 
                if (!inventory.activeSelf)
                { 
                    OpenInventory();
                }
                else
                {
                    CloseInventory();
                }
                UI_ArtifactDisplayDescriptionPopup.DespawnAllInstances();
                GameObject.Find("ItemContextMenu").transform.position = new Vector3(10000,10000,10000);
            }
        }
        if (Input.GetButtonDown("Escape"))
        {
            if (traderInventory.activeSelf)
            {
                CloseTraderInventory();
            }
            else if(infoPopup.activeSelf)
            {
                CloseInfoPopup();
            }
            else if(inventory.activeSelf)
            {
                CloseInventory();
            }else if(passiveSkillExchangeManager.gameObject.activeSelf)
            {
                UI_PassiveSkillExchangeManager.CloseSkillExchagneWindow();
            }
            else if (activeSkillExchangeManager.gameObject.activeSelf)
            {
                UI_ActiveSkillExchangeManager.CloseSkillExchagneWindow();
            }
            else if (statsPanel.activeSelf)
            {
                statsPanel.SetActive(false);
            }
            UI_ArtifactDisplayDescriptionPopup.DespawnAllInstances();
            GameObject.Find("ItemContextMenu").transform.position = new Vector3(10000, 10000, 10000);
        }
        if(Input.GetButtonDown("CharacterMenu"))
        {
            if(statsPanel.activeSelf)
            {
                statsPanel.SetActive(false);
            }
            else
            {
               
                statsPanel.SetActive(true);
            }
        }
        if (statsPanel.activeSelf)
        {
            RefreshStatsText();
        }
    }

    public void OpenInventory()
    {
        Time.timeScale = 0;
        inventory.SetActive(true);
    }

    public void CloseInventory()
    {
        Time.timeScale = 1;
        inventory.SetActive(false);
    }


    public void OpenTraderInventory()
    {
        Time.timeScale = 0;
        traderInventory.SetActive(true);
    }


    public void CloseTraderInventory()
    {
        Time.timeScale = 1;
        traderInventory.SetActive(false);
    }

    public void OpenInfoPopup()
    {
        Time.timeScale = 0;
        infoPopup.SetActive(true);
    }

    public void CloseInfoPopup()
    {
        Time.timeScale = 1;
        infoPopup.SetActive(false);
    }

    private void RefreshStatsText()
    {
        Statusmanager s = GameObject.Find("Player").GetComponent<Statusmanager>();
        string statsText = "";
        statsText += String.Format("Class: {0} \n", s.characterClass.ToString());
        statsText += String.Format("Hp: {0} / {1}\n", s.Hp, s.maxHp);
        statsText += String.Format("Sp: {0} / {1}\n", s.Sp, s.maxSp);
        statsText += String.Format("Attack Power: {0}( x {1})\n", s.totalAttackDamage,s.BaseAttackDamageMultiplyier);
        statsText += String.Format("Magic Power: {0}( x {1})\n", s.TotalMagicPower, s.MagicPowerMultiplier);
        statsText += String.Format("Def: {0} \n", s.defence);
        statsText += String.Format("HpRegen.: {0}\n", s.healthRegeneration);
        statsText += String.Format("SpRegen.: {0}\n", s.spRegeneration);
        stats.text = statsText;
    }

    public void TriggerBattleInputOnPlayer(int i)
    {
        GameObject.FindObjectOfType<PlayerController>().BattleInput(i);
    }

    public void TriggerInteractionOnPlayer(int i)
    {
        GameObject.FindObjectOfType<PlayerController>().Interact();
    }


    public void LoadNextLevel()
    {
        GameObject.FindObjectOfType<MapGenerator>().GenerateNewMap();
    }



}
