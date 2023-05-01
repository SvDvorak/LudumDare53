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

    public void Show(bool isVictory, string text)
    {
        Debug.Log("Play victory or defeat sound");
        description.text = text;
        fadeEffect.FadeInAndEnable();
    }
}
