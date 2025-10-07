using System;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    public Vector3 speed; 
    
    
    private void FixedUpdate()
    {
        rb.AddRelativeForce(speed);
    }
    
}
