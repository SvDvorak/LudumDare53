using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInfoPositionAdjuster : MonoBehaviour
{
    void Start()
    {
        var canvasRect = GameObject.Find("OverlayUI").GetComponent<Canvas>().GetComponent<RectTransform>();
        Vector2 centerPoint = canvasRect.position;
        bool isMouseOnRight = Input.mousePosition.x > centerPoint.x;

        var rectTransform = GetComponent<RectTransform>();
        if (isMouseOnRight)
            rectTransform.localPosition = new Vector2(canvasRect.rect.xMin + rectTransform.rect.width * 0.5f, -canvasRect.rect.yMin);
        else
            rectTransform.localPosition = new Vector2(canvasRect.rect.xMax - rectTransform.rect.width * 0.5f, -canvasRect.rect.yMin);
    }
}
