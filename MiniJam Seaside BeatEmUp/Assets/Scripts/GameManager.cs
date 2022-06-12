using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager Variables")]
    public bool gamePaused = false;
    public int currentLevel = 1;


    // Start is called before the first frame update
    void Start()
    {
        
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

    void LoadNextLevel()
    {

    }

    void LoadCurrentLevel()
    {

    }

    void RespawnPlayer()
    {

    }

    #endregion
}
