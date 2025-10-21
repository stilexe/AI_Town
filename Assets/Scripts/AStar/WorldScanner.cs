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
    
    public Vector3 GridSize() { return gridSize; }
    
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

        EventManager.OnObjectDestroyed += ScanWorld;
        
        GridSetUp();
    }

    private void OnDisable()
    {
        EventManager.OnObjectDestroyed -= ScanWorld;
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

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        
        //loop through all neighbours of the node 
        for (int xOffset = -1; xOffset < 2; xOffset++)
        {
            for (int zOffset = -1; zOffset < 2; zOffset++)
            {
                //check if scanning self or outside of grid  
                if (xOffset == 0 && zOffset == 0 ||
                    node.nodeCoord1 + xOffset < 0 || node.nodeCoord2 + zOffset < 0 ||
                    _grid.GetLength(0) - 1 < node.nodeCoord1 + xOffset ||
                    _grid.GetLength(1) - 1 < node.nodeCoord2 + zOffset)
                {
                    continue;
                }
                
                neighbours.Add(_grid[node.nodeCoord1 + xOffset, node.nodeCoord2 + zOffset]);
            }
        }
        
        return neighbours;
    }

    public float DistanceBetweenNodes(Node start, Node end)
    {
        int xDistance = Mathf.Abs(start.nodeCoord1 - end.nodeCoord1);
        int yDistance = Mathf.Abs(start.nodeCoord2 - end.nodeCoord2);
        
        float edgeDistance = gridSize.x / 2;
        float cornerDistance = (edgeDistance * edgeDistance) + (edgeDistance * edgeDistance);

        // diagonal length between nodes times the smallest distance plus horizontal length between nodes times largest distance take shorter distance
        if (xDistance > yDistance)
        {
            return cornerDistance * yDistance + (edgeDistance * (xDistance - yDistance));
        }

        return cornerDistance * xDistance + (gridSize.x / 2) * (yDistance - xDistance);
    }

    private void OnDrawGizmos()
    {
        if (_grid is null) return; 
        
        foreach (Node n in _grid)
        {
            if (n.isBlocked)
            {
                Gizmos.color = Color.black;
            }
            else
            {
                Gizmos.color = Color.white;
            }
            
            Gizmos.DrawWireCube(n.location, gridSize);
        }
    }
}
