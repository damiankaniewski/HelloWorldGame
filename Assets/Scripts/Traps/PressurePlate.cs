using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject trap;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trap.SetActive(true);
            Destroy(trap,0.7f);
            Destroy(gameObject,0.7f);
        }
        
        
    }
}
