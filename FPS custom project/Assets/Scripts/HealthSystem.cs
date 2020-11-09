using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable
{
    private int maxHealth = 100;
    private int currentHealth;

    [SerializeField] private HealthBar healthBar;

    // Read only property
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // Setting the slider to max health on start
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth); // Pass in the current health into the slider when we take damage
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        healthBar.SetHealth(currentHealth);
    }

    public void Die(float time)
    {
        Destroy(gameObject, time); // Time of death animation
    }
}