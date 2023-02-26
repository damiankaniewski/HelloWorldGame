using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMultipliers : MonoBehaviour
{
    public static float difficultyMultiplier = 1f;

    public void Easy()
    {
        difficultyMultiplier = 0.5f;
    }
    public void Normal()
    {
        difficultyMultiplier = 1f;
    }
    public void Hard()
    {
        difficultyMultiplier = 1.5f;
    }

    public void Extreme()
    {
        difficultyMultiplier = 2f;
    }
}
