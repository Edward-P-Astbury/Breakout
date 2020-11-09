public class Inventory
{
    private int _ammo;

    // Demonstrating constructors
    public Inventory(int ammo)
    {
        _ammo = ammo;
    }

    public int Ammo
    {
        get
        {
            return _ammo;
        }
        set
        {
            _ammo = value;
        }
    }
}