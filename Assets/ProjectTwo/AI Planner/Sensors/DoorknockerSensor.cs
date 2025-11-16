using Anthill.AI;
using UnityEngine;

public class DoorknockerSensor : MonoBehaviour, ISense
{
    public enum DoorknockerScenario
    {
        AtDoor = 0,
        SeesDoor = 1,
        Knocked = 2
    }

    public bool atDoor, seesDoor, knocked;
    public GameObject door; 
    
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(DoorknockerScenario.AtDoor, atDoor);
        aWorldState.Set(DoorknockerScenario.SeesDoor, seesDoor);
        aWorldState.Set(DoorknockerScenario.Knocked, knocked);
    }
}
