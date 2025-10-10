using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathFollow : MonoBehaviour
{
    public delegate void ReachedPathEnd();
    public event ReachedPathEnd ReachedPathEndEvent; 
    
    private List<Node> _path;
    private int _currentIndex;

    [SerializeField] private TurnTowards turnTowards;
    [SerializeField] private float distanceBeforeWaypointChange;
    [SerializeField] private List<GameObject> possibleTargets; //for testing

    private void Start()
    {
        SetPath(possibleTargets[Random.Range(0, possibleTargets.Count)].transform.position);
    }

    public void SetPath(Vector3 target)
    {
        _path = PathFinder.Instance.FindPath(transform.position, target);
        
        _currentIndex = 0;
    }
    
    private void FixedUpdate()
    {
        if (_path == null || _path.Count == 0) return; 
        
        if (Vector2.Distance(transform.position, _path[_currentIndex].location) < distanceBeforeWaypointChange)
        {
            turnTowards.ChangeTarget(_path[_currentIndex].location, true);
            
            // end of path
            if (_currentIndex == _path.Count - 1)
            {
                _currentIndex = 0;
                _path = null;
                ReachedPathEndEvent?.Invoke();
                return; 
            }
            
            _currentIndex++;
        }
    }
}
