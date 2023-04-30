using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CursorCameraMover : MonoBehaviour
{
    public Transform targetSprite;
    private Camera camera;


    private float speed = 10f;
    private float edgeMargin = 20f;

    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x < edgeMargin && transform.position.x > -screenWidth / 2)
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        else if (mousePos.x > screenWidth - edgeMargin && transform.position.x < screenWidth / 2)
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (mousePos.y < edgeMargin && transform.position.y > -screenHeight / 2)
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
        else if (mousePos.y > screenHeight - edgeMargin && transform.position.y < screenHeight / 2)
            transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
