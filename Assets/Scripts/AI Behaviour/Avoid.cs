using System;
using UnityEngine;

public class Avoid : MonoBehaviour, ISteering 
{
    [SerializeField] private LayerMask avoidMask;
    [SerializeField] private int avoidRange;
    [SerializeField] private float turnRange;
    [SerializeField] private float slowSpeed;

    private RaycastHit _toAvoid;

    private Vector3 _torque;
    private Vector3 _force;

    private Look _look;

    private void Start()
    {
        _look = GetComponent<Look>();
    }

    public bool IsNeeded()
    {
        foreach (RaycastHit hit in _look.LookAround(avoidMask, avoidRange))
        {
            _toAvoid = hit;
            return true;
        }
        
        return false; 
    }

    public Vector3[] CalculateMovement()
    {
        Debug.Log(Vector3.Angle(_toAvoid.transform.position - transform.position, transform.forward));
        
        //turn away from object 
        if (Vector3.Angle(_toAvoid.transform.position - transform.position, transform.forward) < 90) 
        {
            return new Vector3[]
            {
                new (0, (-turnRange * (avoidRange / _toAvoid.distance)) ,0),
                Vector3.back * (slowSpeed * (avoidRange / _toAvoid.distance))
            }; // turn and slow strength bigger the closer the object is 
        }
        
        return new Vector3[]
        { 
            new (0, turnRange * (avoidRange / _toAvoid.distance) ,0), 
            Vector3.back * (slowSpeed * (avoidRange / _toAvoid.distance))
        };
    }
}
