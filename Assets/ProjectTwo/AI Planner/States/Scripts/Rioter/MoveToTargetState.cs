using Anthill.AI;
using UnityEngine;

public class MoveToTargetState : AntAIState
{
    private GameObject _parent;
    private RioterSensor _sensor;
    private TurnTowards _turnTowards;
    private Look _look;
    private Avoid _avoid;
    
    public override void Create(GameObject go)
    {
        _sensor = go.GetComponent<RioterSensor>();
        _look = go.GetComponent<Look>();
        _turnTowards = go.GetComponent<TurnTowards>();
        _avoid = go.GetComponent<Avoid>();
        _parent = go; 
    }

    public override void Enter()
    {
        Debug.Log("Moving to target");
        _turnTowards.ChangeTarget(_sensor.target.gameObject.transform.position);
        _avoid.AddException(_sensor.target);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if (_sensor.target == null) //if target has been destroyed before reaching 
        {
            _sensor.hasTarget = false;
        }
        
        //check if target in reachable distance 
        foreach (Collider c in _look.CheckReachableDistance())
        {
            if (c == _sensor.target.GetComponent<Collider>())
            {
                _sensor.atTarget = true;
                Finish();
                return;
            }
        }
    }

    public override void Exit()
    {

    }
}
