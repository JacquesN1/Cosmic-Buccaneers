using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetMenuHoldSpace : MonoBehaviour
{
    private float spaceInHold;
    public TextMeshProUGUI holdSpaceText;
    public GameObject player;

    private void Update()
    {
        //Calculates remaining carry capacity of player and displays in menu
        spaceInHold = player.gameObject.GetComponent<PlayerInventory>().maxWeight - player.gameObject.GetComponent<PlayerInventory>().weightCarrying;
        gameObject.SetActive(false);
        holdSpaceText.text = $"SPACE IN HOLD: {spaceInHold}";
        gameObject.SetActive(true);
    }
}
