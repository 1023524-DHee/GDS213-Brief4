using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
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
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        StartCoroutine(DestroyAndSpawn());
    }

    private IEnumerator DestroyAndSpawn()
    {
        yield return null;
        if(transform.childCount > 0) DestroyImmediate(transform.GetChild(0).gameObject);
        Instantiate(itemSO.itemModel, transform);
    }
    #endif
}
