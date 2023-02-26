using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;
    private Health playerHealth;
    private GameObject enemy;
    [SerializeField] private GameObject fireParticles;
    
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("ProjectileSound");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,target,speed*Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.transform.GetComponent<Health>();
            //playerHealth.TakeDamage(5);
            playerHealth.TakeDamage(enemy.GetComponent<Enemy>().damage);
            DestroyProjectile();
            fireParticles = Instantiate(fireParticles, transform.position, Quaternion.identity);
            Destroy(fireParticles,2);
        }

        if (other.CompareTag("Ground"))
        {
            DestroyProjectile();
            fireParticles = Instantiate(fireParticles, transform.position, Quaternion.identity);
            Destroy(fireParticles,2);
        }
    }

    void DestroyProjectile()
    {
        FindObjectOfType<AudioManager>().Play("ProjectileImpact");
        Destroy(gameObject);
    }

    void SpawnFireParticles()
    {
        fireParticles = Instantiate(fireParticles, transform.position, Quaternion.identity);
        Destroy(fireParticles,1f);
    }
}
