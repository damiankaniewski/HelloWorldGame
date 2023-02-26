using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButtons : MonoBehaviour
{
    public static bool OptionsOpened = false;

    public GameObject MenuUI;

    private void Start()
    {
        //PauseMenu.GameIsPaused = false;
    }

    private void Update()
    {
        //PauseMenu.GameIsPaused = false;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.GameIsPaused = true;
            OptionsOpened = false;
            Back();
        }
    }

    void Back()
    {
        MenuUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void BackButton()
    {
        PauseMenu.GameIsPaused = true;
        OptionsOpened = false;
    }

    public void OptionsButton()
    {
        PauseMenu.GameIsPaused = true;
        OptionsOpened = true;
    }
}