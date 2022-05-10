using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * UI Functionality for the player inventory system.
 * For inventory functionality, look at InventorySystem_Static.cs
 * This class does not directly change the inventory.
 */
public class PlayerInventory_UI : MonoBehaviour
{
    private List<RectTransform> _slotPositions;
    private CanvasGroup _inventoryCanvasGroup;
    private Camera _mainCamera;

    private bool _canClose;
    
    public Transform inventorySlotParent;
    public GameObject inventoryButton;
    public RectTransform panelRectTransform;
    public CanvasGroup itemViewer;

    public Item_SO testSO;
    
    private void Awake()
    {
        _inventoryCanvasGroup = GetComponent<CanvasGroup>();
        _mainCamera = Camera.main;
        
        Initialise();
    }
    
    private void OnEnable()
    {
        InventorySystem_Static.onAddItem += OnAddItem;
        InventorySystem_Static.onRemoveItem += OnRemoveItem;
        InventorySystem_Static.onSplitItem += OnSplitItem;
    }

    private void OnDisable()
    {
        InventorySystem_Static.onAddItem -= OnAddItem;
        InventorySystem_Static.onRemoveItem -= OnRemoveItem;
        InventorySystem_Static.onSplitItem -= OnSplitItem;
    }

    // Check if tapped outside of UI and close the UI
    private void Update()
    {
        if (!(_canClose)) return;
        
        if (Input.GetMouseButtonDown(0) && !RectTransformUtility.RectangleContainsScreenPoint(panelRectTransform, Input.mousePosition))
        {
            CloseMenu();
        }
    }

    // Gets the rect transforms of all slots in the inventory
    private void Initialise()
    {
        _slotPositions = new List<RectTransform>();
        foreach (Transform child in inventorySlotParent)
        {
            _slotPositions.Add(child.GetComponent<RectTransform>());
        }

        TurnOffCanvasGroup(itemViewer);
    }
    
    // Checks if a slot is empty or not and fills in or removes the button prefab from the slot.
    private void UpdateInventory()
    {
        for (int ii = 0; ii < InventorySystem_Static.GetPlayerInventorySize(); ii++)
        {
            Item_SO item = InventorySystem_Static.GetItem(ii, out bool hasGotItem);

            if (_slotPositions[ii].childCount > 0 && !hasGotItem)
            {
                _slotPositions[ii].GetComponent<Image>().enabled = true;
            }
            else if(_slotPositions[ii].childCount == 0 && hasGotItem)
            {
                _slotPositions[ii].GetComponent<Image>().enabled = false;
                PlayerInventory_Button button = Instantiate(inventoryButton, _slotPositions[ii]).GetComponent<PlayerInventory_Button>();
                button.Initialise(ii, this);
            }
        }
    }
    
    #region Item Functions
    // Subscribed to OnAddItem event
    private void OnAddItem(bool hasAddedItem)
    {
        if (hasAddedItem) UpdateInventory();
    }

    // Subscribed to OnRemoveItem event
    private void OnRemoveItem(bool hasRemovedItem)
    {
        if (hasRemovedItem) UpdateInventory();
    }

    // Subscribed to OnSplitItem event
    private void OnSplitItem(bool hasSplitItem)
    {
        if (hasSplitItem) UpdateInventory();
    }
    #endregion
    
    #region Open/Close Functions
    // Open the UI
    public void OpenMenu()
    {
        TurnOnCanvasGroup(_inventoryCanvasGroup);
        _canClose = true;
    }

    //Close the UI
    public void CloseMenu()
    {
        TurnOffCanvasGroup(_inventoryCanvasGroup);
        _canClose = false;
    }

    // Open the item viewer
    public void OpenItemViewer(Item_SO item)
    {
        TurnOnCanvasGroup(itemViewer);
        ItemViewer.current.ShowItem(item);
        _canClose = false;
    }
    
    // Close the item viewer
    public void CloseItemViewer()
    {
        TurnOffCanvasGroup(itemViewer);
        ItemViewer.current.HideItem();
        _canClose = true;
    }
    #endregion

    #region Helper Functions
    // Turns on the CanvasGroup
    private void TurnOnCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    // Turns off the CanvasGroup
    private void TurnOffCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    #endregion
}
