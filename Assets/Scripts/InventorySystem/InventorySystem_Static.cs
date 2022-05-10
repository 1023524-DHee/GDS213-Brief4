using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This static class is used to interact with the player inventory
 */
public static class InventorySystem_Static
{
    private static PlayerInventory_SO _playerInventory = (PlayerInventory_SO) Resources.Load("Inventory/PlayerInventory");

    public static PlayerInventory_SO GetPlayerInventory() => _playerInventory;
    public static int GetPlayerInventorySize() => _playerInventory.inventorySize;
    
    // Add item event
    public static event Action<bool> onAddItem;
    
    // Add item to the player inventory
    public static void AddItem(Item_SO item, out bool hasAcceptedItem)
    {
        _playerInventory.AcceptItem(item, out hasAcceptedItem);
        if (onAddItem != null) onAddItem(hasAcceptedItem);
    }

    // Remove item event
    public static event Action<bool> onRemoveItem;
    
    // Remove item from the player inventory according to index
    public static void RemoveItem(int index, out bool hasRemovedItem)
    {
        _playerInventory.RemoveItem(index, out hasRemovedItem);
        if (onRemoveItem != null) onRemoveItem(hasRemovedItem);
    }

    // Split item event
    public static event Action<bool> onSplitItem;
    
    // Split an item in the player inventory according to index.
    public static void SplitItem(int index, out bool hasSplitItem)
    {
        _playerInventory.SplitItem(index, out hasSplitItem);
        if (onSplitItem != null) onSplitItem(hasSplitItem);
    }
    
    // Get an item from the player inventory according to index
    public static Item_SO GetItem(int index, out bool hasGotItem)
    {
        return _playerInventory.GetItem(index, out hasGotItem);
    }
}
