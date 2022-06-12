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
    public Slider _effectsVolume;
    public Slider _musicVolume;

    #region functions

    private void Start()
    {
        OpenSettings();
    }

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
            OpenSettings();
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
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().StartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void OpenSettings()
    {
        _effectsVolume.value = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().effectsVolume;
        _musicVolume.value = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().musicVolume;
    }

    public void UpdateSettings()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().effectsVolume = _effectsVolume.value;
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().musicVolume = _musicVolume.value;
    }

    #endregion 
}
