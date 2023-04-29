using System.Collections;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeTime = 0.5f;

    private float fadeStartTime;

    void Start()
    {
        StartCoroutine(FadeInRoutine());
    }

    public void FadeOutAndDestroy()
    {
        StartCoroutine(FadeOutRoutine());
        Destroy(gameObject, fadeTime);
    }
    
    public void FadeOutAndDisable() => StartCoroutine(FadeOutAndDisableRoutine());
    public void FadeIn() => StartCoroutine(FadeInRoutine());
    public void FadeOut() => StartCoroutine(FadeOutRoutine());

    private IEnumerator FadeOutAndDisableRoutine()
    {
        yield return FadeOutRoutine();
        gameObject.SetActive(false);
    }

    private IEnumerator FadeInRoutine()
    {
        canvasGroup.alpha = 0;
        var startTime = Time.time;
        while (canvasGroup.alpha < 1)
        {
            var elapsed = (Time.time - startTime) / fadeTime;
            canvasGroup.alpha = elapsed;

            yield return null;
        }
    }

    private IEnumerator FadeOutRoutine()
    {
        canvasGroup.alpha = 1;
        var startTime = Time.time;
        while (canvasGroup.alpha > 0)
        {
            var elapsed = (Time.time - startTime) / fadeTime;
            canvasGroup.alpha = 1 - elapsed;

            yield return null;
        }
    }
}