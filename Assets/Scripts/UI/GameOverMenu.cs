using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject player;
    public GameController gameController;
    public Button firstBtn;
    public TextMeshProUGUI daysSurvivedText;
    public GameObject leaderboardMenu;

    void Awake()
    {
        //hides menu on game start
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        player.GetComponent<PlayerController>().PausePlayerControls(true);
        Cursor.visible = true;
        Time.timeScale = 0;

        gameObject.SetActive(false);
        daysSurvivedText.text = ($"YOU LASTED {gameController.daysSurvived} DAYS");
        gameObject.SetActive(true);

        firstBtn.Select();
    }

    public void ExitToMain()
    {
        player.GetComponent<PlayerController>().PausePlayerControls(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void NewGame()
    {
        player.GetComponent<PlayerController>().PausePlayerControls(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenLeaderboard()
    {
        gameObject.SetActive(false);
        leaderboardMenu.GetComponent<LeaderboardMenu>().OpenMenu();
    }
}
