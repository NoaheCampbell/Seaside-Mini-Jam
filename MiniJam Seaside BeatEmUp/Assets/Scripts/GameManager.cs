using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Game Manager Variables")]
    public bool gamePaused = false;
    public int currentLevel = 0;

    [Header("Save Variables")]
    public int playerLives = 3;
    [Range(0, 1)] public float effectsVolume = .5f;
    [Range(0, 1)] public float musicVolume = .5f;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        GamePauseCheck();
    }

    #region public functions

    public void StartGame()
    {
        LoadNextLevel();
    }

    public void LevelComplete()
    {
        LoadNextLevel();
    }

    public void MovePlayer(Vector3 position)
    {
        GameObject.FindWithTag("Player").transform.position = position;
    }
    public void RespawnPlayer()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerUI>().UpdateLives();
        if (playerLives > 0)
        {
            LoadCurrentLevel();
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    public void SetSound(bool effects, bool music)
    {
        if (effects)
        {
            GameObject.FindWithTag("EffectsAudio").GetComponent<AudioSource>().volume = effectsVolume;
        }

        if (music)
        {
            GameObject.FindWithTag("MusicAudio").GetComponent<AudioSource>().volume = musicVolume;
        }
    }

    public void UpdateMovementVolume()
    {
        GameObject.FindWithTag("MovementAudio").GetComponent<AudioSource>().volume = effectsVolume;
    }

    #endregion

    #region private functions

    void GamePauseCheck()
    {
        if (gamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    IEnumerator GameOver()
    {
        currentLevel = 0;

        GameObject.FindWithTag("Player").GetComponent<PlayerUI>().GameOverScreen();

        yield return new WaitForSeconds(3);

        // load main menu
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    void LoadNextLevel()
    {
        currentLevel += 1;
        SceneManager.LoadScene("Level_" + (currentLevel).ToString());
    }

    void LoadCurrentLevel()
    {
        //SceneManager.LoadScene("Level_" + currentLevel.ToString()); 
        SceneManager.LoadScene("PlayerMovementTest");
    }

    #endregion
}
