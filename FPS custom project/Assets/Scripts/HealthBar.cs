using UnityEngine;
using UnityEngine.UI; // namespace for UI

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public void SetHealth(int health)
    {
        slider.value = health; // The sliders value is set to the value passed in

        fill.color = gradient.Evaluate(slider.normalizedValue); // Normalized value means it will remain between 0 and 1, which is the values of our gradient
    }

    // Setting max health value
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health; // Making sure slider starts with maximum amount of health

        fill.color = gradient.Evaluate(1f); // Altering the colour to fit our gradient. One is the max value, so it will be green
    }
}