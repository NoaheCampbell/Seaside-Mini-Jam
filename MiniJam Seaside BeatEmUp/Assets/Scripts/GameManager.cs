using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Game Manager Variables")]
    public bool gamePaused = false;
    public int currentLevel = 1;

    [Header("Save Variables")]
    public int playerLives = 3;
    [Range(0, 1)] public float effectVolume = .5f;
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

    public void LevelComplete()
    {

    }

    public void MovePlayer(Vector3 position)
    {
        GameObject.FindWithTag("Player").transform.position = position;
    }
    public void RespawnPlayer()
    {
        if (playerLives >= 0)
        {
            LoadCurrentLevel();
        }
        else
        {
            GameOver();
        }
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

    void GameOver()
    {

    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene("Level_" + (currentLevel+1).ToString());
    }

    void LoadCurrentLevel()
    {
        //SceneManager.LoadScene("Level_" + currentLevel.ToString()); 
        SceneManager.LoadScene("PlayerMovementTest");
    }

    #endregion
}
