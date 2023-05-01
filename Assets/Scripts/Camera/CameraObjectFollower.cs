using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectFollower : MonoBehaviour
{
    public SpriteRenderer background;

    public Transform target;
    public float smoothTime = 0.3f;

    private Camera camera;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private Vector2 spriteSize;

    void Start()
    {
        camera = GetComponent<Camera>();
        camera.transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        spriteSize = background.sprite.bounds.size;
        spriteSize.x -= 2;
        spriteSize.y -= 2;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        Vector3 cameraPosition = camera.transform.position;
        Vector2 cameraViewSize = new Vector2(camera.orthographicSize * camera.aspect * 2, camera.orthographicSize * 2);


        float maxHorizontalOffset = spriteSize.x / 2f - cameraViewSize.x / 2f;
        float maxVerticalOffset = spriteSize.y / 2f - cameraViewSize.y / 2f;

        float horizontalOffset = Mathf.Clamp(target.position.x - background.transform.position.x, -maxHorizontalOffset, maxHorizontalOffset);
        float verticalOffset = Mathf.Clamp(target.position.y - background.transform.position.y, -maxVerticalOffset, maxVerticalOffset);

        targetPosition.x = background.transform.position.x + horizontalOffset;
        targetPosition.y = background.transform.position.y + verticalOffset;

        bool isCameraOutsideSprite = cameraPosition.x < background.transform.position.x - spriteSize.x / 2 + cameraViewSize.x / 2
            || cameraPosition.x > background.transform.position.x + spriteSize.x / 2 - cameraViewSize.x / 2
            || cameraPosition.y < background.transform.position.y - spriteSize.y / 2 + cameraViewSize.y / 2
            || cameraPosition.y > background.transform.position.y + spriteSize.y / 2 - cameraViewSize.y / 2;

        if (isCameraOutsideSprite)
            camera.transform.position = targetPosition;
        else
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
