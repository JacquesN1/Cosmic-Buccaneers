using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Planet : InteractableObj
{
    public GameObject planetMenu;
    public EnemyShipSpawner shipSpawner;
    private PlanetShop shop;
    public string planetName;

    private void Awake()
    {
        shop = gameObject.GetComponent<PlanetShop>();
        shipSpawner.AddPlanetToList(gameObject);
    }

    public override void onInteract()
    {
        planetMenu.GetComponent<PlanetMenu>().OpenMenu(this);
    }

    public override void onOverlap()
    {
        player.GetComponent<PlayerController>().PopUpText("PRESS INTERACT BUTTON TO DOCK", 5000);
    }

    public override void onExit()
    {
        player.GetComponent<PlayerController>().PopUpText("", 1);
    }

    public PlanetShop GetPlanetShop() 
    {
        return shop;
    }
}
