using System;
using System.Collections;
using UnityEngine;

public class Doorknocker : MonoBehaviour
{
    private GameObject _door; 
    private PathFollow _pathFollow;

    private void OnEnable()
    {
        _pathFollow = GetComponent<PathFollow>();
        
        _pathFollow.ReachedPathEndEvent += NewDoor;
    }

    private void Start()
    {
        NewDoor();
    }

    private void OnDisable()
    {
        _pathFollow.ReachedPathEndEvent -= NewDoor;
    }

    private void NewDoor()
    {
        GameObject newDoor = Town.Instance.RandomDoor();

        while (newDoor == _door) //dont want the same door 
        {
            newDoor = Town.Instance.RandomDoor();
        }
        
        _door = newDoor;

        if (_door is not null) //sometimes gets destroyed 
        {
            _pathFollow.SetPath(_door.transform.position);
        }
        else
        {
            NewDoor();
        }
    }
    
    
}
