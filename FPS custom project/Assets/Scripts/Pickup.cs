using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected int amount;

    // Start is called before the first frame update
    public abstract void Start();

    // Abstract parent class
    public abstract void OnTriggerEnter(Collider other);
}