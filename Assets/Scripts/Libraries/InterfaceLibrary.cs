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
}

public interface IDamageable
{
    int Health { get; }
    void TakeDamage(int damage);
    void Die(); 
}

public interface IInteractable
{
    void Interact(GameObject interacter);
}
