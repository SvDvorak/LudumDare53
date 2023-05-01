using TMPro;
using UnityEngine;

public class GameOverPresentation : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public FadeEffect fadeEffect;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(bool isVictory, string titleText, string descriptionText)
    {
        Debug.Log("Play victory or defeat sound");
        title.text = titleText;
        description.text = descriptionText;
        fadeEffect.FadeInAndEnable();
    }
}
