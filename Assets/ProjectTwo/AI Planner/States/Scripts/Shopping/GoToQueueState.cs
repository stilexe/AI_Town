using Anthill.AI;
using UnityEngine;

public class GoToQueueState : AntAIState
{
    private TurnTowards _turnTowards;
    private GameObject _parent;
    private ShopperSensor _sensor;

    private Vector3 _queueLocation;
    private Queue _queue;
    
    public override void Create(GameObject go)
    {
        _parent = go;
        _sensor = go.GetComponent<ShopperSensor>();
        _turnTowards = go.GetComponent<TurnTowards>();
    }

    public override void Enter()
    {
        Debug.Log("Joining Queue");

        _queueLocation = _sensor.cashier.GetComponent<Queue>().GetQueuePosition(gameObject);

        _turnTowards.ChangeTarget(_queueLocation); 
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if (Vector3.Distance(_parent.transform.position, _queueLocation) < 1f && _sensor.queuing != true)
        {
            _sensor.queuing = true;
            Finish(); 
        }
    }

    public override void Exit()
    {
        
    }
}
