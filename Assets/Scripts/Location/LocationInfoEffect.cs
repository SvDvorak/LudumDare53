using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfoEffect : MonoBehaviour
{
    private float alpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAlpha();

        StartCoroutine(FadeIn());
    }

    private void UpdateAlpha()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, alpha);
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent<Image>(out var image))
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            else if (child.gameObject.TryGetComponent<TextMeshProUGUI>(out var text))
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }
    }

    private IEnumerator FadeIn()
    {
        while (alpha < 1)
        {
            alpha += 0.05f;
            UpdateAlpha();

            yield return new WaitForSeconds(0.03f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
