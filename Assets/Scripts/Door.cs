using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _exitPoint;
    [SerializeField] private GameObject _entryPoint;

    [SerializeField] private AntAIScenario _entryScenario;
    [SerializeField] private AntAIScenario _exitScenario;
    
    public void Interact(GameObject opening)
    {
        if (Vector3.Distance(_exitPoint.transform.position, opening.transform.position) <
            Vector3.Distance(_entryPoint.transform.position, opening.transform.position)) //if exit point is closer they are exiting
        {
            opening.transform.position = _entryPoint.transform.position;
            
            //testing, eventually their scenario change depending on where they are 
            Destroy(opening);

            // if (opening.TryGetComponent(out AntAIAgent agent)) //if the opener is an ai agent change the scenario to the appropriate one
            // {
            //     agent.scenario = _entryScenario;
            // }
        }
        else
        {
            opening.transform.position = _exitPoint.transform.position;
            
            if (opening.TryGetComponent(out AntAIAgent agent))
            {
                agent.scenario = _exitScenario;
            }
        }
    }
}
