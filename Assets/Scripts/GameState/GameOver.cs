using System;
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

    public void OnDestroy()
    {
        ShowEnterLocationInfo.HidInfo -= CheckGameOver;
    }

    public void Show(bool death, string title, string description)
    {
        GameOverPanel.Show(death, title, description);
    }

    private void CheckGameOver()
    {
        if(GameState.Instance.Characters.Any(x => x.IsAlive))
            return;
        Show(true, "Party dead", GameInfo.Instance.PartyDeadGameOverText);
    }
}