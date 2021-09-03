using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject subMenu;
    public Button firstBtn;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        subMenu.SetActive(true);
        player.GetComponent<PlayerController>().PausePlayerControls(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        firstBtn.Select();
    }

    public void ExitMenu()
    {
        gameObject.SetActive(false);
        subMenu.SetActive(false);
        player.GetComponent<PlayerController>().PausePlayerControls(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ExitToMain()
    {
        player.GetComponent<PlayerController>().PausePlayerControls(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}