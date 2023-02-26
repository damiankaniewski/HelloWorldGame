using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBoulder : MonoBehaviour
{
    [SerializeField] private float damage;
    public Animator anim;
    private CircleCollider2D cc;
    private void Start()
    {
        cc = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Bullet"))
        {
            cc.enabled = false;
            anim.SetTrigger("IsBroke");
            Destroy(gameObject,0.5f);
        }
    }

}