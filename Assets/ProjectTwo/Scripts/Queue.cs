using System;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    [SerializeField] private Transform _queueStart;

    private List<ShopperSensor> _customers = new List<ShopperSensor>(); 
    
    public delegate void CustomerServed();
    public event CustomerServed OnCustomerServed;

    public Vector3 GetQueuePosition(GameObject customer)
    {
        float offset = 0; 
        
        if (_customers.Contains(customer.GetComponent<ShopperSensor>()))
        {
            for (int i = 0; i < _customers.Count; i++)
            {
                if (_customers[i] == customer.GetComponent<ShopperSensor>())
                {
                    offset = i * 1.5f;
                    break;
                }
            }
        }
        else
        {
            _customers.Add(customer.GetComponent<ShopperSensor>());
            offset = _customers.Count * 1.5f;
        }
        
        Vector3 position = _queueStart.position;
        
        position.x -= offset;
        
        return position;
    }

    /// <summary>
    /// Check to see if the character is at the front of the line 
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public bool FrontOfLine(GameObject customer)
    {
        if (_customers[0] == customer.GetComponent<ShopperSensor>())
        {
            return true;
        }

        return false; 
    }

    public void NextCustomer()
    {
        _customers.RemoveAt(0);
        OnCustomerServed?.Invoke();
    }
}
