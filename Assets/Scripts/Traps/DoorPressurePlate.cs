using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPressurePlate : MonoBehaviour
{
    public GameObject openableDoor;
    public Animator anim;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("IsOn");
            anim.SetTrigger("IsOn");
            openableDoor.GetComponent<Animator>().SetTrigger("IsOpening");
        }
    }
}
