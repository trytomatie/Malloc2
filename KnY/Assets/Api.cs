using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Api
{
    public static int ManaGems { get; set; }
    public static int Kills { get; set; }
    public static int DamageDone { get; set; }
    public static int DamageTaken { get; set; }
    public static int HealingDone { get; set; }

    public static int TotalManaGems { get; set; }
    public static int TotalKills { get; set; }
    public static int TotalDamageDone { get; set; }
    public static int TotalDamageTaken { get; set; }
    public static int TotalHealingDone { get; set; }
    public static Dictionary<Statusmanager.CharacterClass, int> ContractUnlocks { get; set; } = new Dictionary<Statusmanager.CharacterClass, int>()
    {
        {Statusmanager.CharacterClass.Undefined,-1},
        {Statusmanager.CharacterClass.Warrior,-1},
        {Statusmanager.CharacterClass.Mage,-1},
        {Statusmanager.CharacterClass.Priest,-1},
        {Statusmanager.CharacterClass.Summoner,100},
        {Statusmanager.CharacterClass.Paladin,500},
    };

    public static void ResetCurrent()
    {
        SaveCurrent();
        AddManaGem(0);
        Kills = 0;
        DamageDone = 0;
        DamageTaken = 0;
        HealingDone = 0;
    }
    public static void AddKill()
    {
        Kills++;
        AddManaGem(1);
    }
    public static void AddDamageDone(int value)
    {
        DamageDone += value;
        TotalDamageDone += value;
    }
    public static void AddDamageTaken(int value)
    {
        DamageTaken += value;
        TotalDamageTaken += value;
    }
    public static void AddHealingDone(int value)
    {
        HealingDone += value;
    }

    public static void AddManaGem(int value)
    {
        ManaGems += value;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("ManaGemText"))
        {
            go.GetComponent<Text>().text = ManaGems.ToString();
        }
    }

    public static void ReadSaveData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            GameData save = (GameData)bf.Deserialize(file);
            file.Close();

            ManaGems = save.ManaGems;
            TotalKills = save.Kills;
            TotalDamageDone = save.DamageDone;
            TotalDamageTaken = save.DamageTaken;
            TotalHealingDone = save.HealingDone;

            foreach(Statusmanager.CharacterClass key in save.ContractUnlocks.Keys)
            {
                if(ContractUnlocks.ContainsKey(key))
                {
                    ContractUnlocks[key] = save.ContractUnlocks[key];
                }
            }
        }
    }

    public static void SaveCurrent()
    {
        GameData save = GameData.CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

    }


}


[System.Serializable]
public class GameData
{
    public int ManaGems { get; set; }
    public int Kills { get; set; }
    public int DamageDone { get; set; }
    public int DamageTaken { get; set; }
    public int HealingDone { get; set; }
    public Dictionary<Statusmanager.CharacterClass, int> ContractUnlocks { get; set; } = new Dictionary<Statusmanager.CharacterClass, int>();
    public static GameData CreateSaveGameObject()
    {
        GameData save = new GameData();
        save.ManaGems = Api.ManaGems;
        save.Kills = Api.TotalKills;
        save.DamageDone = Api.TotalDamageDone;
        save.DamageTaken = Api.DamageTaken;
        save.HealingDone = Api.TotalHealingDone;
        save.ContractUnlocks = Api.ContractUnlocks;

        return save;
    }
}
