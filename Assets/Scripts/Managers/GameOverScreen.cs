using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Restart()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<AudioManager>().Stop("DeadTheme");
        SceneManager.LoadScene("Game");
    }
    

    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("Menu");
    }
}