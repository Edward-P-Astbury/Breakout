using UnityEngine;

public class AmmoPickup : Pickup
{
    private AmmoUI ammoUI;

    // Start is called before the first frame update
    public override void Start()
    {
        amount = 100;
        ammoUI = GetComponent<AmmoUI>();
    }

    // Add ammo to the players inventory when collision is detected
    public override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Ammo gained");

            PlayerManager.instance.PlayersInventory += amount;
            ammoUI.UpdateAmmoUI();

            AudioManager.instance.Play("AmmoPickup");

            Destroy(gameObject); // Destory the object once picked up
        }
    }
}