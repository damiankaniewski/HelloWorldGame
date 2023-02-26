using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<AudioManager>().Stop("DeadTheme");
    }

    private void Start()
    {
        SceneManager.UnloadSceneAsync("GameOver");
        SceneManager.UnloadSceneAsync("Game");
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Debug.LogWarning("Quit");
        Application.Quit();
    }

    
    public void OptionsSound()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }
}