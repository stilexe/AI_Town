using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour, ISteering
{
    [SerializeField] private float speed;

    [SerializeField] private bool walking = true;

    public Vector3[] CalculateMovement()
    {
        if (!walking)
        {
            return new Vector3[]
            { 
                Vector3.zero, Vector3.zero
            };
        }
        
        return new Vector3[] {Vector3.zero, Vector3.forward * speed}; 
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
