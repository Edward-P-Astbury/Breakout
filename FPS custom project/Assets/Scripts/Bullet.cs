using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletEmitter; // The point it which it shoots

    [SerializeField] private GameObject bullet; // The bullet prefab itself

    [SerializeField] private float bulletForwardForce = 1400; // Speed of the bullet

    private Rigidbody tempRigidBody;

    //read only property
    public float BulletFowardForce
    {
        get
        {
            return bulletForwardForce;
        }
    }

    public Rigidbody TempRigidBody
    {
        get
        {
            return tempRigidBody;
        }
        set
        {
            tempRigidBody = value;
        }
    }

    public void CreateBullet()
    {
        // Instantiation of the bullet
        GameObject tempBullet;
        tempBullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

        // Accessing the Rigidbody from the bullet
        tempRigidBody = tempBullet.GetComponent<Rigidbody>();

        // Delete game object
        Destroy(tempBullet, 3f);
    }
}