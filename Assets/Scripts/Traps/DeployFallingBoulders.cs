using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployFallingBoulders : MonoBehaviour
{
    public GameObject fallingBoulderPrefab;
    public float respawnTime = 1.0f;
    public float destroyTime;

    private void Start()
    {
        StartCoroutine(boulderWave());
    }

    IEnumerator boulderWave()
    {
        while (true)
        {
            var position = new Vector3(transform.position.x, transform.position.y);
            GameObject gameObject = Instantiate(fallingBoulderPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(respawnTime);
            Destroy(gameObject,destroyTime);
        }
    }
}
