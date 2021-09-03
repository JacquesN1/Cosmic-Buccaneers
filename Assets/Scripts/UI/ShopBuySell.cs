using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuySell : MonoBehaviour
{
    public string item;
    public bool bying;
    public GameObject player;
    public ShopMenu shopMenu;
    private Planet planet;
    private PlanetShop planetShop;
    private PlayerInventory playerInventory;

    public void BuySellItem()
    {
        planet = shopMenu.GetPlanet();
        planetShop = planet.gameObject.GetComponent<PlanetShop>();
        playerInventory = player.gameObject.GetComponent<PlayerInventory>();

        if (bying
        && planetShop.ReturnItemAmmount(item) > 0
        && (playerInventory.ReturnItemAmmount("Bits") - planetShop.GetItemPrice(item) >= 0)
        && (playerInventory.weightCarrying + planetShop.ReturnItemWeight(item) <= playerInventory.maxWeight))
        {
            playerInventory.EditItemAmmount(item, 1);
            planetShop.EditItemAmmount(item, -1);
            playerInventory.EditItemAmmount("Bits",-planetShop.GetItemPrice(item));
            planetShop.EditItemAmmount("Bits", planetShop.GetItemPrice(item));
            playerInventory.weightCarrying += planetShop.ReturnItemWeight(item);

        }
        else
        if (!bying
        && playerInventory.ReturnItemAmmount(item) > 0
        && (planetShop.ReturnItemAmmount("Bits") - planetShop.GetItemPrice(item) >= 0))
        {
            playerInventory.EditItemAmmount(item, -1);
            planetShop.EditItemAmmount(item, +1);
            playerInventory.EditItemAmmount("Bits", planetShop.GetItemPrice(item));
            planetShop.EditItemAmmount("Bits", -planetShop.GetItemPrice(item));
            playerInventory.weightCarrying -= planetShop.ReturnItemWeight(item);
        }
    }
}
