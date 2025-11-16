using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class ShopperSensor : MonoBehaviour, ISense
{
    public enum ShoppingScenario
    {
        FullCart = 0,
        HasCart = 1,
        Paid = 2,
        SeeCart = 3,
        SeeShelf = 4,
        FrontOfQueue = 5,
        SeeCashier = 6,
        Queuing = 7,
        LeftShop = 8
    }

    public bool seeCart, hasCart, seeShelf, frontOfQueue, seeCashier, queuing, paid, fullCart, leftShop;

    public GameObject cart, shelf, cashier;
    
    //cart push not working so temp hold it here 
    public int cartFill = 0;
    
    public ShopManager shopManager;

    /// <summary>
    /// Find closest shop manager. 
    /// </summary>
    public void FindManager()
    {
        ShopManager[] managers = FindObjectsByType<ShopManager>(FindObjectsSortMode.None);
        float currDistance = int.MaxValue; 

        foreach (ShopManager manager in managers)
        {
            if (shopManager is null)
            {
                shopManager = manager;
            }

            if (Vector3.Distance(transform.position, manager.gameObject.transform.position) < currDistance)
            {
                shopManager = manager;
            }
        }
    }
    
    public void FillCart(int amount)
    {
        cartFill += amount;

        if (cartFill >= 5)
        {
            fullCart = true;
        }
    }
    
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(ShoppingScenario.SeeCart, seeCart);
        aWorldState.Set(ShoppingScenario.SeeShelf, seeShelf);
        aWorldState.Set(ShoppingScenario.FrontOfQueue, frontOfQueue);
        aWorldState.Set(ShoppingScenario.SeeCashier, seeCashier);
        aWorldState.Set(ShoppingScenario.Queuing, queuing);
        aWorldState.Set(ShoppingScenario.Paid, paid);
        aWorldState.Set(ShoppingScenario.FullCart, fullCart);
        aWorldState.Set(ShoppingScenario.HasCart, hasCart); 
        aWorldState.Set(ShoppingScenario.LeftShop, leftShop);
    }
}
