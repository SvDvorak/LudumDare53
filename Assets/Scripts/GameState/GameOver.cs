using System.Linq;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameOverPresentation GameOverPanel;
    public ShowEnterLocationInfo showEnterLocationInfo;

    public void Start()
    {
        ShowEnterLocationInfo.HidInfo += CheckGameOver;
    }

    public void Show(bool death, string text)
    {
        GameOverPanel.Show(death, text);
    }

    private void CheckGameOver()
    {
        if(GameState.Instance.Characters.Any(x => x.IsAlive))
            return;
        GameOverPanel.Show(true, GameInfo.Instance.PartyDeadGameOverText);
    }
}