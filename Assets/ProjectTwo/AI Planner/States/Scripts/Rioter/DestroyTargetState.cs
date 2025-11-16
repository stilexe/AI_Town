using System.Collections;
using Anthill.AI;
using UnityEngine;

public class DestroyTargetState : AntAIState
{
    private GameObject _parent;
    private RioterSensor _sensor;
    private TurnTowards _turnTowards;

    private bool _canDestroy;
    
    public override void Create(GameObject go)
    {
        _sensor = go.GetComponent<RioterSensor>();
        _turnTowards = go.GetComponent<TurnTowards>();
        _parent = go;
    }

    public override void Enter()
    {
        _canDestroy = true; 
        
        _sensor.targetDamagable.OnDestroyed += TargetDestroyed;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if (_canDestroy)
        {
            _sensor.targetDamagable.TakeDamage(5);
            StartCoroutine(Cooldown());
        }
    }

    public override void Exit()
    {
        _sensor.targetDamagable.OnDestroyed -= TargetDestroyed;
    }
    
    private IEnumerator Cooldown()
    {
        _canDestroy = false; 
        
        yield return new WaitForSeconds(2);
        
        _canDestroy = true;
    }

    private void TargetDestroyed()
    {
        _sensor.targetDestroyed = true;
        _turnTowards.ClearTarget();
        Finish();
    }
}
