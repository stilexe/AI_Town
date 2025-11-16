using System.Collections;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class PayState : AntAIState
{
    private GameObject _parent;
    private ShopperSensor _sensor;
    private Avoid _avoid;
    private MoveForward _moveForward;

    private Vector3 _queueLocation;
    private Queue _queue;
    
    public override void Create(GameObject go)
    {
        _parent = go;
        _sensor = go.GetComponent<ShopperSensor>();
        _avoid = go.GetComponent<Avoid>();
        _moveForward = go.GetComponent<MoveForward>();
    }

    public override void Enter()
    {
        Debug.Log("Paying");
        _queue = _sensor.cashier.GetComponent<Queue>();
        StartCoroutine(PayDelay());
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        
    }

    public override void Exit()
    {
        
    }

    /// <summary>
    /// Wait a few seconds to pay, put animation here
    /// </summary>
    /// <returns></returns>
    private IEnumerator PayDelay()
    {
        yield return new WaitForSeconds(2f);
        
        _sensor.paid = true;
        _avoid.RemoveException(_sensor.cashier);
        _moveForward.Walking(true);
        
        _queue.NextCustomer();
        
        Finish();
    }
}
