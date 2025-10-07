using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public delegate void ReachedPathEnd();
    public event ReachedPathEnd ReachedPathEndEvent; 
    
    private List<Node> _path;
    private int _currentIndex;

    [SerializeField] private TurnTowards turnTowards;

    private void Start()
    {
        SetRandomPath();
    }

    public void SetPath(List<Node> newPath)
    {
        _path = newPath;
        _currentIndex = 0;
        turnTowards.ChangeTarget(_path[_currentIndex].location);
    }

    public void SetRandomPath()
    {
        _path = WorldScanner.Instance.FindPath(WorldScanner.Instance.FindClosestNode(transform.position, true),
                WorldScanner.Instance.FindRandomNode());
    }
    
    private void FixedUpdate()
    {
        if (_path == null || _path.Count == 0) return; 
        
        if (Vector2.Distance(transform.position, _path[_currentIndex].location) > 0.1f)
        {
            // end of path
            if (_currentIndex == _path.Count - 1)
            {
                _currentIndex = 0;
                _path = null;
                ReachedPathEndEvent?.Invoke();
                return; 
            }
            
            _currentIndex++;
            
            turnTowards.ChangeTarget(_path[_currentIndex].location);
        }
    }
}
