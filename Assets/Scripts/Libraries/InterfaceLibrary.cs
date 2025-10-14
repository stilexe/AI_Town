using UnityEngine;

public interface ISteering
{
    /// <summary>
    /// Calculates the necessary movement for the steering behaviour. 
    /// </summary>
    /// <returns>[torque, force]</returns>
    Vector3[] CalculateMovement();

    bool IsNeeded();
}

public interface IDamageable
{
    int Health { get; }
    void TakeDamage(int damage);
    void Die(); 
}
