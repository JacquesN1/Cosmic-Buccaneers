//Parent class for all iventories in game. Stores the number of each pickup curently in inventory. 
//Also used to store and provide references to the weight of each pickup item.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int bits;
    public int food;
    public int scrap;
    public int goods;
    public int ammo;
    public int energy;

    private float bitsWeight = 0;
    private float foodWeight = 1;
    private float scrapWeight = 1;
    private float goodsWeight = 1;
    private float ammoWeight = 1;
    private float energyWeight = 1;

    public Dictionary<string, int> items = new Dictionary<string, int>();
    public Dictionary<string, float> itemWeights = new Dictionary<string, float>();
    private List<string> keys;

    public void Awake()
    {
        items.Add("Bits", bits);
        items.Add("Food", food);
        items.Add("Scrap", scrap);
        items.Add("Goods", goods);
        items.Add("Ammo", ammo);
        items.Add("Energy", energy);

        itemWeights.Add("Bits", bitsWeight);
        itemWeights.Add("Food", foodWeight);
        itemWeights.Add("Scrap", scrapWeight);
        itemWeights.Add("Goods", goodsWeight);
        itemWeights.Add("Ammo", ammoWeight);
        itemWeights.Add("Energy", energyWeight);

        keys = new List<string>(items.Keys);

        OnAwake();
    }

    public Dictionary<string,int> GetItemsList()
    {
        return items;
    }

    public Dictionary<string,float> GetWeightsList()
    {
        return itemWeights;
    }


    public void EditItemAmmount(string item, int ammountChange)
    {
        if (items.ContainsKey(item))
        {
            items[item] = items[item] + ammountChange;
        }
        else 
        {
            UnityEngine.Debug.Log("Item does not exist");
        }
    }

    public int ReturnItemAmmount(string item)
    {
        if (items.ContainsKey(item))
        {
            return items[item];
        }
        else
        {
            UnityEngine.Debug.Log("Item does not exist");
            return 0;
        }
    }

    public float ReturnItemWeight(string item)
    {
        if (itemWeights.ContainsKey(item))
        {
            return itemWeights[item];
        }
        else
        {
            UnityEngine.Debug.Log("Item does not exist");
            return 0;
        }
    }

    public void RandomizeInventory(int min, int max)
    {
        foreach (string key in keys)
        {
            items[key] = UnityEngine.Random.Range(min, max);
        }
    }

    public virtual void OnAwake(){}
}
