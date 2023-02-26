using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    public GameObject Player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.transform.parent = null;
        }
    }
}
