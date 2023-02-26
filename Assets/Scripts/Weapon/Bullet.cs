using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public int damage = 30;
    private int min, max;
    private int attackDamage;

    private void Awake()
    {
        min = damage - 10;
        max = damage + 10;
    }

    private void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        attackDamage = Random.Range(min, max);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        Enemy patrolEnemy = hit.GetComponent<Enemy>();
        if (patrolEnemy != null)
        {
            patrolEnemy.TakeDamage(attackDamage);
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        if (hit.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        
        
    }
}