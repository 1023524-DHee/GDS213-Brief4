using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Button script for the player inventory button.
 */
public class PlayerInventory_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Touch Values
    public float holdThreshold;

    private bool _isTouching;
    private float _touchTime;
    
    // Inventory Values
    private PlayerInventory_UI _playerInventoryUI;
    private int _slotIndex;

    public void Initialise(int slotIndex, PlayerInventory_UI playerInventoryUI)
    {
        _slotIndex = slotIndex;
        _playerInventoryUI = playerInventoryUI;
    }
    
    // Called when the button is first held down
    private void TouchStart()
    {
        _touchTime = 0;
        _isTouching = true;
        StartCoroutine(GetTouchType());
    }

    // Called when the button is released
    private void TouchEnd()
    {
        if (!_isTouching) return;
        _isTouching = false;
        TapRegistered();
        StopAllCoroutines();
    }

    // Checks if the button is being held down or tapped
    private IEnumerator GetTouchType()
    {
        while (_isTouching && _touchTime < holdThreshold)
        {
            _touchTime += Time.deltaTime;
            yield return null;
        }
        HoldRegistered();
    }
    
    // Called when hold is registered in the GetTouchType() function
    private void HoldRegistered()
    {
        _isTouching = false;
        InventorySystem_Static.SplitItem(_slotIndex, out _);
    }

    // Called when a tap is registered
    private void TapRegistered()
    {
        Item_SO item = InventorySystem_Static.GetItem(_slotIndex, out bool hasGotIem);
        if(hasGotIem) _playerInventoryUI.OpenItemViewer(item);
    }

    #region Pointer Interfaces
    public void OnPointerDown(PointerEventData eventData)
    {
        TouchStart();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TouchEnd();
    }
    #endregion
}
