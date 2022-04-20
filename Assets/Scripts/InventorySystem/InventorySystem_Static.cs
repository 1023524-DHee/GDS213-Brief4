using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventorySystem_Static
{
    private static PlayerInventory_SO _playerInventory = (PlayerInventory_SO) Resources.Load("Inventory/PlayerInventory");

    public static PlayerInventory_SO GetPlayerInventory() => _playerInventory;
    public static int GetPlayerInventorySize() => _playerInventory.inventorySize;
    
    public static event Action<bool> onAddItem;
    public static void AddItem(Item_SO item, out bool hasAcceptedItem)
    {
        _playerInventory.AcceptItem(item, out hasAcceptedItem);
        if (onAddItem != null) onAddItem(hasAcceptedItem);
    }

    public static event Action<bool> onRemoveItem;
    public static void RemoveItem(int index, out bool hasRemovedItem)
    {
        _playerInventory.RemoveItem(index, out hasRemovedItem);
        if (onRemoveItem != null) onRemoveItem(hasRemovedItem);
    }

    public static event Action<bool> onSplitItem;

    public static void SplitItem(int index, out bool hasSplitItem)
    {
        _playerInventory.SplitItem(index, out hasSplitItem);
        if (onSplitItem != null) onSplitItem(hasSplitItem);
    }
    
    public static Item_SO GetItem(int index, out bool hasGotItem)
    {
        return _playerInventory.GetItem(index, out hasGotItem);
    }
}
