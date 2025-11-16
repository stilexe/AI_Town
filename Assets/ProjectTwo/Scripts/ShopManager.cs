using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _cashRegister;
    [SerializeField] private GameObject _cartHolder;
    [SerializeField] private GameObject _door;

    public GameObject CashRegister()
    {
        return _cashRegister; //todo: handle list of them and find with smallest queue 
    }

    public GameObject CartHolder()
    {
        return _cartHolder;
    }

    public GameObject Door()
    {
        return _door;
    }
}
