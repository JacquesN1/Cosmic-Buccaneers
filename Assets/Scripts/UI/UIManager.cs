using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    public GameController gameController;
    public UIUpdater UIDateText;
    public UIUpdater UIBitsText;
    public UIUpdater UIEnergyText;
    public UIUpdater UIFoodText;
    public UIUpdater UIAmmoText;
    public UIUpdater UIScrapText;
    public UIUpdater UIGoodsText;
    public UIUpdater UIWantedLevelText;
    public Bar healthBar;

    // Update is called once per frame
    void Update()
    {
        //Update healthbar
        healthBar.maxValue = player.GetComponent<PlayerController>().maxHealth;
        healthBar.currentValue = player.GetComponent<PlayerController>().health;

        UIDateText.UpdateText(gameController.currentDate.Date.ToString("dd/MM/yyyy"));
        UIBitsText.UpdateText($"BITS: {player.GetComponent<PlayerInventory>().ReturnItemAmmount("Bits")}");
        UIEnergyText.UpdateText($"ENERGY: {player.GetComponent<PlayerInventory>().ReturnItemAmmount("Energy")}");
        UIFoodText.UpdateText($"FOOD: {player.GetComponent<PlayerInventory>().ReturnItemAmmount("Food")}");
        UIAmmoText.UpdateText($"AMMO: {player.GetComponent<PlayerInventory>().ReturnItemAmmount("Ammo")}");
        UIScrapText.UpdateText($"SCRAP: {player.GetComponent<PlayerInventory>().ReturnItemAmmount("Scrap")}");
        UIGoodsText.UpdateText($"GOODS: {player.GetComponent<PlayerInventory>().ReturnItemAmmount("Goods")}");
        UIWantedLevelText.UpdateText($"WANTED LEVEL: {player.GetComponent<PlayerController>().wantedLevel}");
    }
}
