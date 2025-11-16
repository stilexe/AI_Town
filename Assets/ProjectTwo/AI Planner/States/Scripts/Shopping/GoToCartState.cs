using Anthill.AI;
using UnityEngine;

public class GoToCartState : AntAIState
{
    private GameObject _parent;
    private ShopperSensor _sensor; 
    private TurnTowards _turnTowards;
    private Avoid _avoid; 
    
    public override void Create(GameObject go)
    {
        _parent = go;
        _sensor = go.GetComponent<ShopperSensor>();
        _turnTowards = go.GetComponent<TurnTowards>();
        _avoid = _parent.GetComponent<Avoid>();
    }

    public override void Enter()
    {
        Debug.Log("Going to cart");
        _turnTowards.ChangeTarget(_sensor.cart.transform.position);
        _avoid.AddException(_sensor.cart);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if (Vector3.Distance(_parent.transform.position, _sensor.cart.transform.position) < 6f && _sensor.hasCart != true)
        {
            Debug.Log("Taking cart");
            _avoid.RemoveException(_sensor.cart);
            //_sensor.cart.GetComponent<IInteractable>().Interact(_parent);
            _sensor.hasCart = true;
            _turnTowards.ClearTarget();
            Finish(); 
        }
    }

    public override void Exit()
    {
        
    }
}
