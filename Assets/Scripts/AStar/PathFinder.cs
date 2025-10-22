using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public static PathFinder Instance; 
    
    private WorldScanner _scanner;

    private bool _scanning; 
    private List<Node> _path = new List<Node>();
    Node _startNode;
    Node _endNode;

    private List<Node> _open = new List<Node>();
    private List<Node> _closed = new List<Node>();
    
    Node _currentNode;

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        _scanner = GetComponent<WorldScanner>();
    }

    public List<Node> FindPath(Vector3 start, Vector3 end)
    {
        if (_scanning)
        {
            Debug.Log("Already scanning");
            return null;
        }
        
        return FindPath(_scanner.FindClosestNode(start), _scanner.FindClosestNode(end));
    }

    public List<Node> FindPath(Node start, Node end)
    {
        if (_scanning)
        {
            Debug.Log("Already scanning");
            return null;
        }
        
        Debug.Log("Scanning path");
        
        _scanning = true;
        
        _startNode = start;
        _endNode = end;
        
        _open.Clear();
        _closed.Clear();
        _path.Clear();
        
        _currentNode = _startNode;
        _open.Add(_currentNode);

        while (_open.Count > 0)
        {
            _currentNode = _open[0];
            
            //finding the closest node in open 
            foreach (Node n in _open)
            {
                if (_currentNode.pathCost > n.pathCost || 
                    Mathf.Approximately(_currentNode.pathCost, n.pathCost) && 
                    Vector3.Distance(n.location, _endNode.location) < Vector3.Distance(_currentNode.location, _endNode.location))
                {
                    _currentNode = n;
                }
            }
            
            _open.Remove(_currentNode);
            _closed.Add(_currentNode);

            if (_currentNode == _endNode)
            {
                Debug.Log("Found path");
                break; 
            }
            
            float scanningPathCost; 

            foreach (Node n in _scanner.GetNeighbours(_currentNode))
            {
                if (n.isBlocked || _closed.Contains(n))
                {
                    //Debug.Log("Blocked or closed node.");
                    continue;
                }
                
                scanningPathCost = (n.distanceFromStart + Vector3.Distance(_currentNode.location, n.location)) +
                                   Vector3.Distance(n.location, _endNode.location);

                //if the distance is smaller than the distance already there, or if the node isn't in open 
                if (scanningPathCost < n.pathCost || !_open.Contains(n))
                {
                    n.pathCost = scanningPathCost;
                    n.parent = _currentNode;
                
                    if (!_open.Contains(n))
                    {
                        _open.Add(n);
                    }
                }
            }
        }
        
        while (_currentNode != _startNode)
        {
            _path.Add(_currentNode);
            _currentNode = _currentNode.parent;
        }
            
        _path.Reverse();
        
        _scanning = false;
        return _path;
    }
    
    private void OnDrawGizmos()
    {
        foreach (Node n in _open)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(n.location, _scanner.GridSize());
        }

        foreach (Node n in _closed)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(n.location, _scanner.GridSize());
        }

        foreach (Node n in _path)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(n.location, _scanner.GridSize());
        }

        if (_startNode == null || _endNode == null) return; 
        
        Gizmos.color = Color.magenta;

        Gizmos.DrawCube(_startNode.location, _scanner.GridSize());
        Gizmos.DrawCube(_endNode.location, _scanner.GridSize());
    }
}
