using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseSubMenu : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(false);
        levelText.text = $"LV: {player.level}";
        xpText.text = $"XP: {player.xp}/{player.maxXP}";
        gameObject.SetActive(true);
    }
}
