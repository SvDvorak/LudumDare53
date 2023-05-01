using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class CameraCalculations
{
    private static Camera camera;

    private static float screenWidth;
    private static float screenHeight;
    private static float screenWorldWidth;
    private static float screenRightEdge;
    private static float screenLeftEdge;

    private static void SetupCalculations()
    {
        camera = GameObject.FindObjectOfType<Camera>();

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        float orthoSize = camera.orthographicSize;
        float screenAspect = screenWidth / screenHeight;
        screenWorldWidth = orthoSize * 2f * screenAspect;
    }

    public static float GetLeftEdge()
    {
        SetupCalculations();
        return camera.transform.position.x - screenWorldWidth * 0.5f;
    }

    public static float GetRightEdge()
    {
        SetupCalculations();
        return camera.transform.position.x + screenWorldWidth * 0.5f;
    }
}
