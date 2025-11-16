using Anthill.AI;
using UnityEngine;

public class LeaveStoreState : AntAIState
{
    private GameObject _parent;
    private ShopperSensor _sensor;
    private TurnTowards _turnTowards;
    private Avoid _avoid;

    private GameObject _door;

    public override void Create(GameObject go)
    {
        _sensor = go.GetComponent<ShopperSensor>();
        _turnTowards = go.GetComponent<TurnTowards>();
        _avoid = go.GetComponent<Avoid>();
        _parent = go;
    }

    public override void Enter()
    {
        Debug.Log("Leaving store");
        
        if (_sensor.shopManager is null)
        {
            _sensor.FindManager();
        }

        _door = _sensor.shopManager.Door();
        _turnTowards.ChangeTarget(_door.transform.position);
        _avoid.AddException(_door);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if (_door != null && Vector3.Distance(transform.position, _door.transform.position) < 5f)
        {
            Debug.Log("Exiting");
            
            _sensor.leftShop = true;
            _door.GetComponent<IInteractable>().Interact(_parent);
            Finish();
        }
    }

    public override void Exit()
    {
       
    }
}
