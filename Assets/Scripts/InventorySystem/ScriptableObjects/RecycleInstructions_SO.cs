using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecycleInstructions", menuName = "Recycling/Instructions")]
public class RecycleInstructions_SO : ScriptableObject
{
    public List<RecycleBin> correctBin;
    public List<RecycleInstructions> recycleInstructions;
}
