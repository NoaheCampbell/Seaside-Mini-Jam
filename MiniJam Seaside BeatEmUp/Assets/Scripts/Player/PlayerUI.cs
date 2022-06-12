using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private PlayerMaster player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerMaster>();

        OpenSettings();
    }

    public void GameOverScreen()
    {
        player.gameOverScreen.SetActive(true);
    }

    public void SettingsMenu()
    {
        OpenSettings();
        player.settingsMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        player.settingsMenu.SetActive(false);
    }

    void OpenSettings()
    {
        player.effectsVolumeSlider.value = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().effectsVolume;
        player.musicVolumeSlider.value = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().musicVolume;
    }

    public void UpdateSettings()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().effectsVolume = player.effectsVolumeSlider.value;
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().musicVolume = player.musicVolumeSlider.value;

        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().SetSound(true, true);
    }

    public void ExitGame()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().LoadMainMenu();
    }
}
