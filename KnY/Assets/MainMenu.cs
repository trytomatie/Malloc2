using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
