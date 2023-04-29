using System.Collections;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeTime = 0.5f;

    private float fadeStartTime;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeOutAndDestroy()
    {
        StartCoroutine(FadeOut());
        Destroy(gameObject, fadeTime);
    }

    private IEnumerator FadeIn()
    {
        var endTime = Time.time + fadeTime;
        while (canvasGroup.alpha < 1)
        {
            var elapsed = (endTime - Time.time) / fadeTime;
            canvasGroup.alpha = elapsed;

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        var endTime = Time.time + fadeTime;
        while (canvasGroup.alpha > 0)
        {
            var elapsed = (endTime - Time.time) / fadeTime;
            canvasGroup.alpha = 1 - elapsed;

            yield return null;
        }
    }
}