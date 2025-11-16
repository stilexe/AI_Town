using System;
using UnityEngine;

public class Push : MonoBehaviour
{
    [SerializeField] private GameObject pushLocation; 
    
    private GameObject _pushing;
    private Avoid _avoid;

    private void Start()
    {
        _avoid = GetComponent<Avoid>();
    }

    public void AddObject(GameObject newObject)
    {
        _pushing = newObject; 
        
        _pushing.transform.position = pushLocation.transform.position;
        
        _pushing.transform.parent = transform;
        
        _avoid.AddException(_pushing);
        _avoid.AddToRange((int)_pushing.GetComponent<Collider>().bounds.size.z + 2);
    }

    public void RemoveObject()
    {
        _avoid.RemoveException(_pushing);
        Destroy(_pushing);
        _pushing = null;
        _avoid.ResetRange();
    }
}
