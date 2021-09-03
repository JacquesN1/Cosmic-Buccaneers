using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject shopMenu;
    public GameObject repairMenu;
    public GameObject planetName;
    public Button firstBtn;
    private Planet planet;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu(Planet _planet)
    {
        planet = _planet;
        gameObject.SetActive(true);
        SetMenuPlanetName(planet.planetName);
        player.GetComponent<PlayerController>().PausePlayerControls(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        firstBtn.Select();
    }

    public void OpenShop()
    {
        gameObject.SetActive(false);
        shopMenu.GetComponent<ShopMenu>().OpenMenu(planet);
    }

    public void OpenRepairMenu()
    {
        gameObject.SetActive(false);
        repairMenu.GetComponent<RepairMenu>().OpenMenu(planet);
    }

    public void ExitMenu()
    {
        gameObject.SetActive(false);
        player.GetComponent<PlayerController>().PausePlayerControls(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    private void SetMenuPlanetName(string _planetName)
    {
        gameObject.SetActive(false);
        planetName.GetComponent<TextMeshProUGUI>().text = (_planetName);
        gameObject.SetActive(true);
    }
}
