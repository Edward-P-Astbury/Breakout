using UnityEngine;

public class Gun : MonoBehaviour
{
    // Serialising the field makes it available in the Unity inspector
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 15f;

    [SerializeField] private Camera fpsCam; // Reference to camera for raycasting
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect; // Declare as game object so that we can instantiate it

    private float nextTimeToFire = 0f;

    private Bullet bullet;

    private int damage = 2;

    private AmmoUI ammoUI;

    // Awake is called when the script gets initialised
    private void Awake()
    {
        bullet = GetComponent<Bullet>();
        ammoUI = GetComponent<AmmoUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire && PlayerManager.instance.PlayersInventory > 0) // Fire1 is default button for Unity
        {
            nextTimeToFire = Time.time + 1f / fireRate; // Greater the fire rate the less time between shots
            Shoot();
            
            // Accessing the bullet class method
            bullet.CreateBullet();

            // Pushing the bullet in a direction by the bulletFowardForce variable
            bullet.TempRigidBody.AddForce(transform.right * bullet.BulletFowardForce);

            // Decrease the ammo
            PlayerManager.instance.PlayersInventory--;

            // Decrease the ammoUI
            ammoUI.UpdateAmmoUI();
        }
        else if(Input.GetButtonDown("Fire1") && PlayerManager.instance.PlayersInventory <= 0)
        {
            AudioManager.instance.Play("DryFire");
        }
    }

    void Shoot()
    {
        AudioManager.instance.Play("GunFire");

        muzzleFlash.Play(); // Starts the particle system

        RaycastHit hit;

        // First parameter is position from where to shoot and in this case its the camera
        // Second parameter is the direction which we want to shoot the ray
        // Third parameter is hit and the out keyword means Unity will store all the information inside that variable
        // Fourth parameter is simply the range
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Info is inside the hit variable
            Debug.Log(hit.transform.name);

            // Local enemy variable
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                // Reference to the EnemyHealth property in the parent class
                enemy.EnemyHealth.TakeDamage(damage);

                // Instantiate our impact effect
                // Choose the point
                // Choose the rotation
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

                // Have to play the effect after instantiating it
                impactGO.GetComponent<ParticleSystem>().Play();

                // Destory the game object after specified time
                Destroy(impactGO, 1f);
            }
        }
    }
}