using System;
using UnityEngine;

public class Avoid : MonoBehaviour, ISteering 
{
    [SerializeField] private LayerMask observableMask;
    [SerializeField] private float avoidRange;
    [SerializeField] private float turnRange;
    [SerializeField] private float fieldOfView; 
    [SerializeField] private int maxRays;
    [SerializeField] private float slowSpeed;

    private float _rayAngle;
    private int _rayCount;
    private Vector3 _startDirection;

    private Vector3 _torque;
    private Vector3 _force;

    public bool IsNeeded()
    {
        return true; 
    }

    public Vector3[] CalculateMovement()
    {
        _torque = Vector3.zero;
        _force = Vector3.zero;
        
        // if (rb.linearVelocity.magnitude < .4)
        // {
        //     rb.AddRelativeTorque(0, turnRange ,0);
        // }

        _rayCount = maxRays; //TODO: change the number of rays based on distance from camera (maybe) 
        _rayAngle = 0;
        _startDirection = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward;

        for (int i = 0; i < _rayCount; i++)
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(0, _rayAngle, 0) * _startDirection, Color.cyan);
            
            if (Physics.Raycast(transform.position, Quaternion.Euler(0, _rayAngle, 0) * _startDirection, out RaycastHit hit, avoidRange,
                    observableMask))
            {
                //Debug.Log(name + "raycast hit" + hit.collider.name);

                //more back force closer the hit is 
                _force = Vector3.back * (slowSpeed); //todo: smaller hit distance bigger push back maths 

                //turn away from object 
                if (i > _rayCount / 2) //go right
                {
                    _torque -= new Vector3(0, turnRange * (avoidRange / hit.distance) ,0); // turn strength bigger the closer the object is 
                }
                else  //go left 
                {
                    _torque += new Vector3(0, turnRange * (avoidRange / hit.distance) ,0);
                }

                break;
            }

            _rayAngle += fieldOfView / _rayCount; 
        }
        
        return new Vector3[]{_torque, _force}; 
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.cyan;
    //     Gizmos.DrawWireSphere(transform.position, avoidRange);
    // }
}
