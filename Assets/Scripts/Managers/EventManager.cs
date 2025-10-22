using UnityEngine;

public static class EventManager
{
    public delegate void ObjectDestroyed();
    public static event ObjectDestroyed OnObjectDestroyed;
    public static void InvokeObjectDestroyed() {OnObjectDestroyed?.Invoke();}
}
