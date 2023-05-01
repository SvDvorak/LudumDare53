using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 1f;
    public float minZoom = 1f;
    public float maxZoom = 10f;
    public float smoothTime = 0.1f; // Adjust this to set the zoom smoothness

    private Camera mainCamera;
    private float currentZoom; // Adjust this to set the initial zoom level
    private float targetZoom;

    private void Start()
    {
        currentZoom = maxZoom;
        mainCamera = GetComponent<Camera>();
        targetZoom = currentZoom;
    }

    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        targetZoom -= scroll * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, smoothTime);
        mainCamera.orthographicSize = currentZoom;
    }
}

