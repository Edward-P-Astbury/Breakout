using UnityEngine;

public class HealthPickup : Pickup
{
    // Start is called before the first frame update
    public override void Start()
    {
        amount = 50;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerManager.instance.PlayerHealth.CurrentHealth < 100)
            {
                Debug.Log("health gained");

                PlayerManager.instance.PlayerHealth.Heal(amount);

                AudioManager.instance.Play("HealthPickup");

                Destroy(gameObject); // Destory the object once picked up
            }
        }
    }
}