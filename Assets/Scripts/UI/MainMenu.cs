using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button firstBtn;
    public GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        firstBtn.Select();
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        firstBtn.Select();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsMenu()
    {
        gameObject.SetActive(false);
        settingsMenu.GetComponent<SettingsMenu>().OpenMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
