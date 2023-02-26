using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    public Image image;

    public void SetHealth(float health, float maxHealth)
    {
        Slider.gameObject.SetActive(health<maxHealth);
        Slider.value = health;
        Slider.maxValue = maxHealth;

        image.color = Color.Lerp(Low, High, Slider.normalizedValue);
    }
    
    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
        if (Slider.value <= 0)
        {
            Destroy(gameObject,1);
        }
    }
}