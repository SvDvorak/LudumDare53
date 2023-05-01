using UnityEngine;

public class LocationInfoPositionAdjuster : MonoBehaviour
{
    public float screenOffset;
    private Camera mainCamera;
    private RectTransform canvasRect;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        canvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        Vector2 centerPoint = canvasRect.position;
        bool isMouseOnRight = mainCamera.ScreenToWorldPoint(Input.mousePosition).x > centerPoint.x;

        var rectTransform = GetComponent<RectTransform>();
        if (isMouseOnRight)
            rectTransform.localPosition = new Vector2(canvasRect.rect.xMin + rectTransform.rect.width * 0.5f + screenOffset, -canvasRect.rect.yMin - screenOffset);
        else
            rectTransform.localPosition = new Vector2(canvasRect.rect.xMax - rectTransform.rect.width * 0.5f - screenOffset, -canvasRect.rect.yMin - screenOffset);
    }
}
