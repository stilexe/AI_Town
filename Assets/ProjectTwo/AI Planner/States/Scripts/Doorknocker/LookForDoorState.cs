using Anthill.AI;
using UnityEngine;

public class LookForDoorState : AntAIState
{
    private GameObject _parent;
    private DoorknockerSensor _sensor;
    
    public override void Create(GameObject go)
    {
        _sensor = go.GetComponent<DoorknockerSensor>();
        _parent = go; 
    }

    public override void Enter()
    {
        Debug.Log("Looking for door.");

        if (_sensor.seesDoor) //back around 
        {
            _sensor.seesDoor = false;
            _sensor.atDoor = false;
            _sensor.knocked = false; 
        }
        
        GameObject newDoor = Town.Instance.RandomDoor();

        if (_sensor is null)
        {
            Debug.Log("No sensor found.");
            return;
        }

        while (newDoor == _sensor.door) //dont want the same door 
        {
            newDoor = Town.Instance.RandomDoor();
        }
        
        _sensor.door = newDoor;
        _sensor.seesDoor = true;
        
        Finish();
        
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        
    }

    public override void Exit()
    {
        
    }
}
