using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory_UI : MonoBehaviour
{
    private List<RectTransform> _slotPositions;
    
    public Transform inventorySlotParent;
    public GameObject inventoryButton;
    public CanvasGroup itemViewer;
    
    private void Awake()
    {
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

    private void Initialise()
    {
        _slotPositions = new List<RectTransform>();
        foreach (Transform child in inventorySlotParent)
        {
            _slotPositions.Add(child.GetComponent<RectTransform>());
        }

        itemViewer.alpha = 0;
        itemViewer.interactable = false;
        itemViewer.blocksRaycasts = false;
    }

    private void OnAddItem(bool hasAddedItem)
    {
        if (hasAddedItem) UpdateInventory();
    }

    private void OnRemoveItem(bool hasRemovedItem)
    {
        if (hasRemovedItem) UpdateInventory();
    }

    private void OnSplitItem(bool hasSplitItem)
    {
        if (hasSplitItem) UpdateInventory();
    }
    
    private void UpdateInventory()
    {
        for (int ii = 0; ii < InventorySystem_Static.GetPlayerInventorySize(); ii++)
        {
            Item_SO item = InventorySystem_Static.GetItem(ii, out bool hasGotItem);

            if (_slotPositions[ii].childCount > 0 && !hasGotItem)
            {
                _slotPositions[ii].GetComponent<Image>().enabled = true;
                Destroy(_slotPositions[ii].GetChild(0));
            }
            else if(_slotPositions[ii].childCount == 0 && hasGotItem)
            {
                _slotPositions[ii].GetComponent<Image>().enabled = false;
                PlayerInventory_Button button = Instantiate(inventoryButton, _slotPositions[ii]).GetComponent<PlayerInventory_Button>();
                button.Initialise(ii, this);
            }
        }
    }

    public void OpenItemViewer(Item_SO item)
    {
        itemViewer.alpha = 1;
        itemViewer.interactable = true;
        itemViewer.blocksRaycasts = true;
        ItemViewer.current.ShowItem(item);
    }

    public void CloseItemViewer()
    {
        itemViewer.alpha = 0;
        itemViewer.interactable = false;
        itemViewer.blocksRaycasts = false;
        ItemViewer.current.HideItem();
    }
}
