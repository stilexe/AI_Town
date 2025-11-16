using System;
using System.Collections.Generic;
using Tanks;
using TMPro;
using UnityEngine;

public class AIBoolDisplay : MonoBehaviour
{
    private ShopperSensor _shopper;
    private DoorknockerSensor _knocker;
    private RioterSensor _rioter;

    private List<TextMeshProUGUI> _children = new List<TextMeshProUGUI>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            _children.Add(child.gameObject.GetComponent<TextMeshProUGUI>());
        }
    }

    private void Update()
    {
        if (_shopper)
        {
            _children[0].color = _shopper.seeCart ? Color.green : Color.red;
            _children[1].color = _shopper.hasCart ? Color.green : Color.red;
            _children[2].color = _shopper.seeShelf ? Color.green : Color.red;
            _children[3].color = _shopper.fullCart ? Color.green : Color.red;
            _children[4].color = _shopper.seeCashier ? Color.green : Color.red;
            _children[5].color = _shopper.queuing ? Color.green : Color.red;
            _children[6].color = _shopper.frontOfQueue ? Color.green : Color.red;
            _children[7].color = _shopper.paid ? Color.green : Color.red;
            _children[8].color = _shopper.leftShop ? Color.green : Color.red;
        }

        if (_knocker)
        {
            _children[0].color = _knocker.seesDoor ? Color.green : Color.red;
            _children[1].color = _knocker.atDoor ? Color.green : Color.red;
            _children[2].color = _knocker.knocked ? Color.green : Color.red;
        }

        if (_rioter)
        {
            _children[0].color = _rioter.hasTarget ? Color.green : Color.red;
            _children[1].color = _rioter.atTarget ? Color.green : Color.red;
            _children[2].color = _rioter.targetDestroyed ? Color.green : Color.red;
        }
    }

    public void DisplayShopper(ShopperSensor shopper)
    {
        _knocker = null; 
        _rioter = null;
        
        _shopper = shopper;
    }

    public void DisplayKnocker(DoorknockerSensor knocker)
    {
        _shopper = null; 
        _rioter = null;
        
        _knocker = knocker;
    }

    public void DisplayRioter(RioterSensor rioter)
    {
        _knocker = null;
        _shopper = null;
        
        _rioter = rioter;
    }
}
