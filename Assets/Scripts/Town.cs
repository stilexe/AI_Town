using System;
using System.Collections;
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

    // object needs to be destroyed before the event is called, can't be done on a script attached to the object 
    public void ObjectDestroyed(GameObject destroyed)
    {
        Destroy(destroyed);
        StartCoroutine(EventWait());
        //EventManager.InvokeObjectDestroyed();
    }

    private IEnumerator EventWait()
    {
        yield return new WaitForSeconds(1);
        
        EventManager.InvokeObjectDestroyed();
    }
}
