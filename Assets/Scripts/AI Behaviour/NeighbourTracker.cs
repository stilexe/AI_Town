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
    
    [SerializeField] private bool cohesion;
    [SerializeField] private float cohesionForce;
    private Vector3 _cohesionDirection;

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

        if (_neighbourColliders.Length <= 1) return; 

        //todo: maybe neaten all this up but for now seperate functions for each so its easier to know whats going on 
        if(align) Align(_neighbourColliders);
        if(separate) Separate(_neighbourColliders);
        if(cohesion) Cohese(_neighbourColliders);
    }

    private void Align(Collider[] neighbours)
    {
        _alignDirection = Vector3.zero;

        foreach (Collider c in neighbours)
        {
            if (c == GetComponent<Collider>()) continue;
            
            _alignDirection += c.transform.forward;
        }
        
        _alignDirection /= (neighbours.Length - 1);
        
        Debug.DrawRay(transform.position + (Vector3.up * .5f), _alignDirection, Color.green);

        rb.AddTorque(Vector3.Cross(transform.forward, _alignDirection) * alignForce);
    }

    private void Separate(Collider[] neighbours)
    {
        _separateDirection = Vector3.zero;

        foreach (Collider c in neighbours)
        {
            if(c == GetComponent<Collider>()) continue;
            
            //direction to each neighbour times by 75% of the search radius take the distance to the neighbour so stronger the closer they are
            _separateDirection += (c.transform.position - transform.position).normalized * ((searchRadius * .75f) - Vector3.Distance(c.transform.position, transform.position));
        }
        
        _separateDirection /= (neighbours.Length - 1);
        
        Debug.DrawRay(transform.position + (Vector3.up * .5f), _separateDirection, Color.red);
        
        rb.AddForce(-_separateDirection * separateForce);
    }

    private void Cohese(Collider[] neighbours)
    {
        _cohesionDirection = Vector3.zero;

        foreach (Collider c in neighbours)
        {
            if(c == GetComponent<Collider>()) continue;
            
            //times by percentage distance is of max distance stronger the further away they are 
            _cohesionDirection += (c.transform.position - transform.position).normalized *  Vector3.Distance(c.transform.position, transform.position) / ((searchRadius * .75f));
        }
        
        _cohesionDirection /= (neighbours.Length - 1);
        
        Debug.DrawRay(transform.position + (Vector3.up * .5f), _cohesionDirection, Color.blue);
        
        rb.AddForce(_cohesionDirection * cohesionForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta; 
        
        //Gizmos.DrawWireSphere(transform.position + (transform.forward * .3f), searchRadius);
    }
}
