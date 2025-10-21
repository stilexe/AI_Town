using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
    
    public void Interact(GameObject opening)
    {
        if (Vector3.Distance(spawnPoints[0].transform.position, opening.transform.position) <
            Vector3.Distance(spawnPoints[1].transform.position, opening.transform.position))
        {
            opening.transform.position = spawnPoints[1].transform.position;
        }
        else
        {
            opening.transform.position = spawnPoints[0].transform.position;
        }
    }
}
