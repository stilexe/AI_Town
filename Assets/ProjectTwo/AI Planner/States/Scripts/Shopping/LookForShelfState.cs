using Anthill.AI;
using UnityEngine;

public class LookForShelfState : AntAIState
{
    private Look _look;
    private MoveForward _moveForward;
    private GameObject _parent;
    private ShopperSensor _sensor; 

    public override void Create(GameObject go)
    {
        _look = go.GetComponent<Look>();
        _moveForward = go.GetComponent<MoveForward>();
        _sensor = go.GetComponent<ShopperSensor>();
        _parent = go;
    }

    public override void Enter()
    {
        Debug.Log("Looking for shelf");
        
        _moveForward.NewSpeed(0.7f); //slow down
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        foreach (RaycastHit hit in _look.LookAround())
        {
            if (hit.transform.gameObject.CompareTag("Shopping Shelf") && hit.transform.gameObject != _sensor.shelf)
            {
                Debug.Log("Found shelf");
                
                _sensor.shelf = hit.transform.gameObject;
                _sensor.seeShelf = true;
                Finish();
            }
        }
    }

    public override void Exit()
    {
        _moveForward.NewSpeed(1);
    }
}
