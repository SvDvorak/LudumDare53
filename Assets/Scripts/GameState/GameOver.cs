using System.Linq;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverPanel;
    public ShowEnterLocationInfo showEnterLocationInfo;

    public void Start()
    {
        showEnterLocationInfo.HidInfo += CheckGameOver;
        GameOverPanel.SetActive(false);
    }

    private void CheckGameOver()
    {
        if(GameState.Instance.Characters.Any(x => x.IsAlive))
            return;
        GameOverPanel.GetComponent<FadeEffect>().FadeInAndEnable();
    }
}