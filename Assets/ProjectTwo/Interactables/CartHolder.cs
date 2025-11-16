using UnityEngine;

public class CartHolder : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interacter)
    {
        GameObject newCart = Instantiate(gameObject);
        gameObject.tag = "Untagged"; //so new cart doesn't get interacted with by other shoppers 
        newCart.GetComponent<Cart>().NewPusher(interacter);
    }
}
