using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CursorCameraMover : MonoBehaviour
{
    [SerializeField] Transform targetSprite;
    private Camera camera;

    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    public float speed = 10f;
    public float edgeMargin = 20f;

    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        camera = GetComponent<Camera>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void OldVersionUpdate()
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
