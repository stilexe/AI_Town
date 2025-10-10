using UnityEngine;

public class Node
{
    public bool isBlocked;
    public Vector3 location;
    public int nodeCoord1, nodeCoord2;

    public float distanceFromStart;
    public float distanceToEnd;
    public float pathCost;

    public Node parent;
}
