using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WorldItem : MonoBehaviour, IInteractable
{
    public Item_SO itemSO;

    public void Interact()
    {
        InventorySystem_Static.AddItem(itemSO, out bool successfulAdd);
        if (successfulAdd) ItemAdded();
        else ItemNotAdded();
    }

    private void ItemAdded()
    {
        Debug.Log("Item picked up");
        Destroy(gameObject);
    }

    private void ItemNotAdded()
    {
        Debug.Log("Inventory is full");
    }
}
