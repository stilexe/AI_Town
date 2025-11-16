using Anthill.AI;
using UnityEngine;

public class SearchForTargetState : AntAIState
{
    private GameObject _parent;
    private RioterSensor _sensor;
    private Look _look;
    
    public override void Create(GameObject go)
    {
        _sensor = go.GetComponent<RioterSensor>();
        _look = go.GetComponent<Look>();
        _parent = go; 
    }

    public override void Enter()
    {
        Debug.Log("Searching for target");

        if (_sensor.hasTarget) //reset if we've looped back around
        {
            _sensor.hasTarget = false;
            _sensor.targetDestroyed = false;
            _sensor.atTarget = false;
        }
    }
    
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if (_sensor.hasTarget) return; //it was still running after it was done
        
        // look for item with damageable interface 
        foreach (RaycastHit hit in _look.LookAround())
        {
            if (hit.collider.gameObject.TryGetComponent(out IDamageable d))
            {
                Debug.Log("Found damagable");
                _sensor.target = hit.collider.gameObject;
                _sensor.targetDamagable = d;
                _sensor.hasTarget = true;
                Finish();
            }
        }
    }

    public override void Exit()
    {

    }
}
