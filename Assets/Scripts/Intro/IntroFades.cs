using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroFades : MonoBehaviour
{
    public FadeEffect FadeEffect;
    public AudioSource Music;

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
        StartCoroutine(FadeOutMusic());
        StartCoroutine(WaitAndChangeToGameScene());
    }

    private IEnumerator FadeOutMusic()
    {
        var startVolume = Music.volume;
        var startTime = Time.time;
        while (Music.volume > 0)
        {
            var elapsed = (Time.time - startTime) / FadeEffect.fadeTime;
            Music.volume = startVolume * (1 - elapsed);

            yield return null;
        }
    }

    private IEnumerator WaitAndChangeToGameScene()
    {
        yield return new WaitForSeconds(FadeEffect.fadeTime + 0.5f);
        SceneManager.LoadScene("GameScene");
    }
}
