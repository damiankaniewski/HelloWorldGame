using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class PlayerAimBase : MonoBehaviour
{
    public PlayerAimWeapon playerAimWeapon;
    public GameObject weapon;
    
    private void Start()
    {
        playerAimWeapon.OnShoot += PlayerAimWeapon_OnShoot;
        weapon.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            weapon.SetActive(false);
        }
    }

    private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        UtilsClass.ShakeCamera(0.02f, 0.1f);
    }
    
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
 
        weapon.SetActive(false);
    }
}
