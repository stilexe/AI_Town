using UnityEngine;

public class Shelf : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interacter)
    {
        Debug.Log("Shelf interact");
    }
}
