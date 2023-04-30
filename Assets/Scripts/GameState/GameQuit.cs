using UnityEngine;

public class GameQuit : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            QuitGame();
    }
}
