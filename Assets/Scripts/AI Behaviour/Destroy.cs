using System;
using System.Collections;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private LayerMask destroyTargets;
    [SerializeField] private int maxDistance;
    [SerializeField] private float damageCooldown;
    [SerializeField] private float reachRadius;
    [SerializeField] private int strength; 

    private GameObject _toDestroy;
    private Damageable _damageable;
    private Look _look;
    private TurnTowards _turnTowards;
    private Collider[] _reach;
    private bool _destroying;
    private bool _canDestroy;

    private void OnEnable()
    {
        _look = GetComponent<Look>(); 
        _turnTowards = GetComponent<TurnTowards>();
    }

    private void FixedUpdate()
    {
        //if nothing to destroy look around for something 
        if (_toDestroy is null)
        {
            foreach (RaycastHit hit in _look.LookAround(destroyTargets, maxDistance))
            {
                if (hit.collider.gameObject.GetComponent<Damageable>() is not null) //todo: change how this figures out if its damageable
                {
                    _toDestroy = hit.collider.gameObject;
                    _turnTowards.ChangeTarget(hit.point);
                    return;
                }
            }
        }

        if (!_canDestroy) return;
        
        foreach (Collider c in _look.CheckSurroundings(destroyTargets))
        {
            if (_toDestroy is null) //if we don't have something we are targeting but we did find something we can reach 
            {
                _toDestroy = c.gameObject;
                _turnTowards.ChangeTarget(c.transform.position);
            }

            // if (c.gameObject == _toDestroy) //if it's the to destroy object 
            // {
            //     _destroying = true;
            //     c.GetComponent<Damageable>().TakeDamage(strength);
            //     _canDestroy = false; 
            //     StartCoroutine(CauseDamage());
            //     return;
            // }
            
            c.gameObject.GetComponent<Damageable>().TakeDamage(strength); //punch everything in reach once 
            _canDestroy = false; 
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        
        _canDestroy = true;

        if (_toDestroy is null)
        {
            _turnTowards.ClearTarget();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, reachRadius); //show reach 

        if (_toDestroy != null)
        {
            Gizmos.DrawLine(transform.position, _toDestroy.transform.position);
        }
    }
}
