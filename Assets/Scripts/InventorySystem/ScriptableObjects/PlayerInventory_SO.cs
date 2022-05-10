using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Inventory/PlayerInventory")]
public class PlayerInventory_SO : ScriptableObject
{
    public int inventorySize;

    private InventorySlot[] _inventorySlots;

    private void OnValidate()
    {
        InitialiseSlots();
    }

    #region Initialisers
    private void InitialiseSlots()
    {
        _inventorySlots = new InventorySlot[inventorySize];
        for (int ii = 0; ii < inventorySize; ii++)
        {
            _inventorySlots[ii] = new InventorySlot();
        }
    }
    #endregion
    
    #region Item Functions
    /*
     * Accepts an Item_SO.
     */
    public void AcceptItem(Item_SO item, out bool hasAcceptedItem)
    {
        hasAcceptedItem = false;
        InventorySlot slot = GetFirstEmptyInventorySlot();
        if (slot == null) return;

        hasAcceptedItem = true;
        slot.AddItem(item);
    }

    /*
     * Removes an item from the inventory slots array.
     */
    public void RemoveItem(int slotIndex, out bool hasRemovedItem)
    {
        hasRemovedItem = false;
        InventorySlot slot = _inventorySlots[slotIndex];
        if (!slot.IsFilled()) return;

        hasRemovedItem = true;
        slot.RemoveItem();
    }

    /*
     * Gets an item from the inventory  slots array
     * If slot filled, return Item_SO
     * If slot not filled, return null
     */
    public Item_SO GetItem(int slotIndex, out bool hasGotItem)
    {
        hasGotItem = false;
        InventorySlot slot = _inventorySlots[slotIndex];
        if (!slot.IsFilled()) return null;

        hasGotItem = true;
        return slot.GetItem();
    }

    /*
     * Splits an item
     */
    public void SplitItem(int slotIndex, out bool hasSplitItem)
    {
        hasSplitItem = false;
        InventorySlot slot = _inventorySlots[slotIndex];
        if (!slot.IsFilled()) return;

        List<Item_SO> splitItems = slot.GetItem().itemComponents;
        if (splitItems.Count == 0) return;
        
        hasSplitItem = IsSplitable(splitItems);
        if (hasSplitItem)
        {
            RemoveItem(slotIndex, out _);
            foreach (Item_SO item in splitItems)
            {
                AcceptItem(item, out _);
            }
        }
    }
    #endregion
    
    #region Helper Functions
    /*
     * Gets the first empty slot in the inventory slots.
     * If found, returns InventorySlot
     * If not found, returns null
     */
    private InventorySlot GetFirstEmptyInventorySlot()
    {
        foreach (InventorySlot slot in _inventorySlots)
        {
            if (slot.IsFilled()) continue;
            return slot;
        }

        return null;
    }

    /*
     * Gets the number of filled and unfilled slots in the inventory
     */
    private void GetFillUnfilledInventorySlots(out int numFilled, out int numUnfilled)
    {
        numFilled = 0;
        numUnfilled = 0;
        
        foreach (InventorySlot slot in _inventorySlots)
        {
            if (slot.IsFilled()) numFilled++;
            else numUnfilled++;
        }
    }

    /*
     * Checks if the item has enough space to split
     */
    private bool IsSplitable(List<Item_SO> splitItems)
    {
        bool hasSplitItem = false;
        GetFillUnfilledInventorySlots(out _, out int numUnfilled);

        if (numUnfilled + 1 - splitItems.Count >= 0)
        {
            hasSplitItem = true;
        }

        return hasSplitItem;
    }
    #endregion
}

public class InventorySlot
{
    private Item_SO _item;
    private bool _isFilled;

    public Item_SO GetItem() => _item;
    public bool IsFilled() => _isFilled;

    public void AddItem(Item_SO item)
    {
        _item = item;
        _isFilled = true;
    }

    public void RemoveItem()
    {
        _item = null;
        _isFilled = false;
    }
}
