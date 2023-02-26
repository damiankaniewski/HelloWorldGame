using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol Parameters")] public float speed;
    public Transform[] patrolPoints;
    public float waitTime;
    private int currentPointIndex;
    private bool once;


    [Header("Attack Parameters")] [SerializeField]
    private float attackCooldown;
    [SerializeField] private float rangedAttackCooldown;

    [SerializeField] private float meleeRange;
    [SerializeField] private float rangedRange;
    public int damage;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreatDistance;
    [SerializeField] private GameObject projectile;

    [Header("Collider Parameters")] [SerializeField]
    private float colliderDistance;

    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")] [SerializeField]
    private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    [Header("Coin Value")] public int coinValue;
    public ScoreManager scoreManager;

    [Header("Other")] [SerializeField] private GameObject floatingTextPrefab;
    public HealthBarBehaviour healthBar;
    public Transform player;
    private Animator anim;

    private Health playerHealth;

    private PolygonCollider2D polygonCollider2D;

    private Rigidbody2D rb;
    //private EnemyPatrol enemyPatrol;

    public int maxHealth = 100;
    private int currentHealth;
    [Header("Regeneration")] private int angle = 180;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
        InvokeRepeating("Regenerate", 0.0f, 0.2f);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance) //if player not in aggro range
        {
            if (transform.position != patrolPoints[currentPointIndex].position)
            {
                gameObject.GetComponent<Animator>().SetBool("IsMoving", true);
                if (transform.position.x < patrolPoints[currentPointIndex].position.x)
                {
                    Debug.Log("LEFT");
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (transform.position.x > patrolPoints[currentPointIndex].position.x)
                {
                    Debug.Log("RIGHT");
                    transform.rotation = Quaternion.Euler(0, -180, 0);
                }

                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position,
                    speed * Time.deltaTime);
            }
            else
            {
                if (once == false)
                {
                    once = true;
                    gameObject.GetComponent<Animator>().SetBool("IsMoving", false);
                    StartCoroutine(Wait());
                }
            }
        }
        /*else if ((Vector2.Distance(transform.position, player.position) < stoppingDistance &&
                 Vector2.Distance(transform.position, player.position) > retreatDistance)) //if player in aggro range
        {
            
        }*/
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance) //if player too close
        {
            if (transform.position.x < player.position.x)
            {
                Debug.Log("LEFT");
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector2(patrolPoints[0].position.x, transform.position.y), speed * Time.deltaTime);
            }
            else if (transform.position.x > player.position.x)
            {
                Debug.Log("RIGHT");
                transform.rotation = Quaternion.Euler(0, -180, 0);
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector2(patrolPoints[1].position.x, transform.position.y), speed * Time.deltaTime);
            }

            if (PlayerInSight())
            {
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("MeleeAttack");
                }
            }

            if (PlayerInRangeSight())
            {
                if (cooldownTimer >= rangedAttackCooldown)
                {
                    anim.SetTrigger("MeleeAttack");
                    Instantiate(projectile.transform, transform.position, Quaternion.identity);
                    cooldownTimer = 0;
                }
            }
        }


        healthBar.SetHealth(currentHealth, maxHealth);

        cooldownTimer += Time.deltaTime;


        //Attack only when player in sight?
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }

        RotateByDegrees();
        once = false;
    }

    void RotateByDegrees()
    {
        Vector3 rotationToAdd = new Vector3(0, 180, 0);
        transform.Rotate(rotationToAdd);
    }

    void Regenerate()
    {
        if (currentHealth < maxHealth)
            currentHealth += 1;
    }

    public void TakeDamage(int damage)
    {
        ShowDamage(damage.ToString());
        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        healthBar.SetHealth(currentHealth, maxHealth);

        anim.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        scoreManager.ChangeScore(coinValue);
        Destroy(gameObject, 1.5f);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(
                boxCollider.bounds.center + transform.right * meleeRange * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * meleeRange, boxCollider.bounds.size.y,
                    boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private bool PlayerInRangeSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(
                boxCollider.bounds.center +
                transform.right * rangedRange * 3 * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * rangedRange * 3, boxCollider.bounds.size.y * 6,
                    boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * meleeRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * meleeRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * rangedRange * 3 * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * rangedRange * 3, boxCollider.bounds.size.y * 6,
                boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage * DifficultyMultipliers.difficultyMultiplier);
    }

    public void EnemyFootsteps()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void EnemyHurt()
    {
        FindObjectOfType<AudioManager>().Play("EnemyHurt");
    }

    public void EnemyDead()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDead");
    }

    void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coins"))
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}