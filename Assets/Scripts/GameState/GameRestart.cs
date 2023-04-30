using System;
using UnityEngine;

public class GameRestart : MonoBehaviour
{
    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            RestartGame();
    }
}
