using Anthill.AI;
using UnityEngine;

public class LookForCartState : AntAIState
{
    private GameObject _parent;
    private ShopperSensor _sensor; 

    public override void Create(GameObject go)
    {
        _sensor = go.GetComponent<ShopperSensor>();
        _parent = go;
    }

    public override void Enter()
    {
        if (_sensor.shopManager is null)
        {
            _sensor.FindManager();
        }

        _sensor.cart = _sensor.shopManager.CartHolder();
        _sensor.seeCart = true;
        Finish();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        
    }

    public override void Exit()
    {
       
    }
}
