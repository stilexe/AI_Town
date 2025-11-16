using UnityEngine;

public class Cart : MonoBehaviour
{
    private int _maxContents = 3; 
    private int _currentContents;

    private GameObject _pusher; 

    public void NewPusher(GameObject newPusher)
    {
        _currentContents = 0;
        _pusher = newPusher;
        _pusher.GetComponent<ShopperSensor>().cart = gameObject;
        _pusher.GetComponent<Push>().AddObject(gameObject);
    }
    
    public void Fill(int amount)
    {
        _currentContents += amount;

        if (_currentContents >= _maxContents)
        {
            _currentContents = _maxContents;
            _pusher.GetComponent<ShopperSensor>().fullCart = true; 
        }
    }
}
