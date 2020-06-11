using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("DifficultyDropDown").GetComponent<Dropdown>().value = Director.GetInstance().difficultyScaling;
        SetDifficulty();
        SetSeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }
    public void AncientLabyrinthButton()
    {
        SceneManager.LoadScene("AncientLabyrinth");
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

    public void SetSeed()
    {
        Director.SetGlobalSeed();
    }
}
