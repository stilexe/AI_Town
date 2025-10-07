using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Avoid : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private LayerMask observableMask;
    [SerializeField] private float avoidRange;
    [SerializeField] private float turnRange;
    [SerializeField] private float fieldOfView; 
    [SerializeField] private int maxRays;
    [SerializeField] private float slowSpeed;

    private float _rayAngle;
    private int _rayCount;
    private Vector3 _startDirection;

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude < .4)
        {
            rb.AddRelativeTorque(0, turnRange ,0);
        }

        _rayCount = maxRays; //TODO: change the number of rays based on distance from camera (maybe) 
        _rayAngle = 0;
        _startDirection = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward;

        for (int i = 0; i < _rayCount; i++)
        {
            if (Physics.Raycast(transform.position, Quaternion.Euler(0, _rayAngle, 0) * _startDirection, out RaycastHit hit, avoidRange,
                    observableMask))
            {
                //Debug.Log(name + "raycast hit" + hit.collider.name);

                //more back force closer the hit is 
                rb.AddRelativeForce(Vector3.back * (slowSpeed)); //todo: smaller hit distance bigger push back maths 

                if (i > _rayCount / 2) // saw object on the left  
                {
                    rb.AddRelativeTorque(0, -turnRange * (avoidRange / hit.distance) ,0); // turn strength bigger the closer the object is 
                }
                else //saw object on the right 
                {
                    rb.AddRelativeTorque(0, turnRange * (avoidRange / hit.distance) ,0);
                }
                
                return;
            }

            _rayAngle += fieldOfView / _rayCount; 
        }
    }
}
