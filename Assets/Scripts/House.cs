using System.Collections;
using TMPro;
using UnityEngine;

public class House : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private TextMeshProUGUI healthText;
    private int _health;
    
    private void Start()
    {
        _health = maxHealth;
    }

    private void FixedUpdate()
    {
        healthText.text = _health + "/" + maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        Debug.Log("Taking damage");
        
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        EventManager.InvokeObjectDestroyed(transform.position);
            
        Destroy(gameObject);
    }
}
