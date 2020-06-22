using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject buyButton;
    public Image contractImage;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("DifficultyDropDown").GetComponent<Dropdown>().value = Director.GetInstance().difficultyScaling;
        GameObject.Find("ContractDropDown").GetComponent<Dropdown>().value = (int)Director.GetInstance().characterClass;
        Api.AddManaGem(0);
        SetDifficulty();
        SetSeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        int i = GameObject.Find("ContractDropDown").GetComponent<Dropdown>().value;
        if (Api.ContractUnlocks[(Statusmanager.CharacterClass)i] == -1)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
    public void AncientLabyrinthButton()
    {
        int i = GameObject.Find("ContractDropDown").GetComponent<Dropdown>().value;
        if (Api.ContractUnlocks[(Statusmanager.CharacterClass)i] == -1)
        {
            SceneManager.LoadScene("AncientLabyrinth");
        }
    }

    public void IntroButton()
    {
        SceneManager.LoadScene("Intro");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetDifficulty()
    {
        Director.SetDifficulty();
    }

    public void SetContract()
    {
        int i = GameObject.Find("ContractDropDown").GetComponent<Dropdown>().value;
        if(Api.ContractUnlocks[(Statusmanager.CharacterClass)i] != -1)
        {
            buyButton.SetActive(true);
            buyButton.GetComponent<Text>().text = "Buy for " + Api.ContractUnlocks[(Statusmanager.CharacterClass)i] + " Mana Gems";
            contractImage.color = new Color32(66, 66, 66, 255);
        }
        else
        {
            buyButton.SetActive(false);
            contractImage.color = new Color32(255, 255, 255, 255);
        }
        Director.SetContact(i);
    }

    public void BuyContract()
    {
        int i = GameObject.Find("ContractDropDown").GetComponent<Dropdown>().value;
        if(Api.ManaGems >= Api.ContractUnlocks[(Statusmanager.CharacterClass)i])
        {
            Api.ManaGems -= Api.ContractUnlocks[(Statusmanager.CharacterClass)i];
            Api.ContractUnlocks[(Statusmanager.CharacterClass)i] = -1;
        }
        Api.SaveCurrent();
        SetContract();
    }

    public void SetSeed()
    {
        Director.SetGlobalSeed();
    }
}
