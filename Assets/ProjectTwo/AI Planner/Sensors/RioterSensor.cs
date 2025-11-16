using Anthill.AI;
using UnityEngine;

public class RioterSensor : MonoBehaviour, ISense
{
    public enum RioterScenario
    {
        HasTarget = 0,
        TargetDestroyed = 1,
        AtTarget = 2
    }

    public bool hasTarget, targetDestroyed, atTarget;
    
    public GameObject target;
    public IDamageable targetDamagable;

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(RioterScenario.HasTarget, hasTarget);
        aWorldState.Set(RioterScenario.TargetDestroyed, targetDestroyed);
        aWorldState.Set(RioterScenario.AtTarget, atTarget);
    }
}
