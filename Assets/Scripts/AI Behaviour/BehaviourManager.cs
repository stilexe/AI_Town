using System;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    private List<ISteering> _behaviours = new List<ISteering>();

    [SerializeField] private Rigidbody rb;

    private Vector3 _torque, _force; 
    private Vector3[] _torFor;

    private void OnEnable()
    {
        foreach (ISteering s in GetComponents<ISteering>())
        {
            _behaviours.Add(s);
        }
    }

    private void FixedUpdate()
    {
        _torque = Vector3.zero;
        _force = Vector3.zero;
        
        foreach (ISteering behaviour in _behaviours)
        {
            _torFor = behaviour.CalculateMovement();

            _torque += _torFor[0];
            _force += _torFor[1];
        }

        rb.AddRelativeForce(_force);
        rb.AddRelativeTorque(_torque);

    }
}
