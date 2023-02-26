using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100;
    public Animator anim;
    public Rigidbody2D rb;

    public static Health instance;
    public TextMeshProUGUI text;

    public int HealingPotionAmount = 3;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeAmount(-1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeAmount(1);
        }

        if (Input.GetKeyDown(KeyCode.H) && HealingPotionAmount > 0)
        {
            ChangeAmount(-1);
            Healing(50*(1/DifficultyMultipliers.difficultyMultiplier));
        }

        if (HealingPotionAmount == 0)
        {
            Debug.Log("You have 0 potions!");
        }

        if (healthAmount <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            FindObjectOfType<AudioManager>().Stop("PlayerFootsteps");
            FindObjectOfType<AudioManager>().Stop("Theme");
            anim.Play("Dead");
            StartCoroutine(ExecuteAfterTime(1));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Healing(20*(1/DifficultyMultipliers.difficultyMultiplier));
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        FindObjectOfType<AudioManager>().Play("DeadTheme");
        SceneManager.LoadScene("GameOver");
    }

    public void TakeDamage(float Damage)
    {
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
        anim.SetTrigger("Hurt");
        healthAmount -= Damage;
        healthBar.fillAmount = healthAmount / 100;
    }

    public void Healing(float healPoints)
    {
        healthAmount += healPoints;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        FindObjectOfType<AudioManager>().Play("PlayerHeal");

        healthBar.fillAmount = healthAmount / 100;
    }

    public void ChangeAmount(int amount)
    {
        HealingPotionAmount += amount;
        text.text = "x" + HealingPotionAmount.ToString();
    }
}