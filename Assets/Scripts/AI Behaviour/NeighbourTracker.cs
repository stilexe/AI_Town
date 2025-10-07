using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeighbourTracker : MonoBehaviour
{
    private Collider[] _neighbourColliders;
    private List<GameObject> _neighbours = new List<GameObject>();

    [SerializeField] private int maxNeighbours;
    [SerializeField] private float searchRadius;
    [SerializeField] private LayerMask charLayer;
    [SerializeField] private float eyeLevel;

    private void Start()
    {
        _neighbourColliders = new Collider[maxNeighbours];
    }

    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, searchRadius, _neighbourColliders, charLayer);

        foreach (Collider c in _neighbourColliders)
        {
            if (!c.gameObject == this.gameObject)
            {
                _neighbours.Add(c.gameObject);
            }
        }
        
        //check each neighbour with raycast at eye level to see if they are blocked, remove if they are 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta; 
        
        Gizmos.DrawWireSphere(transform.position, searchRadius);
        
        if(_neighbours == null) return; 
        
        foreach (GameObject n in _neighbours)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(n.transform.position, .5f);
        }
    }
}
