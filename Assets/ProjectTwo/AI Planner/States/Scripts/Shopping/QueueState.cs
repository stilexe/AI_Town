using Anthill.AI;
using UnityEngine;

public class QueueState : AntAIState
{
    private TurnTowards _turnTowards;
    private GameObject _parent;
    private ShopperSensor _sensor;
    private MoveForward _moveForward; 
    
    private Queue _queue;

    private Vector3 _queueLocation;
    private bool _inPlace; 
    
    public override void Create(GameObject go)
    {
        _parent = go;
        _sensor = go.GetComponent<ShopperSensor>();
        _turnTowards = go.GetComponent<TurnTowards>();
        _moveForward = go.GetComponent<MoveForward>();
    }

    public override void Enter()
    {
        Debug.Log("Queuing");

        _queue = _sensor.cashier.GetComponent<Queue>();
        _queueLocation = _queue.GetQueuePosition(gameObject);

        _turnTowards.ChangeTarget(_queueLocation);

        _queue.OnCustomerServed += NewPosition;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if (!_inPlace && Vector3.Distance(_parent.transform.position, _queueLocation) < 1f)
        {
            _inPlace = true;
            _moveForward.Walking(false);

            if (_queue.FrontOfLine(gameObject))
            {
                _sensor.frontOfQueue = true;
                Finish();
            }
        }
    }

    public override void Exit()
    {
        _queue.OnCustomerServed -= NewPosition;
    }

    private void NewPosition()
    {
        _queueLocation = _queue.GetQueuePosition(gameObject);
        
        _turnTowards.ChangeTarget(_queueLocation);
        
        _moveForward.Walking(true);
        
        _inPlace = false;
    }
}
