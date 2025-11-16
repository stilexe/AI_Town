using System.Collections;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class GoToShelfState : AntAIState
{
    private Look _look;
    private TurnTowards _turnTowards;
    private MoveForward _moveForward;
    private GameObject _parent;
    private ShopperSensor _sensor;
    private Avoid _avoid;

    private bool _picked; //if theyve grabbed something yet 
    
    public override void Create(GameObject go)
    {
        _parent = go;
        _sensor = go.GetComponent<ShopperSensor>();
        _turnTowards = go.GetComponent<TurnTowards>();
        _avoid = go.GetComponent<Avoid>();
        _moveForward = go.GetComponent<MoveForward>();
    }

    public override void Enter()
    {
        Debug.Log("Going to shelf");
        _turnTowards.ChangeTarget(_sensor.shelf.transform.position);
        _avoid.AddException(_sensor.shelf);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if (Vector3.Distance(_parent.transform.position, _sensor.shelf.transform.position) < 3.5f) //and not picked eventually 
        {
            StartCoroutine(GetFromShelf());
        }
    }

    public override void Exit()
    {
        _moveForward.Walking(true); //make sure it starts walking again 
    }

    /// <summary>
    /// Stop moving and wait a bit to emulate taking something from shelf, probably put animation in here. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetFromShelf()
    {
        Debug.Log("Getting from shelf");

        _picked = true; 
        
        _moveForward.Walking(false);
        
        yield return new WaitForSeconds(2f);
        
        _sensor.FillCart(1);
        Debug.Log("Items in cart " + _sensor.cartFill);
        
        _sensor.seeShelf = false;
        _picked = false;
        _turnTowards.ClearTarget();
        _avoid.RemoveException(_sensor.shelf);
        
        _moveForward.Walking(true);

        Finish();

        // if (_sensor.fullCart)
        // {
        //     Finish();
        // }
    }
}
