using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardMenu : MonoBehaviour
{
    public GameObject gameOverMenu;
    public Button firstBtn;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        //shows menu and hides previous menu when opened
        gameObject.SetActive(true);
        gameOverMenu.SetActive(false);
        firstBtn.Select();
    }

    public void ExitMenu()
    {
        gameObject.SetActive(false);
        gameOverMenu.GetComponent<GameOverMenu>().OpenMenu();
    }
}
