using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class NeighbourTracker : MonoBehaviour
{
    private Collider[] _neighbourColliders;
    private List<Collider> _neighbours;

    [SerializeField] private Rigidbody rb; 
    [SerializeField] private int maxNeighbours;
    [SerializeField] private float searchRadius;
    [SerializeField] private LayerMask charLayer;
    [SerializeField] private float eyeLevel;

    [SerializeField] private bool align;
    [SerializeField] private float alignForce;
    private Vector3 _alignDirection;
    [SerializeField] private bool separate;
    [SerializeField] private float separateForce;
    private Vector3 _separateDirection;

    private void Start()
    {
        _neighbourColliders = new Collider[maxNeighbours];
    }

    private void FixedUpdate()
    {
        _neighbourColliders = Physics.OverlapSphere(transform.position + (transform.forward * .2f), searchRadius, charLayer);

        foreach (Collider c in _neighbourColliders)
        {
            if (c == GetComponent<Collider>()) break; 
            
            //check each neighbour with raycast at eye level to see if they are blocked, remove if they are 
            
            Debug.DrawLine(transform.position + (Vector3.up * .5f), c.transform.position + (Vector3.up * .5f), Color.yellow);
        }

        if (_neighbourColliders.Length == 1) return; 

        //todo: maybe neaten all this up but for now seperate functions for each so its easier to know whats going on 
        if(align) Align(_neighbourColliders);
        if(separate) Separate(_neighbourColliders);
    }

    private void Align(Collider[] neighbours)
    {
        _alignDirection = Vector3.zero;

        foreach (Collider c in neighbours)
        {
            _alignDirection += c.transform.forward;
        }
        
        _alignDirection /= neighbours.Length;
        
        Debug.DrawRay(transform.position + (Vector3.up * .5f), _alignDirection, Color.green);

        rb.AddTorque(Vector3.Cross(transform.forward, _alignDirection) * alignForce);
    }

    private void Separate(Collider[] neighbours)
    {
        _separateDirection = Vector3.zero;

        foreach (Collider c in neighbours)
        {
            _separateDirection += (c.transform.position - transform.position).normalized * ((searchRadius / 2) - Vector3.Distance(c.transform.position, transform.position));
        }
        
        _separateDirection /= neighbours.Length;
        
        Debug.DrawRay(transform.position + (Vector3.up * .5f), _separateDirection, Color.red);
        
        rb.AddForce(_separateDirection * separateForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta; 
        
        Gizmos.DrawWireSphere(transform.position + (transform.forward * .3f), searchRadius);
        
        if(_neighbours == null) return; 
        
        foreach (Collider c in _neighbours)
        {
            Gizmos.color = Color.yellow;

            //Gizmos.DrawWireSphere(c.transform.position, .5f); 
        }
    }
}
