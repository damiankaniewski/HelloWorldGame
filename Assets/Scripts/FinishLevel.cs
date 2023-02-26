using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class FinishLevel : MonoBehaviour
{
    public int finishScore;
    public GameObject currentLevel;
    public GameObject nextLevel;
    public ScoreManager score;
    public Animator anim;

    private void Update()
    {
        if(score.score >= finishScore)
            anim.SetBool("IsLevelCompleted", true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(score.score < finishScore)
            Debug.Log("You need to collect all coins!");
        
        if (col.gameObject.CompareTag("Player") && score.score >= finishScore)
        {
            FindObjectOfType<AudioManager>().Play("FinishLevel");
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        score.score = 0;
        score.ChangeScore(score.score);
        nextLevel.SetActive(true);
        currentLevel.SetActive(false);
        
    }
}

