using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISteering
{
    /// <summary>
    /// Calculates the necessary movement for the steering behaviour. 
    /// </summary>
    /// <returns>[torque, force]</returns>
    Vector3[] CalculateMovement();

    Dictionary<Color,List<Vector3>> LineRenderDisplay();
    List<string> LineRenderDescription();
}

public interface IDamageable
{
    void TakeDamage(int damage);
    void Die();
}

public interface IInteractable
{
    void Interact(GameObject interacter);
}
