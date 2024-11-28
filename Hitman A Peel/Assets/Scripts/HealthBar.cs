using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthBar : MonoBehaviour
{
    public Slider slider; // slider for the health bar
    public Gradient gradient; // gradient for the health bar
    public Image fill; 
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health; // set the max value of the slider to the health
        slider.value = health;

        fill.color = gradient.Evaluate(100f); // set the color of the fill to the max value of the gradient
    }
    public void SetHealth(int health)
    {
        slider.value = health; // set the value of the slider to the health

        fill.color = gradient.Evaluate(slider.normalizedValue); // set the color of the fill to the normalized value of the gradient
    }



}
