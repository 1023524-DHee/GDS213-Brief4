using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Recycling/Item")]
public class Item_SO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    
    public GameObject itemModel;
    public List<Item_SO> itemComponents;

    public RecycleInstructions_SO recycleInstructions;
}
