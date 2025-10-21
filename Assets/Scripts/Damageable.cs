using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
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
            EventManager.InvokeObjectDestroyed();
            
            Destroy(gameObject);
        }

        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        gameObject.GetComponent<Material>().SetColor("_Color", Color.red);
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Material>().SetColor("_Color", Color.white);
    }
}
