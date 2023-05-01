using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroFades : MonoBehaviour
{
    public FadeEffect FadeEffect;

    public void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        FadeEffect.FadeOut();
    }
    
    public void FadeOut()
    {
        FadeEffect.FadeIn();
        StartCoroutine(WaitAndChangeToGameScene());
    }

    private IEnumerator WaitAndChangeToGameScene()
    {
        yield return new WaitForSeconds(FadeEffect.fadeTime + 0.5f);
        SceneManager.LoadScene("GameScene");
    }
}
