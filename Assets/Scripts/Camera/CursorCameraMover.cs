using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCameraMover : MonoBehaviour
{
    public float speed = 10f; // Adjust this value to control the speed of camera movement.
    public float edgeMargin = 20f; // Adjust this value to control how close the mouse needs to be to the edge before the camera starts moving.

    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        // Get the screen width and height in pixels.
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        // Get the position of the mouse in pixels.
        Vector3 mousePos = Input.mousePosition;

        // Check if the mouse is close enough to the edge of the screen to move the camera.
        if (mousePos.x < edgeMargin && transform.position.x > -screenWidth / 2)
        {
            // Move the camera to the left.
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }
        else if (mousePos.x > screenWidth - edgeMargin && transform.position.x < screenWidth / 2)
        {
            // Move the camera to the right.
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (mousePos.y < edgeMargin && transform.position.y > -screenHeight / 2)
        {
            // Move the camera down.
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
        }
        else if (mousePos.y > screenHeight - edgeMargin && transform.position.y < screenHeight / 2)
        {
            // Move the camera up.
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }
}
