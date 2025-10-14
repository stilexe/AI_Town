using System;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField] private float fieldOfView; 
    [SerializeField] private int maxRays;

    private float _rayAngle;
    private int _rayCount;
    private Vector3 _startDirection;

    public List<RaycastHit> LookAround(LayerMask observableMask, int observableRange)
    {
        List<RaycastHit> inView = new List<RaycastHit>();
        
        _rayCount = maxRays; //TODO: change the number of rays based on distance from camera (maybe) 
        _rayAngle = 0;
        _startDirection = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward;

        for (int i = 0; i < _rayCount; i++)
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(0, _rayAngle, 0) * _startDirection, Color.cyan);
            
            if (Physics.Raycast(transform.position, Quaternion.Euler(0, _rayAngle, 0) * _startDirection, 
                    out RaycastHit hit, observableRange, observableMask))
            {
                inView.Add(hit); 
            }
            
            _rayAngle += fieldOfView / _rayCount; 
        }

        return inView;
    }
}
