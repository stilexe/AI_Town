using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldScanner : MonoBehaviour
{
    public static WorldScanner Instance;
    
    /// <summary>
    /// How many grid squares in each direction.
    /// </summary>
    [SerializeField] private Vector3 groundSize;
    /// <summary>
    /// Size of the grid squares.
    /// </summary>
    [SerializeField] private Vector3 gridSize;
    [SerializeField] private LayerMask blockedLayers;
    
    private Node[,] _grid;
    
    List<Node> _open = new List<Node>(); 
    List<Node> _closed = new List<Node>();
    List<Node> _path = new List<Node>();
    
    public bool generateRandomPath; 
    
    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        
        _grid = new Node[(int)groundSize.x, (int)groundSize.z];
        GridSetUp();
    }

    private void Start()
    {
        //for testing
        if (generateRandomPath)
        {
            if (_grid != null)
            {
                FindPath(FindRandomNode(), FindRandomNode());
            }
        }
    }

    private void GridSetUp()
    {
        for (int i = 0; i < groundSize.x; i++)
        {
            for (int j = 0; j < groundSize.z; j++)
            {
                _grid[i, j] = new Node(){location = new Vector3(transform.position.x + (i * gridSize.x), 0, transform.position.z + (j * gridSize.y)), nodeCoord1 = i, nodeCoord2 = j};
            }
        }
        
        ScanWorld();
    }

    public void ScanWorld()
    {
        foreach (Node n in _grid)
        {
            // check if node is blocked 
            if (Physics.CheckBox(n.location, gridSize / 2, Quaternion.identity, blockedLayers))
            {
                n.isBlocked = true;
            }
            else
            {
                n.isBlocked = false;
            }
        }
    }

    /// <summary>
    /// Finds closest accessible node to the passed position. 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="allowBlocked">Whether to allow the node to be blocked.</param>
    /// <returns>Closest node to passed position.</returns>
    public Node FindClosestNode(Vector3 position, bool allowBlocked = false)
    {
        Node toReturn = null;
        
        foreach (Node n in _grid)
        {
            if (!allowBlocked)
            {
                if (n.isBlocked) continue;
            }
            
            if (toReturn == null || Vector3.Distance(n.location, position) < Vector3.Distance(toReturn.location, position))
            {
                toReturn = n;
            }
        }
        
        return toReturn;
    }

    public Node FindRandomNode()
    {
        Node toReturn = _grid[Random.Range(0, _grid.GetLength(0)), Random.Range(0, _grid.GetLength(1))];

        while (toReturn.isBlocked)
        {
            toReturn = _grid[Random.Range(0, _grid.GetLength(0)), Random.Range(0, _grid.GetLength(1))];
        }
        
        return toReturn;
    }

    public List<Node> FindPath(Node startNode, Node endNode)
    {
        Debug.Log("Find path" + startNode.location + " to " + endNode.location);
        
        _path = new List<Node>();
        _open = new List<Node>();
        _closed = new List<Node>();
        
        Node currentNode = startNode;
        _open.Add(currentNode);
        
        while (_open.Count == 1) //testing need to change back to more than 0
        {
            //get node in open list with lowest total path cost and make it current node
            foreach (Node n in _open)
            {
                if (currentNode.pathCost > n.pathCost)
                {
                    currentNode = n;
                }
            }
            
            _open.Remove(currentNode);
            _closed.Add(currentNode);
            
            //if reached the end node
            if (currentNode == endNode)
            {
                Debug.Log("Found path");
                break;
            }
            
            Node scanningNode;
            float scanningPathCost; 

            // loop through neighbours of current node 
            for (int xOffset = -1; xOffset < 2; xOffset++)
            {
                for (int zOffset = -1; zOffset < 2; zOffset++)
                {
                    //if not within the grid todo: not working properly 
                    if (_grid.GetLength(0) < currentNode.nodeCoord1 + xOffset ||
                        _grid.GetLength(1) < currentNode.nodeCoord2 + zOffset)
                    {
                        Debug.Log("Outside grid");
                        continue;
                    }

                    scanningNode = _grid[currentNode.nodeCoord1 + xOffset, currentNode.nodeCoord2 + zOffset];

                    //if node is blocked or in closed list
                    if (scanningNode.isBlocked || _closed.Contains(scanningNode))
                    {
                        Debug.Log("Blocked or closed node.");
                        continue;
                    }
                    
                    scanningPathCost = (currentNode.distanceFromStart + Vector3.Distance(currentNode.location, scanningNode.location)) +
                                       Vector3.Distance(scanningNode.location, endNode.location);

                    if (!_open.Contains(scanningNode) || scanningPathCost < scanningNode.pathCost)
                    {
                        scanningNode.pathCost = scanningPathCost;
                        scanningNode.parent = currentNode;
                        
                        if (!_open.Contains(scanningNode))
                        {
                            Debug.Log("Node added to open");
                            _open.Add(scanningNode);
                        }
                        
                    } 

                }
            }
        }
        
        while (currentNode.parent != null)
        {
            _path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        _path.Reverse();

        return _path; 
    }

    private void OnDrawGizmos()
    {
        if (_grid is null) return; 
        
        foreach (Node n in _grid)
        {
            if (n.isBlocked)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }

            if (_open.Contains(n))
            {
                Gizmos.color = Color.blue;
            }
            else if (_closed.Contains(n))
            {
                Gizmos.color = Color.yellow;
            }

            if (_path.Contains(n))
            {
                Gizmos.color = Color.magenta;
            }
            
            Gizmos.DrawWireCube(n.location, gridSize);
        }
        
    }
}
