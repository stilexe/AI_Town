using System;
using System.Collections;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float damageCooldown;
    [SerializeField] private int strength; 

    private GameObject _toDestroy;
    private Look _look;
    private TurnTowards _turnTowards;
    private bool _canDestroy = true;

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
            foreach (RaycastHit hit in _look.LookAround())
            {
                if (hit.collider.gameObject.TryGetComponent(out IDamageable d))
                {
                    _toDestroy = hit.collider.gameObject;
                    _turnTowards.ChangeTarget(hit.point);
                    return;
                }
            }
        }

        if (!_canDestroy) return;
        
        foreach (Collider c in _look.CheckReachableDistance())
        {
            if (c.gameObject.TryGetComponent(out IDamageable d))
            {
                if (_toDestroy is null) //if we don't have something we are targeting but we did find something we can reach 
                {
                    _toDestroy = c.gameObject;
                    _turnTowards.ChangeTarget(c.transform.position);
                }
                
                d.TakeDamage(strength); 
 
                StartCoroutine(Cooldown());
                return; 
            }
        }
    }

    private IEnumerator Cooldown()
    {
        _canDestroy = false; 
        
        yield return new WaitForSeconds(damageCooldown);

        if (_toDestroy is null)
        {
            _turnTowards.ClearTarget();
        }
        
        _canDestroy = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (_toDestroy != null)
        {
            Gizmos.DrawLine(transform.position, _toDestroy.transform.position);
        }
    }
}
