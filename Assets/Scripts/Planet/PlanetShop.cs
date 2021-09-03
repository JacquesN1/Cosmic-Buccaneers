//Stores the inventry and prices of a planets stock;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShop : Inventory
{
    private Planet planet;

    public Dictionary<string, int> prices = new Dictionary<string, int>();
    public int foodPrice = 0;
    public int scrapPrice = 0;
    public int goodsPrice = 0;
    public int energyPrice = 0;
    public int ammoPrice = 0;

    public override void OnAwake()
    {
        prices.Add("Food", foodPrice);
        prices.Add("Scrap", scrapPrice);
        prices.Add("Goods", goodsPrice);
        prices.Add("Ammo", ammoPrice);
        prices.Add("Energy", energyPrice);

        planet = gameObject.GetComponent<Planet>();
    }

    public int GetItemPrice(string item)
    {
        if (prices.ContainsKey(item))
        {
            return prices[item];
        }
        else
        {
            Debug.Log("Item does not exist");
            return 0;
        }
    }

    public void DayPassed()
    { 
        if (ReturnItemAmmount("Bits") <= 5000)
        {
            EditItemAmmount("Bits", +1);
        }
    }
}
