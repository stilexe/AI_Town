using System;
using System.Collections;
using UnityEngine;

public class TurnTowards : MonoBehaviour
{
    [SerializeField] private bool activeTarget; 
    [SerializeField] private Vector3 target;
    [SerializeField] private float minDistanceFromTarget;
    [SerializeField] private float maxDistanceFromTarget; 

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float turnSpeed; 
    
    private Vector3 _targetDirection;
    private float _angle;
    private float _speed;

    [SerializeField] private bool tethered; 
    [SerializeField] private GameObject tether;

    private bool _pathFollow; 

    private void Start()
    {
        if (tethered)
        {
            target = tether.transform.position;
        }
    }

    private void FixedUpdate()
    {
        // if tethered and closer to the target than the minimum distance, return  
        if (!activeTarget || tethered && Vector3.Distance(transform.position, target) <= minDistanceFromTarget)
        {
            return; 
        }
        
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
                rb.AddRelativeTorque(0, _speed, 0);
            }
            else
            {
                rb.AddRelativeTorque(0, -_speed, 0);
            }

            //higher angles kind of back up a bit 
            if (Mathf.Abs(_angle) > 40)
            {
                rb.AddRelativeForce(Vector3.back * _speed);
            }
        }
    }

    public void ChangeTarget(Vector3 newTarget, bool pathPoint = false)
    {
        if (!activeTarget)
        {
            activeTarget = true;
        }

        _pathFollow = pathPoint;
        
        target = newTarget;
    }

    public void ClearTarget()
    {
        activeTarget = false;
    }

    private void OnDrawGizmos()
    {
        if (!activeTarget) return;

        Gizmos.color = Color.blue;
        
        Gizmos.DrawWireSphere(target, minDistanceFromTarget);
        Gizmos.DrawLine(transform.position, target); 
    }
}
