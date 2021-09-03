//Handles all code for the shop menu including handling transactions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    public GameObject planetMenu;
    public GameObject player;
    public Button firstBtn;
    private Planet planet;

    void Awake()
    {
        //hides menu on game start
        gameObject.SetActive(false);
    }

    public void OpenMenu(Planet _planet)
    {
        planet = _planet;

        //shows menu and hides previous menu when opened
        gameObject.SetActive(true);
        planetMenu.SetActive(false);
        firstBtn.Select();
    }

    public void ExitMenu()
    {
        gameObject.SetActive(false);
        planetMenu.GetComponent<PlanetMenu>().OpenMenu(planet);
    }

    // Returns planet the store is located in
    public Planet GetPlanet()
    {
        return planet;
    }
}
