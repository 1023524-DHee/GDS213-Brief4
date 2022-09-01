using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Item_SO itemSO;
    [SerializeField] private GameObject itemObject;

    public bool Pickup()
    {
        InventorySystem_Static.AddItem(itemSO, out bool successfulAdd);
        if (successfulAdd && GameManager.Instance.Money >= itemSO.purchasePrice)
        {
            ItemAdded();
            return true;
        }
        ItemNotAdded();
        return false;
    }

    private void ItemAdded()
    {
        Debug.Log("Item picked up");
        GameManager.Instance.Money -= itemSO.purchasePrice;

        // TODO Randomise the shop item
        Destroy(itemObject);
        itemSO = null;
    }

    private void ItemNotAdded()
    {
        Debug.Log("Inventory is full");
    }

    public void ShowItem()
    {
        if (!itemSO) return;

        EventManager.Instance.onUpdateShopViewedItem.Invoke(this);
        GameManager.Instance.currentViewedShopItem = this;

        Debug.Log($"Purchase {itemSO.itemName}");
    }

    public void StopShowItem()
    {
        EventManager.Instance.onUpdateShopViewedItem.Invoke(null);
        GameManager.Instance.currentViewedShopItem = null;
    }
}
