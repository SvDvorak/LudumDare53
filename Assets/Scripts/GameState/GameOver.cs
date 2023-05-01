using System.Linq;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameOverPresentation GameOverPanel;
    public ShowEnterLocationInfo showEnterLocationInfo;

    public void Start()
    {
        showEnterLocationInfo.HidInfo += CheckGameOver;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F4))
            GameState.Instance.Characters.ForEach(x => x.IsAlive = false);
    }

    private void CheckGameOver()
    {
        if(GameState.Instance.Characters.Any(x => x.IsAlive))
            return;
        GameOverPanel.Show();
    }
}