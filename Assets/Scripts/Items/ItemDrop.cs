using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private Inventory inventory;
    private Dictionary<string, int> items = new Dictionary<string, int>();
    private Dictionary<string, float> itemWeights = new Dictionary<string, float>();
    private GameObject item;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    public void DropItems()
    {
        items = inventory.GetItemsList();
        itemWeights = inventory.GetWeightsList();

        foreach (string key in items.Keys)
        {
            if (items[key] > 0)
            {
                item = (Resources.Load(key, typeof(GameObject)) as GameObject);
                item.GetComponent<Item>().ammount = items[key];
                item.GetComponent<Item>().weight = itemWeights[key];
                Instantiate(item, transform.position, Quaternion.identity);
            }
        }
    }
}
