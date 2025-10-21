using System;
using System.Collections;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private IInteractable _interactable;
    private Look _look;
    private bool _canInteract = true;

    private void OnEnable()
    {
        _look = GetComponent<Look>();
    }

    private void FixedUpdate()
    {
        if (!_canInteract) return; 
        
        foreach (Collider c in _look.CheckSurroundings())
        {
            _interactable = c.GetComponent<IInteractable>();
            
            if (_interactable != null)
            {
                _interactable.Interact(gameObject);
                _canInteract = false;
                StartCoroutine(Cooldown());
                return; 
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5);

        _canInteract = true;
    }
}
