using Anthill.AI;
using UnityEngine;

public class ShoppingState : AntAIState
{
    private GameObject _parent; 
    
    public override void Create(GameObject go)
    {
        _parent = transform.parent.gameObject;
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
