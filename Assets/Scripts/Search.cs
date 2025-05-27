using System;
using UnityEngine;

/*public class Search : MonoBehaviour
{
    [SerializeField] private int maxRays; 
    
    private float _rayAngle;
    private Vector3 _startDirection; 
    
    
    private void FixedUpdate()
    {
        _rayCount = maxRays; //TODO: change the number of rays based on distance from camera (maybe) 
        _rayAngle = 0;
        _startDirection = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward;

        for (int i = 0; i < _rayCount; i++)
        {
            if (Physics.Raycast(transform.position, Quaternion.Euler(0, _rayAngle, 0) * _startDirection, out RaycastHit hit, avoidRange,
                    observableMask))
            {
                Debug.Log(name + "raycast hit" + hit.collider.name);
            
                rb.AddRelativeTorque(0, turnRange * (avoidRange / hit.distance) ,0); // turn strength bigger the closer the object is 
                
                return;
            }

            _rayAngle += fieldOfView / _rayCount; 
        }
    }
}*/
