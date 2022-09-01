using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private GameObject purchaseButton;

    private void Start()
    {
        purchaseButton.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.Instance.onMoneyChanged.AddListener(UpdateMoney);
        EventManager.Instance.onUpdateShopViewedItem.AddListener(ShowPurchaseButton);
        UpdateMoney(GameManager.Instance.Money);
    }

    private void OnDisable()
    {
        EventManager.Instance.onMoneyChanged.RemoveListener(UpdateMoney);
        EventManager.Instance.onUpdateShopViewedItem.RemoveListener(ShowPurchaseButton);
    }

    private void UpdateMoney(float money)
    {
        moneyText.text = $"${money}";
    }

    private void ShowPurchaseButton(ShopItem shopItem)
    {
        purchaseButton.SetActive(shopItem != null);
    }

    public void PurchaseItem()
    {
        // Pickup item
        GameManager.Instance.currentViewedShopItem.Pickup();
        // Stop viewing item
        EventManager.Instance.onUpdateShopViewedItem.Invoke(null);
    }
}
