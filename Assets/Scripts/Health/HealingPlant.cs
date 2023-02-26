using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPlant : MonoBehaviour
{
    public Health hp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hp.Healing(25*(1/DifficultyMultipliers.difficultyMultiplier));
            hp.ChangeAmount(1);
        }
    }
    
}
