using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        Initialise();
        _inventoryCanvasGroup = GetComponent<CanvasGroup>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!(_canClose)) return;
        
        if (Input.GetMouseButtonDown(0) && !RectTransformUtility.RectangleContainsScreenPoint(panelRectTransform, Input.mousePosition))
        {
            CloseMenu();
        }
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

        TurnOffCanvasGroup(itemViewer);
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
            }
            else if(_slotPositions[ii].childCount == 0 && hasGotItem)
            {
                _slotPositions[ii].GetComponent<Image>().enabled = false;
                PlayerInventory_Button button = Instantiate(inventoryButton, _slotPositions[ii]).GetComponent<PlayerInventory_Button>();
                button.Initialise(ii, this);
            }
        }
    }

    public void OpenMenu()
    {
        TurnOnCanvasGroup(_inventoryCanvasGroup);
        _canClose = true;
    }

    public void CloseMenu()
    {
        TurnOffCanvasGroup(_inventoryCanvasGroup);
        _canClose = false;
    }

    public void OpenItemViewer(Item_SO item)
    {
        TurnOnCanvasGroup(itemViewer);
        ItemViewer.current.ShowItem(item);
        _canClose = false;
    }

    public void CloseItemViewer()
    {
        TurnOffCanvasGroup(itemViewer);
        ItemViewer.current.HideItem();
        _canClose = true;
    }

    private void TurnOnCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void TurnOffCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
