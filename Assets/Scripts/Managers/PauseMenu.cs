using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == true)
            {
                if (OptionsButtons.OptionsOpened == false)
                {
                    Resume();
                    GameIsPaused = false;
                }
            }
            else
            {
                Pause();
                GameIsPaused = true;
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        OptionsButtons.OptionsOpened = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        OptionsButtons.OptionsOpened = false;
    }

    public void OptionsSound()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }
}