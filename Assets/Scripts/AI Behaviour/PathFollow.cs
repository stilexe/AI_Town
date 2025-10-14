using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathFollow : MonoBehaviour
{
    public delegate void ReachedPathEnd();
    public event ReachedPathEnd ReachedPathEndEvent; 
    
    private List<Node> _path = new List<Node>();
    private int _currentIndex;

    private TurnTowards _turnTowards;
    [SerializeField] private float distanceBeforeWaypointChange;
    [SerializeField] private List<GameObject> possibleTargets; //for testing

    private void Start()
    {
        _turnTowards = GetComponent<TurnTowards>();
        SetPath(possibleTargets[Random.Range(0, possibleTargets.Count)].transform.position);
    }

    public void SetPath(Vector3 target)
    {
        if (PathFinder.Instance == null)
        {
            Debug.Log("Path finder null");
            return; 
        }
        
        _path = PathFinder.Instance.FindPath(transform.position, target);
        
        _currentIndex = 0;
        _turnTowards.ChangeTarget(_path[_currentIndex].location, true);
    }
    
    private void FixedUpdate()
    {
        if (_path == null || _path.Count == 0) return; 
        
        if (Vector2.Distance(transform.position, _path[_currentIndex].location) < distanceBeforeWaypointChange)
        {
            // end of path
            if (_currentIndex == _path.Count - 1)
            {
                _currentIndex = 0;
                _path = null;
                ReachedPathEndEvent?.Invoke();
                _turnTowards.ClearTarget();
                return; 
            }
            
            _currentIndex++;
            _turnTowards.ChangeTarget(_path[_currentIndex].location, true);
        }
    }
}
