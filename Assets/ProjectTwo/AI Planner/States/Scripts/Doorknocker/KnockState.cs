using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class KnockState : AntAIState
{
    private GameObject _parent;
    private DoorknockerSensor _sensor;
    private MoveForward _moveForward;
    
    public override void Create(GameObject go)
    {
        _moveForward = go.GetComponent<MoveForward>();
        _sensor = go.GetComponent<DoorknockerSensor>();
        _parent = go; 
    }

    public override void Enter()
    {
        Debug.Log("Knocking");
        
        _moveForward.Walking(false);

        StartCoroutine(WaitForAnswer());
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        
    }

    public override void Exit()
    {
        
    }

    private IEnumerator WaitForAnswer()
    {
        yield return new WaitForSeconds(5f);

        _sensor.knocked = true; 
        
        _moveForward.Walking(true);

        Finish();
    }
}
