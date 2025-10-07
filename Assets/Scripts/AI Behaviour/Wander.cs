using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wander : MonoBehaviour
{
    private float _wanderPerlin;

    [SerializeField] private Rigidbody rb;
    
    private void FixedUpdate()
    {
        _wanderPerlin = Mathf.PerlinNoise1D(Time.time);

        rb.AddRelativeTorque(0, _wanderPerlin, 0);
    }
}
