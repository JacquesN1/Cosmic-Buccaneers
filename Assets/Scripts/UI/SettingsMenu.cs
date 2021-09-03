using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public GameObject startMenu;
    public Button firstBtn;
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Awake()
    {
        //hides menu on game start
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        //shows menu and hides previous menu when opened
        gameObject.SetActive(true);
        startMenu.SetActive(false);
        firstBtn.Select();

        //Populate resolution menu
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void ExitMenu()
    {
        gameObject.SetActive(false);
        startMenu.GetComponent<MainMenu>().OpenMenu();
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
