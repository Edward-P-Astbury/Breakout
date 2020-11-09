public interface IDamagable
{
    // We make our HealthSystem derive from this interface so that any class implementing the HealthSystem can be damaged.

    void TakeDamage(int damage);
    void Die(float time);

    void Heal(int amount);
}
