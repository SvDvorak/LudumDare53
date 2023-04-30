using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float MaxZoom = 8;
    public float MinZoom = 2;
    public float ScrollSpeed = 1;
    private Camera mainCamera;

    public void Start()
    {
        mainCamera = Camera.main;
    }

    public void Update()
    {
        var scroll = mainCamera.orthographicSize + Input.mouseScrollDelta.y * ScrollSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(scroll, MinZoom, MaxZoom);
    }
}