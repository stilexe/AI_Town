using System;
using UnityEngine;

public class MoveForward : MonoBehaviour, ISteering
{
    [SerializeField] Rigidbody rb;

    [SerializeField] private Vector3 speed;

    [SerializeField] private bool walking;

    public bool IsNeeded()
    {
        return walking;
    }

    public Vector3[] CalculateMovement()
    {
        return new Vector3[] {Vector3.zero, speed}; 
    }
    
}
