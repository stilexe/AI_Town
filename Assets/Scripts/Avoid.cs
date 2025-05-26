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
    [SerializeField] private int noOfRays;

    private float _rayAngle; 

    private void FixedUpdate()
    {
        //this seems a lot to be doing every frame 

        for (int i = 0; i < noOfRays; i++)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, avoidRange,
                    observableMask))
            {
                Debug.Log(name + "raycast hit" + hit.collider.name);
            
                rb.AddRelativeTorque(0, turnRange/ hit.distance ,0);
                break;
            }
        }
    }
}
