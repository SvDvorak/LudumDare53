using TMPro;
using UnityEngine;

public class GameOverPresentation : MonoBehaviour
{
    public TMP_Text description;
    public FadeEffect fadeEffect;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        description.text = GameInfo.Instance.PartyDeadGameOverText;
        fadeEffect.FadeInAndEnable();
    }
}
