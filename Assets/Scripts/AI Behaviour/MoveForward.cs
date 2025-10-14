using System;
using UnityEngine;

public class MoveForward : MonoBehaviour, ISteering
{
    [SerializeField] private float speed;

    [SerializeField] private bool walking = true;

    public bool IsNeeded()
    {
        return walking;
    }

    public Vector3[] CalculateMovement()
    {
        return new Vector3[] {Vector3.zero, Vector3.forward * speed}; 
    }
    
}
