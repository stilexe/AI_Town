using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Town : MonoBehaviour
{
    public static Town Instance;
    
    [SerializeField] private GameObject supermarketDoor;
    [SerializeField] private GameObject cnrStoreDoor;
    
    private GameObject[] houseDoors;

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
        
        houseDoors = GameObject.FindGameObjectsWithTag("Door");
    }

    public GameObject RandomDoor()
    {
        return houseDoors[Random.Range(0, houseDoors.Length)];
    }

    public GameObject SupermarketDoor()
    {
        return supermarketDoor;
    }

    public GameObject CnrStoreDoor()
    {
        return cnrStoreDoor;
    }
}
