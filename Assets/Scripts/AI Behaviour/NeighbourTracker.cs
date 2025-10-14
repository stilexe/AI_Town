using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class NeighbourTracker : MonoBehaviour, ISteering
{
    private Collider[] _neighbourColliders;
    private List<Collider> _neighbours;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private int maxNeighbours;
    [SerializeField] private float searchRadius;
    [SerializeField] private LayerMask charLayer;
    [SerializeField] private float eyeLevel;

    //[SerializeField] private bool align;
    [SerializeField] private float alignForce;
    private Vector3 _alignDirection;
    
    //[SerializeField] private bool separate;
    [SerializeField] private float separateForce;
    private Vector3 _separateDirection;
    
    //[SerializeField] private bool cohesion;
    [SerializeField] private float cohesionForce;
    private Vector3 _cohesionDirection;

    private Vector3 _torque;
    private Vector3 _force; 

    private void Start()
    {
        _neighbourColliders = new Collider[maxNeighbours];
    }

    public bool IsNeeded()
    {
        _neighbourColliders = Physics.OverlapSphere(transform.position + (transform.forward * .2f), searchRadius, charLayer);

        foreach (Collider c in _neighbourColliders)
        {
            if (c == GetComponent<Collider>()) continue; 
            
            //check each neighbour with raycast at eye level to see if they are blocked, remove if they are 
            
            Debug.DrawLine(transform.position + (Vector3.up * .5f), c.transform.position + (Vector3.up * .5f), Color.yellow);
        }

        if (_neighbourColliders.Length <= 1)
        {
            return false;
        }

        return true;
    }

    public Vector3[] CalculateMovement()
    {
        _torque = Vector3.zero;
        _force = Vector3.zero;
        
        // _alignDirection = Vector3.zero;
        // _separateDirection = Vector3.zero;
        // _cohesionDirection = Vector3.zero;

        foreach (Collider c in _neighbourColliders)
        {
            if (c == GetComponent<Collider>()) continue;
            
            _alignDirection = c.transform.forward;
            _separateDirection = -(c.transform.position - transform.position).normalized * 
                                  (searchRadius - Vector3.Distance(c.transform.position, transform.position)); //higher force closer to neighbour
            _cohesionDirection = (c.transform.position - transform.position).normalized *  
                (Vector3.Distance(c.transform.position, transform.position) / searchRadius); //higher force further from neighbour
        }
        
        _alignDirection /= (_neighbourColliders.Length - 1);
        _separateDirection /= (_neighbourColliders.Length - 1);
        _cohesionDirection /= (_neighbourColliders.Length - 1);
        
        Debug.DrawRay(transform.position + (Vector3.up * .5f), _alignDirection, Color.green);
        Debug.DrawRay(transform.position + (Vector3.up * .5f), _separateDirection, Color.red);
        Debug.DrawRay(transform.position + (Vector3.up * .5f), _cohesionDirection, Color.blue);

        _torque += Vector3.Cross(transform.forward, _alignDirection) * alignForce;
        _force += _separateDirection * separateForce;
        _force += _cohesionDirection * cohesionForce;
        
        // rb.AddTorque(Vector3.Cross(transform.forward, _alignDirection) * alignForce);
        // rb.AddForce(_separateDirection * separateForce);
        // rb.AddForce(_cohesionDirection * cohesionForce);
        
        return new Vector3[] { _torque, _force };
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta; 
        
        Gizmos.DrawWireSphere(transform.position + (transform.forward * .3f), searchRadius);
    }
}
