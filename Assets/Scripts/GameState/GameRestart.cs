using System;
using UnityEngine;

public class GameRestart : MonoBehaviour
{
    public void RestartGame()
    {
        GameState.Instance = new GameState();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            RestartGame();
    }
}
