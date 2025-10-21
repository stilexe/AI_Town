using UnityEngine;

public static class EventManager
{
    public delegate void ObjectDestroyed(Vector3 position);
    public static event ObjectDestroyed OnObjectDestroyed;
    public static void InvokeObjectDestroyed(Vector3 position) {OnObjectDestroyed?.Invoke(position);}
}
