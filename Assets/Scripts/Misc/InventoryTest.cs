using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Item_SO item;

    private void Awake()
    {
        Invoke(nameof(Add), 0.2f);
    }

    private void Add()
    {
        InventorySystem_Static.AddItem(item, out _);
    }
}
