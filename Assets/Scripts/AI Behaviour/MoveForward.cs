using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour, ISteering
{
    [SerializeField] private float defaultSpeed;

    [SerializeField] private bool walking = true;

    private float _speed;

    private void Start()
    {
        _speed = defaultSpeed;
    }

    public void NewSpeed(float newSpeed)
    {
        _speed = defaultSpeed * newSpeed;
    }

    public void Walking(bool ifWalking)
    {
        walking = ifWalking;
    }

    public Vector3[] CalculateMovement()
    {
        if (!walking)
        {
            return new Vector3[]
            { 
                Vector3.zero, Vector3.zero
            };
        }
        
        return new Vector3[] {Vector3.zero, Vector3.forward * _speed}; 
    }

    public Dictionary<Color, List<Vector3>> LineRenderDisplay()
    {
        return new Dictionary<Color, List<Vector3>>()
        {
            { Color.green, new List<Vector3>()
                {
                    transform.position, transform.position + transform.forward * 6
                }
            },
        };
    }

    public List<string> LineRenderDescription()
    {
        return new List<string>()
        {
            "Green: direction moving."
        };
    }
}
