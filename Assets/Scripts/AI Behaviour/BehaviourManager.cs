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
        foreach (Component c in GetComponents(typeof(ISteering)))
        {
            _behaviours.Add(c as ISteering);
        }
    }

    private void FixedUpdate()
    {
        _torque = Vector3.zero;
        _force = Vector3.zero;
        
        foreach (ISteering behaviour in _behaviours)
        {
            if(!behaviour.IsNeeded()) continue;
            
            _torFor = behaviour.CalculateMovement();

            _torque += _torFor[0];
            _force += _torFor[1];
        }

        rb.AddRelativeForce(_force);
        rb.AddRelativeTorque(_torque);

    }
}
