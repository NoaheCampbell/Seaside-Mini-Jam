using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Screens")]
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject controls;
    public GameObject credits;

    [Header("Settings UI")]
    public Slider effectsVolume;
    public Slider musicVolume;

    #region functions

    public void OpenMenu(string menuName)
    {
        // set all menus off
        mainMenu.SetActive(false);
        settings.SetActive(false);
        controls.SetActive(false);
        credits.SetActive(false);

        if (menuName == "mainMenu")
        {
            mainMenu.SetActive(true);
        }
        else if (menuName == "settings")
        {
            settings.SetActive(true);
        }
        else if (menuName == "controls")
        {
            controls.SetActive(true);
        }
        else if (menuName == "credits")
        {
            credits.SetActive(true);
        }
    }

    public void PlayGame()
    {

    }

    public void ExitGame()
    {

    }

    public void UpdateSettings()
    {

    }

    #endregion 
}
