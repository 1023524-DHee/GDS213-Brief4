using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float money = 0;
    public float Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            EventManager.Instance.onMoneyChanged.Invoke(money);
        }
    }

    public ShopItem currentViewedShopItem;
}
