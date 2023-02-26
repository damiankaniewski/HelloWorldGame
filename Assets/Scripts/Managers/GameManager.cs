using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private bool gameHasEnded = false;

    public float restartDelay = 1f;

    public GameObject l1, l2, l3, l4, l5, l6, l7, l8, l9, l10;
    public void Awake()
    {
        l1.SetActive(true);
        l2.SetActive(false);
        l3.SetActive(false);
        //l4.SetActive(false);
        //l5.SetActive(false);
        //l6.SetActive(false);
        //l7.SetActive(false);
        //l8.SetActive(false);
        //l9.SetActive(false);
        //l10.SetActive(false);
        
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!");
            Invoke("Restart", restartDelay);
        }
        
    }

    void Restart()
    {
        SceneManager.LoadScene("GameOver");
        Time.timeScale = 1;
    }
    
}