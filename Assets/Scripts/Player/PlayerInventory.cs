using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    public float maxWeight;
    public float weightCarrying = 0;
    public int crew;
    public int crewCapacity;

    public override void OnAwake()
    {
        calciulateInventoryWeight();
    }

    public void calciulateInventoryWeight()
    {
        weightCarrying = 0;

        foreach (string key in items.Keys)
        {
            weightCarrying += (itemWeights[key] * items[key]);
        }
    }
}
