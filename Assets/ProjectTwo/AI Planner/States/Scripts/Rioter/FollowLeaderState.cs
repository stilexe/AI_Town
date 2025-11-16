using Anthill.AI;
using UnityEngine;

public class FollowLeaderState : AntAIState
{
    private GameObject _parent;
    private RioterSensor _sensor;
    
    public override void Create(GameObject go)
    {
        _sensor = go.GetComponent<RioterSensor>();
        _parent = go; 
    }

    public override void Enter()
    {

    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        
    }

    public override void Exit()
    {

    }
}
