using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RepairMenu : MonoBehaviour
{
    public GameObject planetMenu;
    public PlayerController player;
    private PlayerInventory playerInventory;
    public Button firstBtn;
    private Planet planet;
    public Bar healthBar;
    public int repairCost = 5;
    private int totalCost;
    public TextMeshProUGUI textMesh;

    void Awake()
    {
        //hides menu on game start
        gameObject.SetActive(false);
    }

    public void OpenMenu(Planet _planet)
    {
        planet = _planet;
        playerInventory = player.gameObject.GetComponent<PlayerInventory>();

        //shows menu and hides previous menu when opened
        gameObject.SetActive(true);
        planetMenu.SetActive(false);
        firstBtn.Select();

        healthBar.maxValue = player.maxHealth;
        healthBar.currentValue = player.health;

        totalCost = (int)(player.maxHealth - player.health) * repairCost;
        gameObject.SetActive(false);
        textMesh.text = $"{totalCost} BITS TO REPAIR";
        gameObject.SetActive(true);
    }

    public void ExitMenu()
    {
        gameObject.SetActive(false);
        planetMenu.GetComponent<PlanetMenu>().OpenMenu(planet);
    }

    public void RepairButton()
    {
        if (playerInventory.ReturnItemAmmount("Bits") - totalCost >= 0)
        {
            playerInventory.EditItemAmmount("Bits", -totalCost);
            player.HealDamage();
            healthBar.currentValue = player.health;

            totalCost = (int)(player.maxHealth - player.health) * repairCost;
            gameObject.SetActive(false);
            textMesh.text = $"{totalCost} BITS TO REPAIR";
            gameObject.SetActive(true);
        }
    }
}
