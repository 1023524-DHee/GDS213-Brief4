using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory_Button : MonoBehaviour
{
    // Touch Values
    public float holdThreshold;

    private bool isTouching;
    private float touchTime;
    
    // Inventory Values
    private PlayerInventory_UI _playerInventoryUI;
    private int _slotIndex;

    public void Initialise(int slotIndex, PlayerInventory_UI playerInventoryUI)
    {
        _slotIndex = slotIndex;
        _playerInventoryUI = playerInventoryUI;
    }
    
    public void TouchStart()
    {
        touchTime = 0;
        isTouching = true;
        StartCoroutine(GetTouchType());
    }

    public void TouchEnd()
    {
        if (!isTouching) return;
        isTouching = false;
        TapRegistered();
        StopAllCoroutines();
    }

    private IEnumerator GetTouchType()
    {
        while (isTouching && touchTime < holdThreshold)
        {
            touchTime += Time.deltaTime;
            yield return null;
        }
        HoldRegistered();
    }
    
    private void HoldRegistered()
    {
        isTouching = false;
        InventorySystem_Static.SplitItem(_slotIndex, out _);
    }

    private void TapRegistered()
    {
        Item_SO item = InventorySystem_Static.GetItem(_slotIndex, out bool hasGotIem);
        if(hasGotIem) _playerInventoryUI.OpenItemViewer(item);
    }
}
