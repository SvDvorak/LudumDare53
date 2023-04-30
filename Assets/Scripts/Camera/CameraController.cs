using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;

    private float screenWidth;
    private float screenHeight;
    float screenRightEdge;

    private void Start()
    {
        camera = GetComponent<Camera>();

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Get the orthographic size of the camera
        float orthoSize = camera.orthographicSize;

        // Calculate the screen aspect ratio
        float screenAspect = screenWidth / screenHeight;

        // Calculate the screen width in world units
        float screenWorldWidth = orthoSize * 2f * screenAspect;

        // Calculate the right edge of the screen in world units
        screenRightEdge = camera.transform.position.x + screenWorldWidth / 2f;

    }


    void Update()
    {
        
        Debug.Log($"right {screenRightEdge} mouse: {camera.ScreenToWorldPoint(Input.mousePosition)}");
    }

}
