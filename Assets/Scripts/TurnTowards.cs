using System;
using UnityEngine;

public class TurnTowards : MonoBehaviour
{
    public GameObject target;

    [SerializeField] private Rigidbody rb;
    
    private Vector3 _targetDirection; 

    private void FixedUpdate()
    {
        if (target)
        {
            _targetDirection = (target.transform.position - transform.position).normalized; 
        }
    }
}
