using Anthill.AI;
using UnityEngine;

public class GoToDoorState : AntAIState
{
    private GameObject _parent;
    private PathFollow _pathFollow;
    private DoorknockerSensor _sensor;
    
    public override void Create(GameObject go)
    {
        _pathFollow = go.GetComponent<PathFollow>();
        _sensor = go.GetComponent<DoorknockerSensor>();
        _parent = go; 
    }

    public override void Enter()
    {
        Debug.Log("Going to door.");
        _pathFollow.ReachedPathEndEvent += ReachDoor;
        
        _pathFollow.SetPath(_sensor.door.transform.position);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        
    }

    public override void Exit()
    {
        _pathFollow.ReachedPathEndEvent -= ReachDoor;
    }
    
    private void ReachDoor()
    {
        Debug.Log("Reached door");
        
        _sensor.atDoor = true;
        
        Finish();
    }
}
