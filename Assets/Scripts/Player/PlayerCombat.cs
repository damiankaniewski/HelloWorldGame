using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackPower = 40;
    public int attackDamage;
    private int min, max;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public LayerMask enemyLayer;
    private Health hp;

    private void Start()
    {
        hp = GetComponent<Health>();
    }

    private void Awake()
    {
        min = attackPower - 10;
        max = attackPower + 10;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        attackDamage = Random.Range(min, max);
    }


    void Attack()
    {
        FindObjectOfType<AudioManager>().Play("PlayerAttack");

        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>() == true)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            hp.TakeDamage(1);
        }
    }
}