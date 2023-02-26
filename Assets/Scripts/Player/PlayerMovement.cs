using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rb;

    public float runSpeed = 40f;
    public float boostSpeed = 20f;
    public int SpeedPotionAmount = 3;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    public static PlayerMovement instance;
    public TextMeshProUGUI speedPotionAmountText;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetKeyDown(KeyCode.Q) && SpeedPotionAmount > 0)
        {
            FindObjectOfType<AudioManager>().Play("PotionDrink");
            RunBoost();
            ChangeAmount(-1);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("CoinEarn");
        }

        if (other.gameObject.CompareTag("Healing"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("PlayerHeal");
        }
        if (other.gameObject.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("PotionEarn");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Trampoline"))
        {
            FindObjectOfType<AudioManager>().Play("Trampoline");
            rb.velocity = Vector2.up * 20;
            animator.SetBool("IsJumping", true);
        }

        if (other.gameObject.CompareTag("SpeedPlatform"))
        {
            FindObjectOfType<AudioManager>().Play("SpeedPlatform");
        }
    }

    public void FootstepsSound()
    {
        FindObjectOfType<AudioManager>().Play("PlayerFootsteps");
    }

    public void JumpSound()
    {
        FindObjectOfType<AudioManager>().Play("Jump");
    }

    public void RunBoost()
    {
        runSpeed += boostSpeed;
        StartCoroutine(ExecuteAfterTime(5));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        runSpeed -= boostSpeed;
    }

    public void ChangeAmount(int amount)
    {
        SpeedPotionAmount += amount;
        speedPotionAmountText.text = "x" + SpeedPotionAmount.ToString();
    }
}