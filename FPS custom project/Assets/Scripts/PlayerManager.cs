using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    // Using this Singleton pattern means we can keep constant track of our player 
    // Whilst not having to instantiate its position at runtime.
    // This Singleton enforces the existence of only ONE object type at any given moment.
    //Singleton differs from a static class, except it's an actual object that can be passed around and referenced.

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this; // Current instance of the class
        _playerHealth = GetComponent<HealthSystem>();
    }

    #endregion

    [SerializeField] private GameObject _player;
    private HealthSystem _playerHealth;
    Inventory playersInventory = new Inventory(100); // Create a new Inventory with the amount of bullets in the parameter

    // Property
    public GameObject Player
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
        }
    }

    public HealthSystem PlayerHealth
    {
        get
        {
            return _playerHealth;
        }
        set
        {
            _playerHealth = value;
        }
    }

    public int PlayersInventory
    {
        get
        {
            return playersInventory.Ammo;
        }
        set
        {
            playersInventory.Ammo = value;
        }
    }
}