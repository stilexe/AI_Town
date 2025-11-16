using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathFollow : MonoBehaviour
{
    public delegate void ReachedPathEnd();
    public event ReachedPathEnd ReachedPathEndEvent;

    private List<Node> _path = new List<Node>();
    public List<Node> GetPath() { return _path; } 
    private int _currentIndex;

    private TurnTowards _turnTowards;
    [SerializeField] private float distanceBeforeWaypointChange;

    private void OnEnable()
    {
        _turnTowards = GetComponent<TurnTowards>();
    }

    public void SetPath(Vector3 target)
    {
        if (PathFinder.Instance == null)
        {
            Debug.Log("Path finder null");
            return; 
        }

        while (_path is null || _path.Count == 0)
        {
            _path = PathFinder.Instance.FindPath(transform.position, target);
        }
        
        _currentIndex = 0;
        _turnTowards.ChangeTarget(_path[_currentIndex].location, true);
    }
    
    private void FixedUpdate()
    {
        if (_path == null || _path.Count == 0) return; 
        
        //Debug.Log(Vector3.Distance(transform.position, _path[_currentIndex].location));
        
        if (Vector3.Distance(transform.position, _path[_currentIndex].location) < distanceBeforeWaypointChange)
        {
            Debug.Log("next path point");
            
            // end of path
            if (_currentIndex == _path.Count - 1)
            {
                _turnTowards.ClearTarget();
                _currentIndex = 0;
                _path = null;
                ReachedPathEndEvent?.Invoke();
                return; 
            }
            
            _currentIndex++;
            _turnTowards.ChangeTarget(_path[_currentIndex].location, true); 
        }
    }

    private void OnDrawGizmos()
    {
        if (_path == null || _path.Count == 0) return;

        Gizmos.color = Color.green;

        for(int i = _currentIndex; i < _path.Count - 1; i++)
        {
            Gizmos.DrawLine(_path[i].location, _path[i + 1].location);
        }
    }
}
