using System;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MonoBehaviour, ISteering 
{
    [SerializeField] private LayerMask avoidMask;
    [SerializeField] private int avoidRange;
    [SerializeField] private float turnRange;
    [SerializeField] private float slowSpeed;
    [SerializeField] private float maxDropHeight;
    
    private Vector3 _avoidLocation;
    private float _avoidDistance;

    private Vector3 _turnDirection;

    private Look _look;

    private void Start()
    {
        _look = GetComponent<Look>();
    }

    public Vector3[] CalculateMovement()
    {
        if (_look == null)
        {
            return new Vector3[]
            { 
                Vector3.zero, Vector3.zero
            };
        }
        
        _avoidLocation = Vector3.zero;

        foreach (RaycastHit hit in _look.EdgeCheck())
        {
            if (hit.collider is null)
            {
                //Debug.Log("Avoiding cliff");

                _avoidLocation = transform.position + (transform.forward * 2);
                _avoidDistance = Vector3.Distance(_avoidLocation, transform.position);
            
            }
        }
        
        foreach (RaycastHit hit in _look.LookAround(avoidMask, avoidRange))
        {
            _avoidLocation = hit.point;
            _avoidDistance = hit.distance;
        }

        if (_avoidLocation == Vector3.zero)
        {
            return new Vector3[]
            { 
                Vector3.zero, Vector3.zero
            };
        }
        
        //turn away from object 
        // turn and slow strength bigger the closer the object is 
        if (Vector3.Angle(_avoidLocation - transform.position, transform.forward) < 90)
        {
            _turnDirection = new(0, -turnRange * (avoidRange / _avoidDistance), 0);
        }
        else
        {
            _turnDirection = new(0, turnRange * (avoidRange / _avoidDistance), 0);
        }
        
        return new Vector3[]
        { 
            _turnDirection, 
            Vector3.back * (slowSpeed * (avoidRange / _avoidDistance))
        };
    }

    public Dictionary<Color, List<Vector3>> LineRenderDisplay()
    {
        if (_avoidLocation == Vector3.zero)
        {
            return new Dictionary<Color, List<Vector3>>();
        }
        
        return new Dictionary<Color, List<Vector3>>()
        {
            { Color.red, new List<Vector3>()
                {
                    transform.position, _avoidLocation
                }
            },
            
            { Color.blue, new List<Vector3>()
                {
                    transform.position, transform.position + (_turnDirection * 4)
                }
            }
        };
    }
}
