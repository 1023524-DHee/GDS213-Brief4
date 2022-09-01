using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    public UnityEvent<ShopItem> onUpdateShopViewedItem = new UnityEvent<ShopItem>();

    /// <summary>
    /// Value is the current player's bank value
    /// </summary>
    public UnityEvent<float> onMoneyChanged = new UnityEvent<float>();

    /// <summary>
    /// Value is true if the player is in the purchase area
    /// </summary>
    public UnityEvent<bool> purchaseAreaChanged = new UnityEvent<bool>();
}
