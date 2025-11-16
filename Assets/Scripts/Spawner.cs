using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> toSpawn;

    public int spawnCount;

    public Vector3 topCorner, bottomCorner;
    
    void Start()
    {
        foreach (GameObject go in toSpawn)
        {
            for (int i = 0; i < spawnCount; i++)
            { 
                Instantiate(go, new Vector3(Random.Range(topCorner.x, bottomCorner.x), 2 , Random.Range(topCorner.z, bottomCorner.z)), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            }
        }
    }

    /// <summary>
    /// Spawn exactly one of each to spawn object 
    /// </summary>
    public void SpawnOne()
    {
        foreach (GameObject go in toSpawn)
        {
            Instantiate(go,
                new Vector3(Random.Range(topCorner.x, bottomCorner.x), 2, Random.Range(topCorner.z, bottomCorner.z)),
                Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
        }
    }
}
