using System;
using System.Collections;
using UnityEngine;

public class TurnTowards : MonoBehaviour
{
    public Vector3 target;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float turnSpeed; 
    
    private Vector3 _targetDirection;
    private float _angle;
    private float _speed;

    [SerializeField] private bool tethered; 
    [SerializeField] private GameObject tether;
    [SerializeField] private float minTetherDistance;
    [SerializeField] private float maxTetherDistance;

    private void FixedUpdate()
    {
        if (tethered)
        {
            if (Vector3.Distance(transform.position, tether.transform.position) > minTetherDistance)
            {
                //faster turn the further away 
                _speed = turnSpeed * (Vector3.Distance(transform.position, tether.transform.position) / maxTetherDistance);
            }
            else
            {
                return;
            }
            
            _targetDirection = (tether.transform.position - transform.position).normalized;
        }
        else // not tethered 
        {
            _speed = turnSpeed * (Vector3.Distance(transform.position, target)) / 100; 
            _targetDirection = (target - transform.position).normalized;
        }
        
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

            if (Mathf.Abs(_angle) > 40)
            {
                rb.AddRelativeForce(Vector3.back * _angle / 40);
            }
        }
    }

    public void ChangeTarget(Vector3 newTarget)
    {
        target = newTarget;
    }
}
