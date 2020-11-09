using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        ammoText.text = PlayerManager.instance.PlayersInventory.ToString();
    }

    public void UpdateAmmoUI()
    {
        ammoText.text = PlayerManager.instance.PlayersInventory.ToString();
    }
}