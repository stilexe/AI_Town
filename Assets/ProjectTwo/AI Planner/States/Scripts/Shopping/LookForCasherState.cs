using Anthill.AI;
using UnityEngine;

public class LookForCasherState : AntAIState
{
    private Look _look;
    private TurnTowards _turnTowards;
    private GameObject _parent;
    private ShopperSensor _sensor;
    private Avoid _avoid; 
    
    public override void Create(GameObject go)
    {
        _parent = go;
        _sensor = go.GetComponent<ShopperSensor>();
        _turnTowards = go.GetComponent<TurnTowards>();
        _look = go.GetComponent<Look>();
        _avoid = go.GetComponent<Avoid>();
    }

    public override void Enter()
    {
        Debug.Log("Looking for checkout");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if (_sensor.shopManager is null)
        {
            _sensor.FindManager();
        }
        
        _sensor.cashier = _sensor.shopManager.CashRegister();
        _sensor.seeCashier = true;
        _avoid.AddException(_sensor.cashier);
        Finish();
    }

    public override void Exit()
    {
        
    }
}
