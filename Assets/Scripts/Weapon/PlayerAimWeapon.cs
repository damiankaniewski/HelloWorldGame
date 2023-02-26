using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 shellPosition;
    }

    private Transform aimTransform;
    public Transform weaponTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;
    private Animator weaponAnimator;

    public Transform bulletPrefab;

    private float myTime = 0.0f;
    public float fireDelta = 0.2f;
    private float nextFire = 0.5f;
    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        weaponAnimator = weaponTransform.GetComponent<Animator>();
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        aimShellPositionTransform = aimTransform.Find("ShellPosition");
    }

    private void Update()
    {
        myTime = myTime + Time.deltaTime;
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < 90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = +1f;
        }

        aimTransform.localScale = aimLocalScale;
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0) && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            Instantiate(bulletPrefab, aimShellPositionTransform.position, aimShellPositionTransform.rotation);

            FindObjectOfType<AudioManager>().Play("Shoot");
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

            weaponAnimator.SetTrigger("IsShooting");
            OnShoot?.Invoke(this, new OnShootEventArgs()
            {
                gunEndPointPosition = aimGunEndPointTransform.position, shootPosition = mousePosition,
                shellPosition = aimShellPositionTransform.position
            });
            nextFire = nextFire - myTime;
            myTime = 0.0f;
        }
    }
}