using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowards : MonoBehaviour, ISteering
{
    [SerializeField] private bool activeTarget; 
    [SerializeField] private Vector3 target;
    [SerializeField] private float maxDistanceFromTarget; 
    
    [SerializeField] private float turnSpeed; 
    
    private Vector3 _targetDirection;
    private float _angle;
    private float _speed;
    private Vector3 _torque;
    private Vector3 _force;

    [SerializeField] private bool tethered; 
    [SerializeField] private GameObject tether;
    [SerializeField] private float minDistanceFromTether;

    private bool _pathFollow; 

    private void Start()
    {
        if (tethered)
        {
            target = tether.transform.position;
            activeTarget = true;
        }
    }

    public Vector3[] CalculateMovement()
    {
        if (!activeTarget || tethered && Vector3.Distance(transform.position, target) <= minDistanceFromTether)
        {
            return new Vector3[]
            { 
                Vector3.zero, Vector3.zero
            }; 
        }

        _torque = Vector3.zero;
        _force = Vector3.zero;
        
        //higher speed the further away 
        if (!_pathFollow)
        {
            _speed = turnSpeed * (Vector3.Distance(transform.position, target) / maxDistanceFromTarget); 
        }
        else
        {
            _speed = turnSpeed; 
        }
        
        _targetDirection = (target - transform.position).normalized;
        
        _angle = Vector3.SignedAngle(transform.forward, _targetDirection, transform.up);

        if (Mathf.Abs(_angle) > 5)
        {
            if (_angle >= 0)
            {
                _torque += new Vector3(0, _speed, 0);
            }
            else
            {
                _torque -= new Vector3(0, _speed, 0);
            }

            //higher angles kind of back up a bit 
            if (Mathf.Abs(_angle) > 40)
            {
                _force += Vector3.back * _speed;
            }
        }

        return new Vector3[] {_torque, _force};
    }
    
    public void ChangeTarget(Vector3 newTarget, bool pathPoint = false)
    {
        //Debug.Log("Target changed");
        activeTarget = true;
        _pathFollow = pathPoint;
        target = newTarget;
    }

    public void ClearTarget()
    {
        activeTarget = false;
    }

    public Dictionary<Color, List<Vector3>> LineRenderDisplay()
    {
        return new Dictionary<Color, List<Vector3>>()
        {
            { Color.green, new List<Vector3>()
                {
                    transform.position, target
                }
            },
            
            { Color.blue, new List<Vector3>()
                {
                    transform.position, transform.position + (_targetDirection.normalized * 4)
                }
            }
        };
    }
    
    public List<string> LineRenderDescription()
    {
        return new List<string>()
        {
            "Green: shows where we are trying to rotate to face the location",
            "Blue: connects to the location we are turning towards",
        };
    }

    private void OnDrawGizmos()
    {
        if (!activeTarget) return;

        Gizmos.color = Color.blue;
        
        Gizmos.DrawWireSphere(target, maxDistanceFromTarget);
        Gizmos.DrawLine(transform.position, target); 
    }
}
