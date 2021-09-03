//Displayes and updtates the text in the shop menu to show fow many items are in the players and shops inventory
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenuTextUpdater : MonoBehaviour
{
    public GameObject player;
    public ShopMenu shopMenu;
    public bool isPlayerItem;
    public bool isPrice = false;
    private Inventory inventory;
    public string variableName;
    public string itemName;
    private TextMeshProUGUI textMesh;
    private int ammount;

    void OnEnable()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();

        //Gets a reference to either the players or the shops inventory 
        if (isPlayerItem)
        {
            inventory = player.GetComponent<PlayerInventory>();
        }
        else if (shopMenu.GetPlanet() != null)
        {
            inventory = shopMenu.GetPlanet().GetPlanetShop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Finds specified field in inventory from string and displays in the menu
        if (isPrice)
        {
            ammount = shopMenu.GetPlanet().GetPlanetShop().GetItemPrice(variableName);
        }
        else
        {
            ammount = inventory.ReturnItemAmmount(variableName);
        }

        gameObject.SetActive(false);
        textMesh.text = $"{ammount} {itemName}";
        gameObject.SetActive(true);
    }
}
