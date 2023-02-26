using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField] private float damage;
    public GameObject pressurePlate;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pressurePlate.active || gameObject.active)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Health>().TakeDamage(damage);
                Destroy(gameObject);
                Destroy(pressurePlate);
            } 
        }
        
        
    }
}